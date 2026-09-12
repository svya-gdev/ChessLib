namespace Chess.Domain

[<Struct>]
type public Team = // Команда
    | Undefined    // Не определена
    | White        // Чёрных
    | Grays        // Всех серых
    | Black        // Белых
    with
    member internal this.SameTeam =
        match this with
        | White -> White
        | Black -> Black
        | _ -> Undefined
    member internal this.Neutrals =
        match this with
        | White -> Grays
        | Black -> Grays
        | _ -> Undefined
    member internal this.Opposite =
        match this with
        | White -> Black
        | Black -> White
        | _ -> Undefined