namespace Chess.Domain

[<Struct>]
type internal CoordinateShift =
    | H // Horizontal
    | V // Vertical
    | D // Diagonal