using Chess.Domain;

namespace Chess.Infrastructure;


public sealed class PawnRepetitionException() : Exception();
public sealed class PawnCollisionException() : Exception();
public sealed class PawnAbsenceException() : Exception();



internal sealed class PawnMap
{
    private readonly Dictionary<HomeCoordinates, Pawn> pawns = [];
    private readonly HashSet<Guid> guids = [];

    public void AddPawnToCoordinates(Pawn pawn, HomeCoordinates coordinates)
    {
        if (guids.Contains(pawn.Guid)) throw new PawnRepetitionException();
        if (!pawns.TryAdd(coordinates, pawn)) throw new PawnCollisionException();
        guids.Add(pawn.Guid);
    }

    public void RemovePawnFromCoordinates(HomeCoordinates coordinates)
    {
        if (!pawns.Remove(coordinates, out var pawn)) throw new PawnAbsenceException();
        guids.Remove(pawn.Guid);
    }

    public void MovePawnFromCoordinatesToCoordinates(HomeCoordinates oldCoordinates, HomeCoordinates newCoordinates)
    {
        if (pawns.ContainsKey(newCoordinates)) throw new PawnCollisionException();
        if (!pawns.Remove(oldCoordinates, out var pawn)) throw new PawnAbsenceException();

        pawns.Add(newCoordinates, pawn);
    }

    public bool IsPawnAddedToCoordinates(HomeCoordinates coordinates)
    {
        return pawns.ContainsKey(coordinates);
    }

    public bool IsPawnAdded(Pawn pawn)
    {
        return guids.Contains(pawn.Guid);
    }

    public Pawn ReadPawnFromCoordinates(HomeCoordinates coordinates)
    {
        if (!pawns.TryGetValue(coordinates, out var pawn)) throw new PawnAbsenceException();
        return pawn;
    }
}
