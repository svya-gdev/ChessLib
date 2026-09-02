using Chess.Domain;

namespace Chess.Infrastructure;


public sealed class LocationAlreadyOccupiedException() : Exception();
public sealed class LocationAlreadyUnoccupiedException() : Exception();

public sealed class CoordinatesAlreadyOccupiedException() : Exception();
public sealed class CoordinatesAlreadyUnoccupiedException() : Exception();
public sealed class PawnAlreadyAddedException() : Exception();
public sealed class PawnAlreadyRemovedException() : Exception();



public sealed class Board
{
    private readonly OccupationMap occupationMap = new();
    private readonly PawnMap pawnMap = new();
    private readonly DoorMap doorMap = new();



    public void WallAddToCoordinates(WallCoordinates wallCoordinates)
    {
        var homeCoordinates = wallCoordinates.ToHomeCoordinates();
        if (occupationMap.IsCoordinatesOccupied(homeCoordinates)) throw new CoordinatesAlreadyOccupiedException();
        occupationMap.AddOccupationToCoordinates(homeCoordinates);
    }

    public void WallRemoveFromCoordinates(WallCoordinates wallCoordinates)
    {
        var homeCoordinates = wallCoordinates.ToHomeCoordinates();
        if (!occupationMap.IsCoordinatesOccupied(homeCoordinates)) throw new CoordinatesAlreadyUnoccupiedException();
        occupationMap.RemoveOccupationFromCoordinates(homeCoordinates);
    }

    public void BlockAddToCoordinates(TileCoordinates tileCoordinates)
    {
        var homeCoordinates = tileCoordinates.ToHomeCoordinates();
        if (occupationMap.IsCoordinatesOccupied(homeCoordinates)) throw new CoordinatesAlreadyOccupiedException();
        occupationMap.AddOccupationToCoordinates(homeCoordinates);
    }

    public void BlockRemoveFromCoordinates(TileCoordinates tileCoordinates)
    {
        var homeCoordinates = tileCoordinates.ToHomeCoordinates();
        if (!occupationMap.IsCoordinatesOccupied(homeCoordinates)) throw new CoordinatesAlreadyUnoccupiedException();
        occupationMap.RemoveOccupationFromCoordinates(homeCoordinates);
    }



    public void PawnAddToLocation(Pawn pawn, PawnLocation location)
    {
        if (pawnMap.IsPawnAdded(pawn)) throw new PawnAlreadyAddedException();
        BlockAddToCoordinates(location.TileCoordinates);
        pawnMap.AddPawnToCoordinates(pawn, location.HomeCoordinates);
    }

    public void PawnRemoveFromLocation(PawnLocation location)
    {
        if (pawnMap.IsPawnAddedToCoordinates(location.HomeCoordinates)) throw new PawnAlreadyRemovedException();
        BlockRemoveFromCoordinates(location.TileCoordinates);
        pawnMap.RemovePawnFromCoordinates(location.HomeCoordinates);
    }



    public void PawnReplaceOnLocation(Pawn pawn, PawnLocation location)
    {
        PawnRemoveFromLocation(location);
        PawnAddToLocation(pawn, location);
    }

    public Pawn PawnReadFromLocation(PawnLocation location)
    {
        var homeCoordinates = location.HomeCoordinates;
        if (pawnMap.IsPawnAddedToCoordinates(homeCoordinates)) throw new PawnAlreadyRemovedException();
        return pawnMap.ReadPawnFromCoordinates(homeCoordinates);
    }

    public Pawn PawnGetFromLocation(PawnLocation location)
    {
        var result = PawnReadFromLocation(location);
        PawnRemoveFromLocation(location);
        return result;
    }



    public void PawnAdvanceFromLocation(PawnLocation location, PawnDislocation dislocation)
    {
        // if can walk througth
        var newLocation = dislocation.ApplyTo(location);
        var pawn = PawnReadFromLocation(location);
        PawnAddToLocation(pawn, newLocation);
        PawnRemoveFromLocation(location);
    }

    public void PawnCaptureFromLocation(PawnLocation location, PawnDislocation dislocation)
    {
        // if can walk througth
        var newLocation = dislocation.ApplyTo(location);
        var pawn = PawnReadFromLocation(location);
        PawnReplaceOnLocation(pawn, newLocation);
        PawnRemoveFromLocation(location);
    }
}