using Chess.Domain;

namespace Chess.Infrastructure;



public sealed class OccupationCollisionException(HomeCoordinates coordinates) 
    : Exception($"{coordinates} are occupied.");

public sealed class OccupationAbsenceException(HomeCoordinates coordinates)
	: Exception($"{coordinates} are not occupied.");



internal sealed class OccupationMap
{
    private readonly HashSet<HomeCoordinates> occupation = [];
    
    public void AddOccupation(HomeCoordinates coordinates)
    {
        if (!occupation.Add(coordinates)) throw new OccupationCollisionException(coordinates);
    }
    
    public void RemoveOccupation(HomeCoordinates coordinates)
    {
        if (!occupation.Remove(coordinates)) throw new OccupationAbsenceException(coordinates);
    }
    
    public bool IsOccupied(HomeCoordinates coordinates)
    {
        return occupation.Contains(coordinates);
    }
}