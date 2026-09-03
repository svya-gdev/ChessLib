using Chess.Domain;

namespace Chess.Infrastructure;



public sealed class CoordinatesAlreadyOccupiedException() : Exception();
public sealed class CoordinatesAlreadyUnoccupiedException() : Exception();



public sealed class RuleBrokenException() : Exception(); // WIP



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


    

      






    

    // // // // // // // // // // PAWN PLACEMENT RULES // // // // // // // // // //



    public bool IsAbleToAddPawnToLocation(Pawn pawn, PawnLocation location)
        => !populationMap.IsPawnAdded(pawn) && !occupationMap.IsOccupied(location.HomeCoordinates);
    // Yes, if pawn is not added and location is not occupied

    public bool IsLocationOccupiedByPawn(PawnLocation location)
        => populationMap.IsPawnAdded(location.HomeCoordinates);
    // Yes, if pawn is added

    public bool IsAbleToRemovePawnFromLocation(PawnLocation location)
        => IsLocationOccupiedByPawn(location);
    // Yes, if location is occupied by a pawn



    // // // // // // // // // // PAWN PLACEMENT COMMANDS // // // // // // // // // //



    public void PawnAddToLocation(Pawn pawn, PawnLocation location)
    {
        if (!IsAbleToAddPawnToLocation(pawn, location)) throw new RuleBrokenException();

        var homeCoordinates = location.HomeCoordinates;

        occupationMap.AddOccupation(homeCoordinates);
        populationMap.AddPawn(pawn, homeCoordinates);
    }

    public void PawnRemoveFromLocation(PawnLocation location)
    {
        if (!IsAbleToRemovePawnFromLocation(location)) throw new RuleBrokenException();

        var homeCoordinates = location.HomeCoordinates;

        occupationMap.RemoveOccupation(homeCoordinates);
        populationMap.RemovePawn(homeCoordinates);
    }



    // // // // // // // // // // PAWN READ RULE // // // // // // // // // //



    public bool IsAbleToReadPawnFromLocation(PawnLocation location)
        => IsLocationOccupiedByPawn(location);
    // Yes, if location is occupied by a pawn



    // // // // // // // // // // PAWN READ COMMAND // // // // // // // // // //



    public Pawn PawnReadFromLocation(PawnLocation location)
    {
        if (!IsAbleToReadPawnFromLocation(location)) throw new RuleBrokenException();

        return populationMap.ReadPawn(location.HomeCoordinates);
    }



    // // // // // // // // // // PAWN MOVEMENT RULES // // // // // // // // // //



    public bool IsLocationCapturable(PawnLocation location)
        => IsLocationOccupiedByPawn(location);
    // Yes, if occupied by pawn

    public bool IsLocationAdvancable(PawnLocation location)
        => !occupationMap.IsOccupied(location.HomeCoordinates);
    // Yes, if not occupied

    public bool IsPathWalkable(PawnLocation location, PawnDislocation dislocation)
    // Yes, if path is horse-like or no occupation in the way
    {
        if (dislocation.IsHorseLike) return true;

        var stepX = Math.Sign(unchecked((int)dislocation.Horizontal));
        var stepY = Math.Sign(unchecked((int)dislocation.Vertical));

        var tileSteps = Math.Max(
            Math.Abs(unchecked((int)dislocation.Horizontal)),
            Math.Abs(unchecked((int)dislocation.Vertical))
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



    // // // // // // // // // // PAWN MOVEMENT COMMANDS // // // // // // // // // //



    public void PawnCaptureFromLocation(PawnLocation location, PawnDislocation dislocation)
    {
        var targetLocation = dislocation.ApplyTo(location);

        if (!IsLocationOccupiedByPawn(location)) throw new RuleBrokenException();
        if (!IsLocationCapturable(targetLocation)) throw new RuleBrokenException();
        if (!IsPathWalkable(location, dislocation)) throw new RuleBrokenException();

        var oldCoordinates = location.HomeCoordinates;
        var newCoordinates = targetLocation.HomeCoordinates;
        var pawn = populationMap.ReadPawn(oldCoordinates);

        occupationMap.RemoveOccupation(oldCoordinates);
        populationMap.RemovePawn(oldCoordinates);
        occupationMap.RemoveOccupation(newCoordinates);
        populationMap.RemovePawn(newCoordinates);
        occupationMap.AddOccupation(newCoordinates);
        populationMap.AddPawn(pawn, newCoordinates);
    }

    public void PawnAdvanceFromLocation(PawnLocation location, PawnDislocation dislocation)
    {
        var targetLocation = dislocation.ApplyTo(location);

        if (!IsLocationOccupiedByPawn(location)) throw new RuleBrokenException();
        if (!IsLocationAdvancable(targetLocation)) throw new RuleBrokenException();
        if (!IsPathWalkable(location, dislocation)) throw new RuleBrokenException();

        var oldCoordinates = location.HomeCoordinates;
        var newCoordinates = targetLocation.HomeCoordinates;
        var pawn = populationMap.ReadPawn(oldCoordinates);

        occupationMap.RemoveOccupation(oldCoordinates);
        populationMap.RemovePawn(oldCoordinates);
        occupationMap.AddOccupation(newCoordinates);
        populationMap.AddPawn(pawn, newCoordinates);
    }
}