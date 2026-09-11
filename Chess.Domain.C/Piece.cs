namespace Chess.Domain;

public sealed class Piece(PieceDevelopment development)
{
    public readonly Guid Guid = Guid.NewGuid();

    public readonly HashSet<PieceRelocation>   Advances = development.Advances.ToHashSet();
    public readonly HashSet<PieceDislodgement> Captures = development.Captures.ToHashSet();
}