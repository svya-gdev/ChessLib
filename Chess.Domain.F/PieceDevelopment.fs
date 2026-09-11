namespace Chess.Domain

[<Struct>]
type public PieceDevelopment = {
    Advances : Set<PieceRelocation>
    Captures : Set<PieceDislodgement>
}