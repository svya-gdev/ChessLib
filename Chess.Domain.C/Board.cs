namespace Chess.Domain;

public sealed class Board
{



    private readonly OccupationMap occupationMap = new();
    private readonly PopulationMap populationMap = new();



    private void PieceAddToCoordinates(Piece piece, HomeCoordinates coordinates)
    {
        occupationMap.AddOccupation(coordinates);
        populationMap.AddPiece(piece, coordinates);
    }

    private void PieceRemoveFromCoordinates(HomeCoordinates coordinates)
    {
        occupationMap.RemoveOccupation(coordinates);
        populationMap.RemovePiece(coordinates);
    }

    private Piece PieceReadFromCoordinates(HomeCoordinates coordinates)
    {
        return populationMap.ReadPiece(coordinates);
    }

    private void PieceMoveFromCoordinatesToCoordinates(HomeCoordinates oldCoordinates, HomeCoordinates newCoordinates)
    {
        var piece = PieceReadFromCoordinates(oldCoordinates);
        PieceRemoveFromCoordinates(oldCoordinates);
        PieceAddToCoordinates(piece, newCoordinates);
    }

    private void PieceReplaceFromCoordinatesToCoordinates(HomeCoordinates oldCoordinates, HomeCoordinates newCoordinates)
    {
        PieceRemoveFromCoordinates(newCoordinates);
        PieceMoveFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
    }



    // // // // // // // // // // PIECE PLACEMENT RULES // // // // // // // // // // //



    public bool PieceIsAbleToAdd(Piece piece)
    {
        return !populationMap.IsPieceAdded(piece);
        // Yes, if given piece is not added
    }

    public bool PieceIsAbleToAddToLocation(PieceLocation location)
    {
        return !occupationMap.IsOccupied(location.ToHomeCoordinates);
        // Yes, if given location is not occupied
    }



    // // // // // // // // // // PIECE PLACEMENT COMMAND  // // // // // // // // // //



    public void PieceAddToLocation(Piece piece, PieceLocation location)
    {
        if(!PieceIsAbleToAdd(piece)) throw new RuleBrokenException();
        if(!PieceIsAbleToAddToLocation(location)) throw new RuleBrokenException();

        PieceAddToCoordinates(piece, location.ToHomeCoordinates);
    }



    // // // // // // // // // // PIECE REMOVAL RULE // // // // // // // // // // // //



    public bool PieceIsAbleToRemoveFromLocation(PieceLocation location)
    {
        return populationMap.IsPieceAdded(location.ToHomeCoordinates);
        // Yes, if any piece is added to given location
    }



    // // // // // // // // // // PIECE REMOVAL COMMAND // // // // // // // // // // //



    public void PieceRemoveFromLocation(PieceLocation location)
    {
        if (!PieceIsAbleToRemoveFromLocation(location)) throw new RuleBrokenException();

        PieceRemoveFromCoordinates(location.ToHomeCoordinates);
    }



    // // // // // // // // // // PIECE  READMENT  RULE // // // // // // // // // // //



    public bool PieceIsAbleToReadFromLocation(PieceLocation location)
    {
        return populationMap.IsPieceAdded(location.ToHomeCoordinates);
        // Yes, if any piece is added to given location
    }



    // // // // // // // // // // PIECE  READMENT  COMMAND // // // // // // // // // //



    public Piece PieceReadFromLocation(PieceLocation location)
    {
        if (!PieceIsAbleToReadFromLocation(location)) throw new RuleBrokenException();

        return PieceReadFromCoordinates(location.ToHomeCoordinates);
    }



    // // // // // // // // // // PIECE MOVEMENT RULES  // // // // // // // // // // //



    public bool MoveIsPossibleFromLocation(PieceLocation location)
    {
        return populationMap.IsPieceAdded(location.ToHomeCoordinates);
        // Yes, if any piece is added to given location
    }

    public bool MoveIsChangingPiecePosition(PieceRelocation relocation)
    {
        return !relocation.IsNonMoving;
        // Yes, if given relocation is not non-moving
    }

    public bool MoveIsChangingPiecePosition(PieceDislodgement dislodgement)
    {
        return MoveIsChangingPiecePosition(dislodgement.Relocation);
        // Yes, if relocation in given dislodgement is not non-moving
    }

    public bool MoveIsPossibleThroughSpace(PieceLocation location, PieceRelocation relocation)
    {
        // Yes, if path is horse-like or no occupation is in the way

        if (relocation.IsHorseLike) return true;

        var stepX = Math.Sign(unchecked((int)relocation.FileDelta));
        var stepY = Math.Sign(unchecked((int)relocation.RankDelta));

        var tileSteps = Math.Max(
            Math.Abs(unchecked((int)relocation.FileDelta)),
            Math.Abs(unchecked((int)relocation.RankDelta))
        );

        var homeSteps = tileSteps * 2;

        var x = (long)location.ToHomeCoordinates.X;
        var y = (long)location.ToHomeCoordinates.Y;

        for (var i = 1; i < homeSteps; i++)
        {
            x += stepX;
            y += stepY;

            var point = new HomeCoordinates((ulong)x, (ulong)y);
            if (occupationMap.IsOccupied(point)) return false;
        }

        return true;
    }

    public bool MoveIsPossibleThroughSpace(PieceLocation location, PieceDislodgement dislodgement)
    {
        return MoveIsPossibleThroughSpace(location, dislodgement.Relocation);
        // Yes, if path is horse-like or no occupation is in the way
    }



    // // // // // // // // // // PIECE ADVANCEMENT RULES  // // // // // // // // // //



    public bool PieceIsInformedAboutRelocation(PieceLocation location, PieceRelocation relocation)
    {
        return populationMap.ReadPiece(location.ToHomeCoordinates).Advances.Contains(relocation);
        // Yes, if piece on given location contains given relocation
    }

    public bool PieceIsAbleToAdvanceToLocation(PieceLocation location)
    {
        return !occupationMap.IsOccupied(location.ToHomeCoordinates);
        // Yes, if given location is not occupied
    }



    // // // // // // // // // // PIECE CAPTUREMENT RULES  // // // // // // // // // //



    public bool PieceIsInformedAboutDislodgement(PieceLocation location, PieceDislodgement dislodgement)
    {
        return populationMap.ReadPiece(location.ToHomeCoordinates).Captures.Contains(dislodgement);
        // Yes, if piece on given location contains given dislodgement
    }

    public bool PieceIsAbleToCaptureOnLocation(PieceLocation location)
    {
        return populationMap.IsPieceAdded(location.ToHomeCoordinates);
        // Yes, if any piece is added to given location
    }

    public bool PieceIsWillingToCaptureByDislodgement(PieceLocation location, PieceDislodgement dislodgement)
    {
        var feud    = dislodgement.Feud;
        var teamOne = PieceReadFromCoordinates(location.ToHomeCoordinates).Team;
        var teamTwo = PieceReadFromCoordinates(dislodgement.Relocation.ApplyTo(location).ToHomeCoordinates).Team;

        return feud.IsTeamOneHostileToTeamTwo(teamOne, teamTwo);
    }



    // // // // // // // // // // PIECE ADVANCEMENT COMMAND  // // // // // // // // //



    public void PieceAdvanceFromLocationByRelocation(PieceLocation oldLocation, PieceRelocation relocation)
    {
        var newLocation = relocation.ApplyTo(oldLocation);

        if (!MoveIsPossibleFromLocation(oldLocation)) throw new RuleBrokenException();
        if (!MoveIsChangingPiecePosition(relocation)) throw new RuleBrokenException();

        if (!PieceIsInformedAboutRelocation(oldLocation, relocation)) throw new RuleBrokenException();
        if (!PieceIsAbleToAdvanceToLocation(newLocation)) throw new RuleBrokenException();

        if (!MoveIsPossibleThroughSpace(oldLocation, relocation)) throw new RuleBrokenException();

        var oldCoordinates = oldLocation.ToHomeCoordinates;
        var newCoordinates = newLocation.ToHomeCoordinates;

        PieceMoveFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
    }



    // // // // // // // // // // PIECE  CAPTUREMENT  COMMAND // // // // // // // // //



    public void PieceCaptureFromLocationByDislodgement(PieceLocation oldLocation, PieceDislodgement dislodgement)
    {
        var newLocation = dislodgement.Relocation.ApplyTo(oldLocation);

        if (!MoveIsPossibleFromLocation(oldLocation)) throw new RuleBrokenException();
        if (!MoveIsChangingPiecePosition(dislodgement)) throw new RuleBrokenException();

        if (!PieceIsInformedAboutDislodgement(oldLocation, dislodgement)) throw new RuleBrokenException();
        if (!PieceIsAbleToCaptureOnLocation(newLocation)) throw new RuleBrokenException();
        if (!PieceIsWillingToCaptureByDislodgement(oldLocation, dislodgement)) throw new RuleBrokenException();

        if (!MoveIsPossibleThroughSpace(oldLocation, dislodgement)) throw new RuleBrokenException();

        var oldCoordinates = oldLocation.ToHomeCoordinates;
        var newCoordinates = newLocation.ToHomeCoordinates;

        PieceReplaceFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
    }



}