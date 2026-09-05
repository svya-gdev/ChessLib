namespace Chess.Domain

// A place for types and modules that help describe dislocations


module internal Repeat =

    let once(c) = [
        {
            C = c.C
            R = c.R
        }
    ]

    let twiceAlongC(c) = [
        {
            C = 0u - 1u
            R = c.R
        };
        {
            C = 1u
            R = c.R
        }
    ]

    let twiceAlongR(c) = [
        {
            C = c.C
            R = 0u - 1u
        };
        {
            C = c.C
            R = 1u
        }
    ]


    let sevenTimes(c) = [
        for n in 1u .. 7u -> {
            C = c.C * n
            R = c.R * n
        }
    ]

    let sevenTimesFromList(l) = List.concat [
        for n in l -> n |> sevenTimes
    ]

module internal ClassicalDirections =

    let towardsBlacks = {
        C = 0u
        R = 1u
    }

    let   towardsNone = {
        C = 0u
        R = 0u
    }

    let towardsWhites = {
        C = 0u
        R = 0u - 1u
    }

    let diagonals = List.concat [
        towardsBlacks |> Repeat.twiceAlongC;
        towardsWhites |> Repeat.twiceAlongC
    ]

    let orthogonals = List.concat [
        towardsBlacks |> Repeat.once
        towardsNone   |> Repeat.twiceAlongC
        towardsWhites |> Repeat.once
    ]

    let all = List.concat [
        diagonals;
        orthogonals
    ]

    let horseLike = List.concat [
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


module internal Convert =

    let toDislocation(l) = [
        for c in l -> {
            FileDelta = c.C
            RankDelta   = c.R
        }
    ]

    let toClassicalDislodgements(l) : List<PieceDislodgement> = [
        for c in l -> {
            Relocation = {
                FileDelta = c.C
                RankDelta   = c.R
            }
            Spite = WithOppositeTeam
        }
    ]