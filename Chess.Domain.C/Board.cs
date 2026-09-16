namespace Chess.Domain;

public sealed class Board
{
    private readonly OccupationMap occupationMap = new();
    private readonly PopulationMap populationMap = new();

    private void AddPiece(HomeCoordinates coordinates, Piece piece)
    {
        occupationMap.AddOccupation(coordinates);
        populationMap.AddPiece(piece, coordinates);
    }

    private Piece SeePiece(HomeCoordinates coordinates)
    {
        return populationMap.ReadPiece(coordinates);
    }

    private void DelPiece(HomeCoordinates coordinates)
    {
        occupationMap.RemoveOccupation(coordinates);
        populationMap.RemovePiece(coordinates);
    }

    private Piece GetPiece(HomeCoordinates coordinates)
    {
        var piece = SeePiece(coordinates);
        DelPiece(coordinates);
        return piece;
    }

    private void MovePiece(HomeCoordinates oldCoordinates, HomeCoordinates newCoordinates)
    {
        var piece = GetPiece(oldCoordinates);
        AddPiece(newCoordinates, piece);
    }

    private void PushPiece(HomeCoordinates oldCoordinates, HomeCoordinates newCoordinates)
    {
        DelPiece(newCoordinates);
        MovePiece(oldCoordinates, newCoordinates);
    }



    // // // // // // // // // // PIECE PLACEMENT RULES // // // // // // // // // // //



    /// <summary>
    /// Checks whether the <paramref name="piece"/> can be added to <see langword="this"/> <see cref="Board"/>.
    /// </summary>

    /// <param name="piece">
    /// A <see cref="Piece"/> whose <see cref="Piece.Guid"/> should be checked.
    /// </param>

    /// <returns>
    /// Returns <see langword="true"/> if the <paramref name="piece"/>'s <see cref="Piece.Guid"/> has not been added yet; otherwise, <see langword="false"/>.
    /// </returns>

    public bool CanAddPiece(Piece piece)
    {
        return !populationMap.IsPieceAdded(piece);
    }



    /// <summary>
    /// Checks whether a <see cref="Piece"/> can be added to <see langword="this"/> <see cref="Board"/> on the <paramref name="location"/>.
    /// </summary>

    /// <param name="location">
    /// A <see cref="PieceLocation"/> to check.
    /// </param>

    /// <returns>
    /// <see langword="true"/> if the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has not been occupied; otherwise, <see langword="false"/>.
    /// </returns>

    public bool CanAddPiece(PieceLocation location)
    {
        return !occupationMap.IsOccupied(location.ToHomeCoordinates);
    }



    // // // // // // // // // // PIECE PLACEMENT COMMAND  // // // // // // // // // //



    /// <summary>
    /// Adds the <paramref name="piece"/> to the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.
    /// </summary>

    /// <param name="piece">
    /// A <see cref="Piece"/> to add.
    /// </param>

    /// <param name="location">
    /// A <see cref="PieceLocation"/> to add to.
    /// </param>

    /// <exception cref="PieceAlreadyAddedSomewhereException">
    /// Thrown when the <paramref name="piece"/>'s <see cref="Piece.Guid"/> has already been added.
    /// </exception>

    /// <exception cref="LocationAlreadyOccupiedBySomethingException">
    /// Thrown when the <paramref name="location"/> has already been occupied.
    /// </exception>

    public void AddPiece(Piece piece, PieceLocation location)
    {
        if(!CanAddPiece(piece))    throw new PieceAlreadyAddedSomewhereException();
        if(!CanAddPiece(location)) throw new LocationAlreadyOccupiedBySomethingException();

        AddPiece(location.ToHomeCoordinates, piece);
    }



    // // // // // // // // // // PIECE REMOVAL RULE // // // // // // // // // // // //



    /// <summary>
    /// Checks whether a <see cref="Piece"/> can be removed from the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.
    /// </summary>

    /// <param name="location">
    /// A <see cref="PieceLocation"/> to check.
    /// </param>

    /// <returns>
    /// <see langword="true"/> if the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has been occupied by a <see cref="Piece"/>; otherwise, <see langword="false"/>.
    /// </returns>

    public bool CanRemovePiece(PieceLocation location)
    {
        return populationMap.IsPieceAdded(location.ToHomeCoordinates);
    }



    // // // // // // // // // // PIECE REMOVAL COMMAND // // // // // // // // // // //



    /// <summary>
    /// Removes a <see cref="Piece"/> from the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.
    /// </summary>
    /// 
    /// <param name="location">
    /// A <see cref="PieceLocation"/> to remove from.
    /// </param>
    /// 
    /// <exception cref="LocationAlreadyNotOccupiedByPieceException">
    /// Thrown when the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has not been occupied by a <see cref="Piece"/>.
    /// </exception>

    public void RemovePiece(PieceLocation location)
    {
        if (!CanRemovePiece(location)) throw new LocationAlreadyNotOccupiedByPieceException();

        DelPiece(location.ToHomeCoordinates);
    }



    // // // // // // // // // // PIECE  READMENT  RULE // // // // // // // // // // //



    /// <summary>
    /// Checks whether a <see cref="Piece"/> can be read from the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.
    /// </summary>
    /// 
    /// <param name="location">
    /// A <see cref="PieceLocation"/> to check.
    /// </param>
    /// 
    /// <returns>
    /// <see langword="true"/> if the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has been occupied by a <see cref="Piece"/>; otherwise, <see langword="false"/>.
    /// </returns>

    public bool CanReadPiece(PieceLocation location)
    {
        return populationMap.IsPieceAdded(location.ToHomeCoordinates);
    }



    // // // // // // // // // // PIECE  READMENT  COMMAND // // // // // // // // // //



    /// <summary>
    /// 
    /// </summary>
    /// 
    /// <param name="location">
    /// 
    /// </param>
    /// 
    /// <returns>
    /// 
    /// </returns>
    /// 
    /// <exception cref="PieceNotFoundException">
    /// 
    /// </exception>

    public Piece ReadPiece(PieceLocation location)
    {
        if (!CanReadPiece(location)) throw new PieceNotFoundException();

        return SeePiece(location.ToHomeCoordinates);
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
        var teamOne = SeePiece(location.ToHomeCoordinates).Team;
        var teamTwo = SeePiece(dislodgement.Relocation.ApplyTo(location).ToHomeCoordinates).Team;

        return feud.IsTeamOneHostileToTeamTwo(teamOne, teamTwo);
    }



    // // // // // // // // // // PIECE ADVANCEMENT COMMAND  // // // // // // // // //



    public void PieceAdvanceFromLocationByRelocation(PieceLocation oldLocation, PieceRelocation relocation)
    {
        var newLocation = relocation.ApplyTo(oldLocation);

        if (!MoveIsPossibleFromLocation(oldLocation))                 throw new NoPieceToMoveException();
        if (!MoveIsChangingPiecePosition(relocation))                 throw new NoMoveToApplyException();

        if (!PieceIsInformedAboutRelocation(oldLocation, relocation)) throw new RelocationUnknownException();
        if (!PieceIsAbleToAdvanceToLocation(newLocation))             throw new NewLocationOccupiedBySomethingException();

        if (!MoveIsPossibleThroughSpace(oldLocation, relocation))     throw new NoSpaceToMoveException();

        var oldCoordinates = oldLocation.ToHomeCoordinates;
        var newCoordinates = newLocation.ToHomeCoordinates;

        MovePiece(oldCoordinates, newCoordinates);
    }



    // // // // // // // // // // PIECE  CAPTUREMENT  COMMAND // // // // // // // // //



    public void PieceCaptureFromLocationByDislodgement(PieceLocation oldLocation, PieceDislodgement dislodgement)
    {
        var newLocation = dislodgement.Relocation.ApplyTo(oldLocation);

        if (!MoveIsPossibleFromLocation(oldLocation))                          throw new NoPieceToMoveException();
        if (!MoveIsChangingPiecePosition(dislodgement))                        throw new NoMoveToApplyException();

        if (!PieceIsInformedAboutDislodgement(oldLocation, dislodgement))      throw new DislodgementUnknownException();
        if (!PieceIsAbleToCaptureOnLocation(newLocation))                      throw new NewLocationNotOccupiedByPieceException();
        if (!PieceIsWillingToCaptureByDislodgement(oldLocation, dislodgement)) throw new NoWillToCaptureException();

        if (!MoveIsPossibleThroughSpace(oldLocation, dislodgement))            throw new NoSpaceToMoveException();

        var oldCoordinates = oldLocation.ToHomeCoordinates;
        var newCoordinates = newLocation.ToHomeCoordinates;

        PushPiece(oldCoordinates, newCoordinates);
    }
}