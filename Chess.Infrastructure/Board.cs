using Chess.Domain;

namespace Chess.Infrastructure;


public sealed class LocationAlreadyOccupiedException() : Exception();
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
        
    }

    public void PawnRemoveFromLocation(PawnLocation location)
    {
        
    }

    public void PawnAdvanceFromLocationWithDislocation(PawnLocation location, PawnDislocation dislocation)
    {
        
    }

    public void PawnCaptureFromLocationWithDislocation(PawnLocation location, PawnDislocation dislocation)
    {
        
    }
}