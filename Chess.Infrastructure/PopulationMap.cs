using Chess.Domain;

namespace Chess.Infrastructure;



public sealed class PawnRepetitionException() : Exception();
public sealed class PawnCollisionException() : Exception();
public sealed class PawnAbsenceException() : Exception();



internal sealed class PopulationMap
{
    private readonly Dictionary<HomeCoordinates, Pawn> pawns = [];
    private readonly HashSet<Guid> guids = [];

    public void AddPawn(Pawn pawn, HomeCoordinates coordinates)
    {
        if (guids.Contains(pawn.Guid)) throw new PawnRepetitionException();
        if (!pawns.TryAdd(coordinates, pawn)) throw new PawnCollisionException();

        guids.Add(pawn.Guid);
    }

    public void RemovePawn(HomeCoordinates coordinates)
    {
        if (!pawns.Remove(coordinates, out var pawn)) throw new PawnAbsenceException();

        guids.Remove(pawn.Guid);
    }

    public Pawn ReadPawn(HomeCoordinates coordinates)
    {
        if (!pawns.TryGetValue(coordinates, out var pawn)) throw new PawnAbsenceException();

        return pawn;
    }

    public bool IsPawnAdded(HomeCoordinates coordinates)
    {
        return pawns.ContainsKey(coordinates);
    }

    public bool IsPawnAdded(Pawn pawn)
    {
        return guids.Contains(pawn.Guid);
    }
}