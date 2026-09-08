namespace Chess.Domain;

public sealed class Piece(PieceMindset mindset, PieceDevelopment development)
{
    public readonly Guid Guid = Guid.NewGuid();
    public readonly PieceMindset Mindset = mindset;

    public readonly HashSet<PieceRelocation>   Advances = development.Advances.ToHashSet();
    public readonly HashSet<PieceDislodgement> Captures = development.Captures.ToHashSet();
}