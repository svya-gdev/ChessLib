namespace Chess.Domain

[<Struct>]
type public PieceDevelopment = {
    Advances : Set<PieceDislocation>
    Captures : Set<PieceDislodgement>
}

module public Development =
    let internal Palfrey : PieceDevelopment = {
        Advances = Set.empty
        Captures = Set.empty
    }
    let internal Destrier : PieceDevelopment = {
        Advances = Set.empty
        Captures = Set.empty
    }
    let internal Courser : PieceDevelopment = {
        Advances = Set.empty
        Captures = Set.empty
    }