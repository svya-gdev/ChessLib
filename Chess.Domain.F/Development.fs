namespace Chess.Domain

[<Struct>]
type public PieceDevelopment = {
    Advances : Set<PieceRelocation>
    Captures : Set<PieceDislodgement>
}

// Chess.Domain.ClassicalPieces
// Chess.Domain.Pieces