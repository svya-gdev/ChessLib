namespace Chess.Domain;



internal sealed class PieceRepetitionException() : Exception();
internal sealed class PieceCollisionException() : Exception();
internal sealed class PieceAbsenceException() : Exception();



internal sealed class PopulationMap
{
    private readonly Dictionary<HomeCoordinates, Piece> pieces = [];
    private readonly HashSet<Guid> guids = [];

    internal void AddPiece(Piece piece, HomeCoordinates coordinates)
    {
        if (guids.Contains(piece.Guid)) throw new PieceRepetitionException();
        if (!pieces.TryAdd(coordinates, piece)) throw new PieceCollisionException();

        guids.Add(piece.Guid);
    }

    internal void RemovePiece(HomeCoordinates coordinates)
    {
        if (!pieces.Remove(coordinates, out var piece)) throw new PieceAbsenceException();

        guids.Remove(piece.Guid);
    }

    internal Piece ReadPiece(HomeCoordinates coordinates)
    {
        if (!pieces.TryGetValue(coordinates, out var piece)) throw new PieceAbsenceException();

        return piece;
    }

    internal bool IsPieceAdded(HomeCoordinates coordinates)
    {
        return pieces.ContainsKey(coordinates);
    }

    internal bool IsPieceAdded(Piece piece)
    {
        return guids.Contains(piece.Guid);
    }
}