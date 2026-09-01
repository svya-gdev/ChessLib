using Chess.Domain;

namespace Chess.Infrastructure;



public sealed class PawnCollisionException() : Exception();
public sealed class PawnAbsenceException() : Exception();



public sealed class PopulationMap(OccupationMap occupationMap)
{
    private OccupationMap occupationMap = occupationMap;
    private Dictionary<Pawn, PawnRepresentation> pawns = [];

    public void AddPawn(Pawn pawn, PawnLocation location)
    {
        if (pawns.ContainsKey(pawn)) throw new PawnCollisionException();

        var representation = new PawnRepresentation();
        representation.Add(occupationMap, location);

        pawns.Add(pawn, representation);
    }

    public void RemovePawn(Pawn pawn)
    {
        if (!pawns.Remove(pawn, out var representation)) throw new PawnAbsenceException();
        representation.Remove();
    }

    public void MovePawn(Pawn pawn, PawnDislocation dislocation)
    {
        if (!pawns.TryGetValue(pawn, out var representation)) throw new PawnAbsenceException();
        representation.Move(dislocation);
    }
}