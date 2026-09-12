namespace Chess.Domain

[<Struct>]
type public PieceDevelopment = {     // Создание и улучшение фигуры
    Advances: Set<PieceRelocation>   // Продвижения
    Captures: Set<PieceDislodgement> // Взятия
}