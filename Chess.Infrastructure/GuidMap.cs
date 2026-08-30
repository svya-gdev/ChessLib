using Chess.Domain;

namespace Chess.Infrastructure;



public sealed class GuidCollisionException() : Exception();
public sealed class GuidAbsenceException() : Exception();



public sealed class GuidMap
{
    private readonly Dictionary<HomeCoordinates, Guid> guids = [];

    public void AddGuidToCoordinates(Guid guid, HomeCoordinates coordinates)
    {
        if (!guids.TryAdd(coordinates, guid)) throw new GuidCollisionException();
    }

    public void MoveGuidFromCoordinatesToCoordinates(HomeCoordinates oldCoordinates, HomeCoordinates newCoordinates)
    {
        if (guids.ContainsKey(newCoordinates)) throw new GuidCollisionException();
        if (!guids.Remove(oldCoordinates, out var guid)) throw new GuidAbsenceException();

        guids.Add(newCoordinates, guid);
    }

    public void RemoveGuidFromCoordinates(HomeCoordinates coordinates)
    {
        if (!guids.Remove(coordinates)) throw new GuidAbsenceException();
    }

    public Guid ReadGuidFromCoordinates(HomeCoordinates coordinates)
    {
        if (!guids.TryGetValue(coordinates, out var guid)) throw new GuidAbsenceException();
        return guid;
    }
}
