namespace Chess.Domain

module public StandardPieces =
    let public whitePawn = {
        Advances = ClassicalDirections.towardsBlacks |> Repeat.once        |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.towardsBlacks |> Repeat.twiceAlongC |> Convert.toClassicalDislodgements |> Set.ofList
    }
    let public blackPawn = {
        Advances = ClassicalDirections.towardsWhites |> Repeat.once        |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.towardsWhites |> Repeat.twiceAlongC |> Convert.toClassicalDislodgements |> Set.ofList
    }
    let public anyBishop = {
        Advances = ClassicalDirections.diagonals |> Repeat.sevenTimesFromList |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.diagonals |> Repeat.sevenTimesFromList |> Convert.toClassicalDislodgements |> Set.ofList
    }
    let public anyRook = {
        Advances = ClassicalDirections.orthogonals |> Repeat.sevenTimesFromList |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.orthogonals |> Repeat.sevenTimesFromList |> Convert.toClassicalDislodgements |> Set.ofList
    }
    let public anyKing = {
        Advances = ClassicalDirections.all |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.all |> Convert.toClassicalDislodgements |> Set.ofList
    }
    let public anyQueen = {
        Advances = ClassicalDirections.all |> Repeat.sevenTimesFromList |> Convert.toDislocation            |> Set.ofList
        Captures = ClassicalDirections.all |> Repeat.sevenTimesFromList |> Convert.toClassicalDislodgements |> Set.ofList
    }
    let public anyKnight = {
            Advances = ClassicalDirections.horseLike |> Convert.toDislocation            |> Set.ofList
            Captures = ClassicalDirections.horseLike |> Convert.toClassicalDislodgements |> Set.ofList
        }