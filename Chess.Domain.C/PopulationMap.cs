namespace Chess.Domain;



internal sealed class PawnRepetitionException() : Exception();
internal sealed class PawnCollisionException() : Exception();
internal sealed class PawnAbsenceException() : Exception();



internal sealed class PopulationMap
{
    private readonly Dictionary<HomeCoordinates, Pawn> pawns = [];
    private readonly HashSet<Guid> guids = [];

    internal void AddPawn(Pawn pawn, HomeCoordinates coordinates)
    {
        if (guids.Contains(pawn.Guid)) throw new PawnRepetitionException();
        if (!pawns.TryAdd(coordinates, pawn)) throw new PawnCollisionException();

        guids.Add(pawn.Guid);
    }

    internal void RemovePawn(HomeCoordinates coordinates)
    {
        if (!pawns.Remove(coordinates, out var pawn)) throw new PawnAbsenceException();

        guids.Remove(pawn.Guid);
    }

    internal Pawn ReadPawn(HomeCoordinates coordinates)
    {
        if (!pawns.TryGetValue(coordinates, out var pawn)) throw new PawnAbsenceException();

        return pawn;
    }

    internal bool IsPawnAdded(HomeCoordinates coordinates)
    {
        return pawns.ContainsKey(coordinates);
    }

    internal bool IsPawnAdded(Pawn pawn)
    {
        return guids.Contains(pawn.Guid);
    }
}