using Chess.Domain;

namespace Chess.Infrastructure;



public sealed class OccupationCollisionException(HomeCoordinates coordinates) 
    : Exception($"{coordinates} are occupied.");

public sealed class OccupationAbsenceException(HomeCoordinates coordinates)
	: Exception($"{coordinates} are not occupied.");



internal sealed class OccupationMap
{
    private readonly HashSet<HomeCoordinates> occupation = [];
    
    public void AddOccupationToCoordinates(HomeCoordinates coordinates)
    {
        if (!occupation.Add(coordinates)) throw new OccupationCollisionException(coordinates);
    }
    
    public void RemoveOccupationFromCoordinates(HomeCoordinates coordinates)
    {
        if (!occupation.Remove(coordinates)) throw new OccupationAbsenceException(coordinates);
    }
    
    public void MoveOccupationFromCoordinatesToCoordinates(HomeCoordinates oldCoordinates, HomeCoordinates newCoordinates)
	{
	    if (occupation.Contains(newCoordinates)) throw new OccupationCollisionException(newCoordinates);
	    if (!occupation.Remove(oldCoordinates)) throw new OccupationAbsenceException(oldCoordinates);
	    occupation.Add(newCoordinates);
	}
    
    public bool IsCoordinatesOccupied(HomeCoordinates coordinates)
    {
        return occupation.Contains(coordinates);
    }
}