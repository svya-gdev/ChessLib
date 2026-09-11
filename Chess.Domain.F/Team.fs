namespace Chess.Domain

[<Struct>]
type public Team =
    | Undefined // SameTeam is Undefined // Neutrals are Undefined // Opposite is Undefined //
    | White     // SameTeam is White     // Neutrals are Grays     // Opposite is Black     //
    | Grays     // SameTeam is Undefined // Neutrals are Undefined // Opposite is Undefined //
    | Black     // SameTeam is Black     // Neutrals are Grays     // Opposite is White     //
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