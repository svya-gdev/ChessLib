namespace Chess.Domain

[<Struct>]
type public PawnDevelopment = {
    Advances : Set<PawnDislocation>
    Captures : Set<PawnDislodgement>
}

module public Development =
    let internal Palfrey : PawnDevelopment = {
        Advances = Set.empty
        Captures = Set.empty
    }
    let internal Destrier : PawnDevelopment = {
        Advances = Set.empty
        Captures = Set.empty
    }
    let internal Courser : PawnDevelopment = {
        Advances = Set.empty
        Captures = Set.empty
    }