namespace Chess.Domain;



internal sealed class OccupationCollisionException() : Exception();

internal sealed class OccupationAbsenceException() : Exception();



internal sealed class OccupationMap
{
    private readonly HashSet<HomeCoordinates> occupation = [];
    
    internal void AddOccupation(HomeCoordinates coordinates)
    {
        if (!occupation.Add(coordinates)) throw new OccupationCollisionException();
    }
    
    internal void RemoveOccupation(HomeCoordinates coordinates)
    {
        if (!occupation.Remove(coordinates)) throw new OccupationAbsenceException();
    }
    
    internal bool IsOccupied(HomeCoordinates coordinates)
    {
        return occupation.Contains(coordinates);
    }
}