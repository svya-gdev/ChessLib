namespace Chess.Domain

// This might be better, as it is more intuitive

module internal Combinations =
    let internal a = [
        {
            FileDelta = 1u
            RankDelta = 0u
        }
        {
            FileDelta = 0u
            RankDelta = 1u
        }
        {
            FileDelta = 0u - 1u
            RankDelta = 0u
        }
        {
            FileDelta = 0u
            RankDelta = 0u - 1u
        }
    ]
    let internal aa = [
        {
            FileDelta = 2u
            RankDelta = 0u
        }
        {
            FileDelta = 0u
            RankDelta = 2u
        }
        {
            FileDelta = 0u - 2u
            RankDelta = 0u
        }
        {
            FileDelta = 0u
            RankDelta = 0u - 2u
        }
    ]
    let internal aaa = [
        {
            FileDelta = 3u
            RankDelta = 0u
        }
        {
            FileDelta = 0u
            RankDelta = 3u
        }
        {
            FileDelta = 0u - 3u
            RankDelta = 0u
        }
        {
            FileDelta = 0u
            RankDelta = 0u - 3u
        }
    ]
    let internal b = [
        {
            FileDelta = 1u
            RankDelta = 1u
        }
        {
            FileDelta = 1u
            RankDelta = 0u - 1u
        }
        {
            FileDelta = 0u - 1u
            RankDelta = 1u
        }
        {
            FileDelta = 0u - 1u
            RankDelta = 0u - 1u
        }
    ]
    let internal bb = [
        {
            FileDelta = 2u
            RankDelta = 2u
        }
        {
            FileDelta = 2u
            RankDelta = 0u - 2u
        }
        {
            FileDelta = 0u - 2u
            RankDelta = 2u
        }
        {
            FileDelta = 0u - 2u
            RankDelta = 0u - 2u
        }
    ]
    let internal bbb = [
        {
            FileDelta = 3u
            RankDelta = 3u
        }
        {
            FileDelta = 3u
            RankDelta = 0u - 3u
        }
        {
            FileDelta = 0u - 3u
            RankDelta = 3u
        }
        {
            FileDelta = 0u - 3u
            RankDelta = 0u - 3u
        }
    ]
    let internal c = [
        {
            FileDelta = 2u
            RankDelta = 1u
        }
        {
            FileDelta = 2u
            RankDelta = 0u - 1u
        }
        {
            FileDelta = 0u - 2u
            RankDelta = 1u
        }
        {
            FileDelta = 0u - 2u
            RankDelta = 0u - 1u
        }
        {
            FileDelta = 1u
            RankDelta = 2u
        }
        {
            FileDelta = 0u - 1u
            RankDelta = 2u
        }
        {
            FileDelta = 1u
            RankDelta = 0u - 2u
        }
        {
            FileDelta = 0u - 1u
            RankDelta = 0u - 2u
        }
    ]

    module internal NewPieces =
    let internal Palfrey : PieceDevelopment = {
        Advances = Set.empty // y
        Captures = Set.empty // n
    }
    let internal Destrier : PieceDevelopment = {
        Advances = Set.empty // n
        Captures = Set.empty // y
    }
    let internal Squire : PieceDevelopment = {
        Advances = Set.empty // y
        Captures = Set.empty // y
    }