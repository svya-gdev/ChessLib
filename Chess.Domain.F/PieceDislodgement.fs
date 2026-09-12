namespace Chess.Domain

[<Struct>]
type public PieceDislodgement = { // Вытеснение фигуры
    Relocation: PieceRelocation   // Смещение фигуры
    Feud:       Feud              // Вражда
}