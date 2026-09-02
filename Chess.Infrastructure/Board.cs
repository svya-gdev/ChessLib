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

    public void PawnAdvanceFromLocationWithDislocation(PawnLocation location, PawnDislocation dislocation)
    {
        
    }

    public void PawnCaptureFromLocationWithDislocation(PawnLocation location, PawnDislocation dislocation)
    {
        
    }
}