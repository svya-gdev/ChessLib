namespace Chess.Domain

module internal Repeat =
    let internal once(c) = [
        {
            C = c.C
            R = c.R
        }
    ]
    let internal twiceAlongC(c) = [
        {
            C = 0u - 1u
            R = c.R
        };
        {
            C = 1u
            R = c.R
        }
    ]
    let internal twiceAlongR(c) = [
        {
            C = c.C
            R = 0u - 1u
        };
        {
            C = c.C
            R = 1u
        }
    ]
    let internal sevenTimes(c) = [
        for n in 1u .. 7u -> {
            C = c.C * n
            R = c.R * n
        }
    ]
    let internal sevenTimesFromList(l) = List.concat [
        for n in l -> n |> sevenTimes
    ]