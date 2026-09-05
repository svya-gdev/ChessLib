namespace Chess.Domain;

public sealed class Piece(Team team, PieceDevelopment development)
{
    public readonly Guid Guid = Guid.NewGuid();
    public readonly Team Team = team;

    public readonly HashSet<PieceRelocation>   Advances = development.Advances.ToHashSet();
    public readonly HashSet<PieceDislodgement> Captures = development.Captures.ToHashSet();
}