using Chess.Domain;

namespace Chess.Infrastructure;


public sealed class CoordinatesAlreadyOccupiedException() : Exception();
public sealed class CoordinatesAlreadyUnoccupiedException() : Exception();
public sealed class PawnAlreadyAddedException() : Exception();
public sealed class PawnAlreadyRemovedException() : Exception();



public sealed class Board
{
    private readonly OccupationMap occupationMap = new();
    private readonly PopulationMap populationMap = new();
    private readonly DoorMap doorMap = new();



    public void WallAddToCoordinates(WallCoordinates wallCoordinates)
    {
        var homeCoordinates = wallCoordinates.ToHomeCoordinates();
        if (occupationMap.IsOccupied(homeCoordinates)) throw new CoordinatesAlreadyOccupiedException();
        occupationMap.AddOccupation(homeCoordinates);
    }

    public void WallRemoveFromCoordinates(WallCoordinates wallCoordinates)
    {
        var homeCoordinates = wallCoordinates.ToHomeCoordinates();
        if (!occupationMap.IsOccupied(homeCoordinates)) throw new CoordinatesAlreadyUnoccupiedException();
        occupationMap.RemoveOccupation(homeCoordinates);
    }

    public void BlockAddToCoordinates(TileCoordinates tileCoordinates)
    {
        var homeCoordinates = tileCoordinates.ToHomeCoordinates();
        if (occupationMap.IsOccupied(homeCoordinates)) throw new CoordinatesAlreadyOccupiedException();
        occupationMap.AddOccupation(homeCoordinates);
    }

    public void BlockRemoveFromCoordinates(TileCoordinates tileCoordinates)
    {
        var homeCoordinates = tileCoordinates.ToHomeCoordinates();
        if (!occupationMap.IsOccupied(homeCoordinates)) throw new CoordinatesAlreadyUnoccupiedException();
        occupationMap.RemoveOccupation(homeCoordinates);
    }



    public void PawnAddToLocation(Pawn pawn, PawnLocation location)
    {
        var homeCoordinates = location.HomeCoordinates;

        if (populationMap.IsPawnAdded(pawn)) throw new PawnAlreadyAddedException();
        if (occupationMap.IsOccupied(homeCoordinates)) throw new CoordinatesAlreadyOccupiedException();

        occupationMap.AddOccupation(homeCoordinates);
        populationMap.AddPawn(pawn, homeCoordinates);
    }

    public void PawnRemoveFromLocation(PawnLocation location)
    {
        var homeCoordinates = location.HomeCoordinates;

        if (!populationMap.IsPawnAdded(homeCoordinates)) throw new PawnAlreadyRemovedException();

        occupationMap.RemoveOccupation(homeCoordinates);
        populationMap.RemovePawn(homeCoordinates);
    }

    public void PawnReplaceOnLocation(Pawn pawn, PawnLocation location)
    {
        var homeCoordinates = location.HomeCoordinates;

        if (populationMap.IsPawnAdded(pawn)) throw new PawnAlreadyAddedException();
        if (!populationMap.IsPawnAdded(location.HomeCoordinates)) throw new PawnAlreadyRemovedException();

        occupationMap.RemoveOccupation(homeCoordinates);
        populationMap.RemovePawn(homeCoordinates);
        occupationMap.AddOccupation(homeCoordinates);
        populationMap.AddPawn(pawn, homeCoordinates);
    }



    public Pawn PawnReadFromLocation(PawnLocation location)
    {
        var homeCoordinates = location.HomeCoordinates;

        if (!populationMap.IsPawnAdded(homeCoordinates)) throw new PawnAlreadyRemovedException();

        return populationMap.ReadPawn(homeCoordinates);
    }

    public Pawn PawnGetFromLocation(PawnLocation location)
    {
        var homeCoordinates = location.HomeCoordinates;

        if (!populationMap.IsPawnAdded(homeCoordinates)) throw new PawnAlreadyRemovedException();

        var result = populationMap.ReadPawn(homeCoordinates);
        occupationMap.RemoveOccupation(homeCoordinates);
        populationMap.RemovePawn(homeCoordinates);
        return result;
    }



    public void PawnAdvanceFromLocation(PawnLocation location, PawnDislocation dislocation)
    {
        var oldCoordinates = location.HomeCoordinates;
        var newCoordinates = dislocation.ApplyTo(location).HomeCoordinates;

        if (!populationMap.IsPawnAdded(oldCoordinates)) throw new PawnAlreadyRemovedException();
        if (occupationMap.IsOccupied(newCoordinates)) throw new CoordinatesAlreadyOccupiedException();

        var pawn = populationMap.ReadPawn(oldCoordinates);

        occupationMap.RemoveOccupation(oldCoordinates);
        populationMap.RemovePawn(oldCoordinates);
        occupationMap.AddOccupation(newCoordinates);
        populationMap.AddPawn(pawn, newCoordinates);
    }

    public void PawnCaptureFromLocation(PawnLocation location, PawnDislocation dislocation)
    {
        var oldCoordinates = location.HomeCoordinates;
        var newCoordinates = dislocation.ApplyTo(location).HomeCoordinates;

        if (!populationMap.IsPawnAdded(oldCoordinates)) throw new PawnAlreadyRemovedException();
        if (!populationMap.IsPawnAdded(newCoordinates)) throw new PawnAlreadyRemovedException();

        var pawn = populationMap.ReadPawn(oldCoordinates);

        occupationMap.RemoveOccupation(oldCoordinates);
        populationMap.RemovePawn(oldCoordinates);
        occupationMap.RemoveOccupation(newCoordinates);
        populationMap.RemovePawn(newCoordinates);
        occupationMap.AddOccupation(newCoordinates);
        populationMap.AddPawn(pawn, newCoordinates);
    }
}