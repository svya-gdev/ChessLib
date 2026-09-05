namespace Chess.Domain

[<Struct>]
type public PieceDevelopment = {
    Advances : Set<PieceRelocation>
    Captures : Set<PieceDislodgement>
}


module public ClassicalPieces =

    let whitePawn = {
        Advances = ClassicalDirections.towardsBlacks |> Repeat.once        |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.towardsBlacks |> Repeat.twiceAlongC |> Convert.toClassicalDislodgements |> Set.ofList
    }

    let blackPawn = {
        Advances = ClassicalDirections.towardsWhites |> Repeat.once        |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.towardsWhites |> Repeat.twiceAlongC |> Convert.toClassicalDislodgements |> Set.ofList
    }

    let anyBishop = {
        Advances = ClassicalDirections.diagonals |> Repeat.sevenTimesFromList |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.diagonals |> Repeat.sevenTimesFromList |> Convert.toClassicalDislodgements |> Set.ofList
    }

    let anyRook = {
        Advances = ClassicalDirections.orthogonals |> Repeat.sevenTimesFromList |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.orthogonals |> Repeat.sevenTimesFromList |> Convert.toClassicalDislodgements |> Set.ofList
    }

    let anyKing = {
        Advances = ClassicalDirections.all |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.all |> Convert.toClassicalDislodgements |> Set.ofList
    }

    let anyQueen = {
        Advances = ClassicalDirections.all |> Repeat.sevenTimesFromList |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.all |> Repeat.sevenTimesFromList |> Convert.toClassicalDislodgements |> Set.ofList
    }

    let anyKnight = {
            Advances = ClassicalDirections.horseLike |> Convert.toDislocation            |> Set.ofList
            Captures = ClassicalDirections.horseLike |> Convert.toClassicalDislodgements |> Set.ofList
        }

module public NewPieces =

    let internal Palfrey : PieceDevelopment = {
        Advances = Set.empty // y
        Captures = Set.empty // n
    }

    let internal Destrier : PieceDevelopment = {
        Advances = Set.empty // n
        Captures = Set.empty // y
    }
    
    let internal squire : PieceDevelopment = {
        Advances = Set.empty // y
        Captures = Set.empty // y
    }