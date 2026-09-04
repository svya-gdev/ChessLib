namespace Chess.Domain;



public sealed class OccupationCollisionException() : Exception();

public sealed class OccupationAbsenceException() : Exception();



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