namespace Chess.Domain

[<Struct>]
type public PieceDislodgement = {
    Relocation: PieceRelocation
    Fued      : Feud
}