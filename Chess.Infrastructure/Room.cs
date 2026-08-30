using Chess.Domain;

namespace Chess.Infrastructure;

public sealed class Room
{
    private ulong occupation;
    
    public bool IsOccupied(byte x, byte y)
    {
        if (x > 7 || y > 7) throw new ArgumentOutOfRangeException(x > 7 ? nameof(x) : nameof(y), "Coordinates must be 0..7.");
        
        return (occupation & (1UL << (y * 8 + x))) != 0UL;
    }
    
    public void Toggle(byte x, byte y)
    {
        if (x > 7 || y > 7) throw new ArgumentOutOfRangeException(x > 7 ? nameof(x) : nameof(y), "Coordinates must be 0..7.");
        
        occupation ^= 1UL << (y * 8 + x);
    }
    
    public void PlaceOn(OccupationMap map, RoomCoordinates coordinates) // Нарушает атомарность
    {
        var origin = coordinates.ToHomeCoordinates();
        
        for (byte y = 0; y < 8; y++)
        {
            for (byte x = 0; x < 8; x++)
            {
                if (IsOccupied(x, y))
                {
                    var home = new HomeCoordinates(origin.X + x, origin.Y + y);
                    map.AddOccupationToCoordinates(home); // Т.к. здесь может выбросить исключение на любой итерации
                }
            }
        }
    }
}
