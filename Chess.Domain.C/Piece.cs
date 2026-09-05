namespace Chess.Domain;

public sealed class Piece(Team team)
{
    public readonly Guid Guid = Guid.NewGuid();
    public readonly Team Team = team;

    public readonly HashSet<PieceRelocation>   Advances = [];
    public readonly HashSet<PieceDislodgement> Captures = [];
}