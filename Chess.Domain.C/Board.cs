namespace Chess.Domain;



public sealed class CoordinatesAlreadyOccupiedException() : Exception();
public sealed class CoordinatesAlreadyUnoccupiedException() : Exception();



public sealed class RuleBrokenException() : Exception(); // WIP



public sealed class Board
{
    private readonly OccupationMap occupationMap = new();
    private readonly PopulationMap populationMap = new();
    // private readonly DoorMap doorMap = new();



    internal void WallAddToCoordinates(WallCoordinates wallCoordinates)
    {
        var homeCoordinates = wallCoordinates.ToHomeCoordinates();
        if (occupationMap.IsOccupied(homeCoordinates)) throw new CoordinatesAlreadyOccupiedException();
        occupationMap.AddOccupation(homeCoordinates);
    }

    internal void WallRemoveFromCoordinates(WallCoordinates wallCoordinates)
    {
        var homeCoordinates = wallCoordinates.ToHomeCoordinates();
        if (!occupationMap.IsOccupied(homeCoordinates)) throw new CoordinatesAlreadyUnoccupiedException();
        occupationMap.RemoveOccupation(homeCoordinates);
    }

    internal void BlockAddToCoordinates(TileCoordinates tileCoordinates)
    {
        var homeCoordinates = tileCoordinates.ToHomeCoordinates();
        if (occupationMap.IsOccupied(homeCoordinates)) throw new CoordinatesAlreadyOccupiedException();
        occupationMap.AddOccupation(homeCoordinates);
    }

    internal void BlockRemoveFromCoordinates(TileCoordinates tileCoordinates)
    {
        var homeCoordinates = tileCoordinates.ToHomeCoordinates();
        if (!occupationMap.IsOccupied(homeCoordinates)) throw new CoordinatesAlreadyUnoccupiedException();
        occupationMap.RemoveOccupation(homeCoordinates);
    }


    
    // // // // // // // // // // PIECE PLACEMENT RULES // // // // // // // // // //



    public bool IsAbleToAddPieceToLocation(Piece piece, PieceLocation location)
        => !populationMap.IsPieceAdded(piece) && !occupationMap.IsOccupied(location.HomeCoordinates);
    // Yes, if piece is not added and location is not occupied

    public bool IsLocationOccupiedByPiece(PieceLocation location)
        => populationMap.IsPieceAdded(location.HomeCoordinates);
    // Yes, if piece is added

    public bool IsAbleToRemovePieceFromLocation(PieceLocation location)
        => IsLocationOccupiedByPiece(location);
    // Yes, if location is occupied by a piece



    // // // // // // // // // // PIECE PLACEMENT COMMANDS // // // // // // // // // //



    public void PieceAddToLocation(Piece piece, PieceLocation location)
    {
        if (!IsAbleToAddPieceToLocation(piece, location)) throw new RuleBrokenException();

        var homeCoordinates = location.HomeCoordinates;

        occupationMap.AddOccupation(homeCoordinates);
        populationMap.AddPiece(piece, homeCoordinates);
    }

    public void PieceRemoveFromLocation(PieceLocation location)
    {
        if (!IsAbleToRemovePieceFromLocation(location)) throw new RuleBrokenException();

        var homeCoordinates = location.HomeCoordinates;

        occupationMap.RemoveOccupation(homeCoordinates);
        populationMap.RemovePiece(homeCoordinates);
    }



    // // // // // // // // // // PIECE READ RULE // // // // // // // // // //



    public bool IsAbleToReadPieceFromLocation(PieceLocation location)
        => IsLocationOccupiedByPiece(location);
    // Yes, if location is occupied by a piece



    // // // // // // // // // // PIECE READ COMMAND // // // // // // // // // //



    public Piece PieceReadFromLocation(PieceLocation location)
    {
        if (!IsAbleToReadPieceFromLocation(location)) throw new RuleBrokenException();

        return populationMap.ReadPiece(location.HomeCoordinates);
    }



    // // // // // // // // // // PIECE MOVEMENT RULES // // // // // // // // // //



    public bool IsDislodgementPossible(PieceLocation attackerLocation, PieceDislodgement dislodgement)
    // Yes, if both locations occupied by pieces and spite against attacked piece's team
    {
        if (!IsLocationOccupiedByPiece(attackerLocation)) return false;

        var attackedLocation = dislodgement.Relocation.ApplyTo(attackerLocation);
        if (!IsLocationOccupiedByPiece(attackedLocation)) return false;

        var attackerTeam = populationMap.ReadPiece(attackerLocation.HomeCoordinates).Team;
        var attackedTeam = populationMap.ReadPiece(attackedLocation.HomeCoordinates).Team;
        return dislodgement.Spite.IsFronTeamToTeam(attackerTeam, attackedTeam);
    }

    public bool IsLocationAdvancable(PieceLocation location)
        => !occupationMap.IsOccupied(location.HomeCoordinates);
    // Yes, if not occupied

    public bool IsPathWalkable(PieceLocation location, PieceRelocation dislocation)
    // Yes, if path is horse-like or no occupation in the way
    {
        if (dislocation.IsHorseLike) return true;

        var stepX = Math.Sign(unchecked((int)dislocation.FileDelta));
        var stepY = Math.Sign(unchecked((int)dislocation.RankDelta));

        var tileSteps = Math.Max(
            Math.Abs(unchecked((int)dislocation.FileDelta)),
            Math.Abs(unchecked((int)dislocation.RankDelta))
        );

        var homeSteps = tileSteps * 2;

        var x = (long)location.HomeCoordinates.X;
        var y = (long)location.HomeCoordinates.Y;

        for (var i = 1; i < homeSteps; i++)
        {
            x += stepX;
            y += stepY;

            var point = new HomeCoordinates((ulong)x, (ulong)y);
            if (occupationMap.IsOccupied(point)) return false;
        }

        return true;
    }



    // // // // // // // // // // PIECE MOVEMENT COMMANDS // // // // // // // // // //



    public void PieceCaptureFromLocation(PieceLocation location, PieceDislodgement dislodgement)
    {
        var targetLocation = dislodgement.Relocation.ApplyTo(location);

        if (!IsDislodgementPossible(location, dislodgement)) throw new RuleBrokenException();
        if (!IsPathWalkable(location, dislodgement.Relocation)) throw new RuleBrokenException();

        var oldCoordinates = location.HomeCoordinates;
        var newCoordinates = targetLocation.HomeCoordinates;
        var piece = populationMap.ReadPiece(oldCoordinates);

        occupationMap.RemoveOccupation(oldCoordinates);
        populationMap.RemovePiece(oldCoordinates);
        occupationMap.RemoveOccupation(newCoordinates);
        populationMap.RemovePiece(newCoordinates);
        occupationMap.AddOccupation(newCoordinates);
        populationMap.AddPiece(piece, newCoordinates);
    }

    public void PieceAdvanceFromLocation(PieceLocation location, PieceRelocation dislocation)
    {
        var targetLocation = dislocation.ApplyTo(location);

        if (!IsLocationOccupiedByPiece(location)) throw new RuleBrokenException();
        if (!IsLocationAdvancable(targetLocation)) throw new RuleBrokenException();
        if (!IsPathWalkable(location, dislocation)) throw new RuleBrokenException();

        var oldCoordinates = location.HomeCoordinates;
        var newCoordinates = targetLocation.HomeCoordinates;
        var piece = populationMap.ReadPiece(oldCoordinates);

        occupationMap.RemoveOccupation(oldCoordinates);
        populationMap.RemovePiece(oldCoordinates);
        occupationMap.AddOccupation(newCoordinates);
        populationMap.AddPiece(piece, newCoordinates);
    }
}