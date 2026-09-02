using Chess.Domain;

namespace Chess.Infrastructure;


public sealed class LocationAlreadyOccupiedException() : Exception();
public sealed class LocationAlreadyUnoccupiedException() : Exception();
public sealed class PawnAlreadyAddedException() : Exception();
public sealed class PawnAlreadyRemovedException() : Exception();



public sealed class Board
{
    private readonly OccupationMap occupationMap = new();
    private readonly PawnMap pawnMap = new();
    private readonly DoorMap doorMap = new();



    public void AddWallToCoordinates(WallCoordinates coordinates)
    {
        occupationMap.AddOccupationToCoordinates(coordinates.ToHomeCoordinates());
    }

    public void RemoveWallFromCoordinates(WallCoordinates coordinates)
    {
        occupationMap.RemoveOccupationFromCoordinates(coordinates.ToHomeCoordinates());
    }

    public void AddBlockToCoordinates(TileCoordinates coordinates)
    {
        occupationMap.AddOccupationToCoordinates(coordinates.ToHomeCoordinates());
    }

    public void RemoveBlockFromCoordinates(TileCoordinates coordinates)
    {
        occupationMap.RemoveOccupationFromCoordinates(coordinates.ToHomeCoordinates());
    }



    public void PawnAddToLocation(Pawn pawn, PawnLocation location)
    {
        if (pawnMap.IsPawnAdded(pawn)) throw new PawnAlreadyAddedException();
        if (occupationMap.IsCoordinatesOccupied(location.HomeCoordinates)) throw new LocationAlreadyOccupiedException();

        pawnMap.AddPawnToCoordinates(pawn, location.HomeCoordinates);
        occupationMap.AddOccupationToCoordinates(location.HomeCoordinates);
    }

    public void PawnRemoveFromLocation(PawnLocation location)
    {
        if (!occupationMap.IsCoordinatesOccupied(location.HomeCoordinates)) throw new LocationAlreadyUnoccupiedException();

        pawnMap.RemovePawnFromCoordinates(location.HomeCoordinates);
        occupationMap.RemoveOccupationFromCoordinates(location.HomeCoordinates);
    }



    public void PawnAdvanceFromLocationWithStraightPath(PawnLocation location, StraightPath path)
    {
        var oldCoordinates = location.HomeCoordinates;
        var newCoordinates = path.ToPawnDislocation().ApplyTo(location).HomeCoordinates;

        if (!occupationMap.IsCoordinatesOccupied(oldCoordinates)) throw new PawnAlreadyRemovedException();
        if (occupationMap.IsCoordinatesOccupied(newCoordinates)) throw new PawnAlreadyAddedException();

        // if is path occupied

        occupationMap.MoveOccupationFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
        pawnMap.MovePawnFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
    }

    public void PawnAdvanceFromLocationWithHorseLikePath(PawnLocation location, HorseLikePath path)
    {
        var oldCoordinates = location.HomeCoordinates;
        var newCoordinates = path.ToPawnDislocation().ApplyTo(location).HomeCoordinates;

        if (!occupationMap.IsCoordinatesOccupied(oldCoordinates)) throw new PawnAlreadyRemovedException();
        if (occupationMap.IsCoordinatesOccupied(newCoordinates)) throw new PawnAlreadyAddedException();

        occupationMap.MoveOccupationFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
        pawnMap.MovePawnFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
    }

    public void PawnCaptureFromLocationWithStraightPath(PawnLocation location, StraightPath path)
    {
        var oldCoordinates = location.HomeCoordinates;
        var newCoordinates = path.ToPawnDislocation().ApplyTo(location).HomeCoordinates;

        if (!occupationMap.IsCoordinatesOccupied(oldCoordinates)) throw new PawnAlreadyRemovedException();
        if (!occupationMap.IsCoordinatesOccupied(newCoordinates)) throw new PawnAlreadyRemovedException();

        // if is path occupied
        // if is pawn from the same team

        occupationMap.RemoveOccupationFromCoordinates(newCoordinates);
        pawnMap.RemovePawnFromCoordinates(newCoordinates);
        occupationMap.MoveOccupationFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
        pawnMap.MovePawnFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
    }

    public void PawnCaptureFromLocationWithHorseLikePath(PawnLocation location, HorseLikePath path)
    {
        var oldCoordinates = location.HomeCoordinates;
        var newCoordinates = path.ToPawnDislocation().ApplyTo(location).HomeCoordinates;

        if (!occupationMap.IsCoordinatesOccupied(oldCoordinates)) throw new PawnAlreadyRemovedException();
        if (!occupationMap.IsCoordinatesOccupied(newCoordinates)) throw new PawnAlreadyRemovedException();

        // if is pawn from the same team

        occupationMap.RemoveOccupationFromCoordinates(newCoordinates);
        pawnMap.RemovePawnFromCoordinates(newCoordinates);
        occupationMap.MoveOccupationFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
        pawnMap.MovePawnFromCoordinatesToCoordinates(oldCoordinates, newCoordinates);
    }
}