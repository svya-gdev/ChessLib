namespace Chess.Domain

module internal ClassicalDirections =
    let internal towardsBlacks = {
        C = 0u
        R = 1u
    }
    let internal towardsNone = {
        C = 0u
        R = 0u
    }
    let internal towardsWhites = {
        C = 0u
        R = 0u - 1u
    }
    let internal diagonals = List.concat [
        towardsBlacks |> Repeat.twiceAlongC;
        towardsWhites |> Repeat.twiceAlongC
    ]
    let internal orthogonals = List.concat [
        towardsBlacks |> Repeat.once
        towardsNone   |> Repeat.twiceAlongC
        towardsWhites |> Repeat.once
    ]
    let internal all = List.concat [
        diagonals;
        orthogonals
    ]
    let internal horseLike = List.concat [
        {
            C = 0u
            R = 2u
        } |> Repeat.twiceAlongC
        {
            C = 0u
            R = 0u - 2u
        } |> Repeat.twiceAlongC
        {
            C = 2u
            R = 0u
        } |> Repeat.twiceAlongR
        {
            C = 0u - 2u
            R = 0u
        } |> Repeat.twiceAlongR
    ]