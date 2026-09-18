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



    /// <summary>Checks whether the <paramref name="piece"/> can be added to <see langword="this"/> <see cref="Board"/>.</summary>
    /// <param name="piece">A <see cref="Piece"/> whose <see cref="Piece.Guid"/> should be checked.</param>
    /// <returns>Returns <see langword="true"/> if the <paramref name="piece"/>'s <see cref="Piece.Guid"/> has not been added yet; otherwise, <see langword="false"/>.</returns>
    public bool CanAddPiece(Piece piece)
    {
        return !populationMap.IsPieceAdded(piece);
    }

    /// <summary>Checks whether a <see cref="Piece"/> can be added to the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.</summary>
    /// <param name="location">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to check.</param>
    /// <returns><see langword="true"/> if the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has not been occupied; otherwise, <see langword="false"/>.</returns>
    public bool CanAddPiece(PieceLocation location)
    {
        return !occupationMap.IsOccupied(location.ToHomeCoordinates);
    }

    /// <summary>Adds the <paramref name="piece"/> to the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.</summary>
    /// <param name="piece">A <see cref="Piece"/> to add.</param>
    /// <param name="location">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to add to.</param>
    /// <exception cref="IDDuplicationException">Thrown when the <paramref name="piece"/>'s <see cref="Piece.Guid"/> has already been added.</exception>
    /// <exception cref="PieceAdditionException">Thrown when the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has already been occupied.</exception>
    public void AddPiece(Piece piece, PieceLocation location)
    {
        if(!CanAddPiece(piece))    throw new IDDuplicationException();
        if(!CanAddPiece(location)) throw new PieceAdditionException();

        AddPiece(location.ToHomeCoordinates, piece);
    }



    /// <summary>Checks whether a <see cref="Piece"/> can be removed from the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.</summary>
    /// <param name="location">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to check.</param>
    /// <returns><see langword="true"/> if the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has been occupied by a <see cref="Piece"/>; otherwise, <see langword="false"/>.</returns>
    public bool CanDeletePiece(PieceLocation location)
    {
        return populationMap.IsPieceAdded(location.ToHomeCoordinates);
    }

    /// <summary>Removes a <see cref="Piece"/> from the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.</summary>
    /// <param name="location">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to remove from.</param>
    /// <exception cref="PieceDeletionException">Thrown when the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has not been occupied by a <see cref="Piece"/>.</exception>
    public void DeletePiece(PieceLocation location)
    {
        if (!CanDeletePiece(location)) throw new PieceDeletionException();

        DelPiece(location.ToHomeCoordinates);
    }



    /// <summary>Checks whether a <see cref="Piece"/> can be read from the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.</summary>
    /// <param name="location">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to check.</param>
    /// <returns><see langword="true"/> if the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has been occupied by a <see cref="Piece"/>; otherwise, <see langword="false"/>.</returns>
    public bool CanReadPiece(PieceLocation location)
    {
        return populationMap.IsPieceAdded(location.ToHomeCoordinates);
    }

    /// <summary>Reads a <see cref="Piece"/> from the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.</summary>
    /// <param name="location">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to read from.</param>
    /// <returns>A <see cref="Piece"/> that occupies the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/>.</returns>
    /// <exception cref="PieceReadmentException">Thrown when the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has not been occupied by a <see cref="Piece"/>.</exception>
    public Piece ReadPiece(PieceLocation location)
    {
        if (!CanReadPiece(location)) throw new PieceReadmentException();

        return SeePiece(location.ToHomeCoordinates);
    }



    /// <summary>Checks whether a <see cref="Piece"/> can be moved from the <paramref name="location"/> <see langword="this"/> <see cref="Board"/>.</summary>
    /// <param name="location">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to check.</param>
    /// <returns><see langword="true"/> if the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has been occupied by a <see cref="Piece"/>; otherwise, <see langword="false"/>.</returns>
    public bool CanMoveFrom(PieceLocation location)
    {
        return populationMap.IsPieceAdded(location.ToHomeCoordinates);
    }

    /// <summary>Checks whether a <see cref="Piece"/> on <see langword="this"/> <see cref="Board"/> can move from the <paramref name="location"/> by the <paramref name="relocation"/>.</summary>
    /// <param name="location">A starting <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to check.</param>
    /// <param name="relocation">A <see cref="PieceRelocation"/> to check.</param>
    /// <returns><see langword="true"/> if the <paramref name="relocation"/> is horse-like or no occupation in the way; otherwise, <see langword="false"/>.</returns>
    public bool CanMoveThrough(PieceLocation location, PieceRelocation relocation)
    {
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

    /// <summary>Checks whether a <see cref="Piece"/> on <see langword="this"/> <see cref="Board"/> can move from the <paramref name="location"/> by the <paramref name="dislodgement"/>.</summary>
    /// <param name="location">A starting <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to check.</param>
    /// <param name="dislodgement">A <see cref="PieceDislodgement"/> whose <see cref="PieceRelocation"/> to check.</param>
    /// <returns>c the <paramref name="dislodgement"/>'s <paramref name="PieceRelocation"/> is horse-like or no occupation in the way; otherwise, <see langword="false"/>.</returns>
    public bool CanMoveThrough(PieceLocation location, PieceDislodgement dislodgement)
    {
        return CanMoveThrough(location, dislodgement.Relocation);
    }

    /// <summary>Checks whether a <see cref="Piece"/> on <see langword="this"/> <see cref="Board"/> can advance to the <paramref name="location"/>.</summary>
    /// <param name="location">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to check.</param>
    /// <returns><see langword="true"/> if the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has not been occupied; otherwise, <see langword="false"/>.</returns>
    public bool CanAdvanceTo(PieceLocation location)
    {
        return !occupationMap.IsOccupied(location.ToHomeCoordinates);
    }

    /// <summary>Checks whether a <see cref="Piece"/> on <see langword="this"/> <see cref="Board"/> can capture on the <paramref name="location"/>.</summary>
    /// <param name="location">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> to check.</param>
    /// <returns><see langword="true"/> if the <paramref name="location"/> on <see langword="this"/> <see cref="Board"/> has been occupied by a <see cref="Piece"/>; otherwise, <see langword="false"/>.</returns>
    public bool CanCaptureOn(PieceLocation location)
    {
        return populationMap.IsPieceAdded(location.ToHomeCoordinates);
    }

    /// <summary>Advances a <see cref="Piece"/> on <see langword="this"/> <see cref="Board"/> from the <paramref name="oldLocation"/> by the <paramref name="relocation"/>.</summary>
    /// <param name="oldLocation">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> with a moving <see cref="Piece"/>.</param>
    /// <param name="relocation">A <see cref="PieceRelocation"/> to apply.</param>
    /// <exception cref="NoMoveToApplyException">Thrown when the <paramref name="relocation"/> is non-moving.</exception>
    /// <exception cref="NoPieceToMoveException">Thrown when the <paramref name="oldLocation"/> has not been occupied by a <see cref="Piece"/>.</exception>
    /// <exception cref="CannotAdvanceException"></exception>
    /// <exception cref="AdvanceUntoldException"></exception>
    /// <exception cref="NoSpaceToMoveException"></exception>
    public void Advance(PieceLocation oldLocation, PieceRelocation relocation)
    {
        if (relocation.IsNonMoving)                   throw new NoMoveToApplyException();

        if (!CanMoveFrom(oldLocation))                throw new NoPieceToMoveException();
        var newLocation = relocation.ApplyTo(oldLocation);
        if (!CanAdvanceTo(newLocation))               throw new CannotAdvanceException();

        var piece = populationMap.ReadPiece(oldLocation.ToHomeCoordinates);
        if (!piece.ContainsAdvance(relocation))       throw new AdvanceUntoldException();
        
        if (!CanMoveThrough(oldLocation, relocation)) throw new NoSpaceToMoveException();

        var oldCoordinates = oldLocation.ToHomeCoordinates;
        var newCoordinates = newLocation.ToHomeCoordinates;
        MovePiece(oldCoordinates, newCoordinates);
    }

    /// <summary>Captures a <see cref="Piece"/> on <see langword="this"/> <see cref="Board"/> from the <paramref name="oldLocation"/> by the <paramref name="dislodgement"/>.</summary>
    /// <param name="oldLocation">A <see cref="PieceLocation"/> on <see langword="this"/> <see cref="Board"/> with an attacking <see cref="Piece"/>.</param>
    /// <param name="dislodgement">A <see cref="PieceDislodgement"/> to apply.</param>
    /// <exception cref="NoMoveToApplyException">Thrown when the <paramref name="dislodgement"/>'s <see cref="PieceDislodgement.Relocation"/> is non-moving.</exception>
    /// <exception cref="NoPieceToMoveException">Thrown when the <paramref name="oldLocation"/> has not been occupied by a <see cref="Piece"/>.</exception>
    /// <exception cref="NoneToCaptureException"></exception>
    /// <exception cref="CaptureUntoldException"></exception>
    /// <exception cref="WillNotAttackException"></exception>
    /// <exception cref="NoSpaceToMoveException"></exception>
    public void Capture(PieceLocation oldLocation, PieceDislodgement dislodgement)
    {
        if (dislodgement.Relocation.IsNonMoving)              throw new NoMoveToApplyException();

        if (!CanMoveFrom(oldLocation))                        throw new NoPieceToMoveException();
        var newLocation = dislodgement.Relocation.ApplyTo(oldLocation);
        if (!CanCaptureOn(newLocation))                       throw new NoneToCaptureException();

        var piece = populationMap.ReadPiece(oldLocation.ToHomeCoordinates);
        if (!piece.ContainsCapture(dislodgement))             throw new CaptureUntoldException();
        
        var feud    = dislodgement.Feud;
        var teamOne = piece.Team;
        var teamTwo = SeePiece(newLocation.ToHomeCoordinates).Team;
        if (feud.IsTeamOneHostileToTeamTwo(teamOne, teamTwo)) throw new WillNotAttackException();

        if (!CanMoveThrough(oldLocation, dislodgement))       throw new NoSpaceToMoveException();

        var oldCoordinates = oldLocation.ToHomeCoordinates;
        var newCoordinates = newLocation.ToHomeCoordinates;
        PushPiece(oldCoordinates, newCoordinates);
    }
}