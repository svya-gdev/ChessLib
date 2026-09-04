using Chess.Domain;

namespace Chess.Infrastructure;

public sealed class Pawn(Team team)
{
    public readonly Guid Guid = Guid.NewGuid();
    public readonly Team Team = team;

    public readonly HashSet<PawnDislocation>  Advances = [];
    public readonly HashSet<PawnDislodgement> Captures = [];
}