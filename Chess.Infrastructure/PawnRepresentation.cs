using Chess.Domain;

namespace Chess.Infrastructure;



public sealed class MapCollisionException() : Exception();
public sealed class MapAbsenceException() : Exception();



public sealed class PawnRepresentation
{
    private OccupationMap? currentMap = null;
    private PawnLocation currentLocation;

    public void Add(OccupationMap map, PawnLocation location)
    {
        if (currentMap is not null) throw new MapCollisionException();

        map.AddOccupationToCoordinates(location.HomeCoordinates);

        currentMap = map;
        currentLocation = location;
    }

    public void Remove()
    {
        if (currentMap is null) throw new MapAbsenceException();

        currentMap.RemoveOccupationFromCoordinates(currentLocation.HomeCoordinates);

        currentMap = null;
    }

    public void Move(PawnDislocation dislocation)
    {
        if (currentMap is null) throw new MapAbsenceException();

        var nextLocation = dislocation.ApplyTo(currentLocation);
        currentMap.MoveOccupationFromCoordinatesToCoordinates(currentLocation.HomeCoordinates, nextLocation.HomeCoordinates);

        currentLocation = nextLocation;
    }
}