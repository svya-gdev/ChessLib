namespace Chess.Domain

module internal Combinations =
    let internal orthogonalOneTileMoves = [
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
    let internal orthogonalTwoTileMoves = [
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
    let internal orthogonalThreeTileMoves = [
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
    let internal diagonalOneTileMoves = [
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
    let internal diagonalTwoTileMoves = [
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
    let internal diagonalThreeTileMoves = [
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
    let internal orthogonalKnightMoves = [
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
    let internal diagonalKnightMoves = [
        {
            FileDelta = 3u
            RankDelta = 1u
        }
        {
            FileDelta = 3u
            RankDelta = 0u - 1u
        }
        {
            FileDelta = 0u - 3u
            RankDelta = 1u
        }
        {
            FileDelta = 0u - 3u
            RankDelta = 0u - 1u
        }
        {
            FileDelta = 1u
            RankDelta = 3u
        }
        {
            FileDelta = 0u - 1u
            RankDelta = 3u
        }
        {
            FileDelta = 1u
            RankDelta = 0u - 3u
        }
        {
            FileDelta = 0u - 1u
            RankDelta = 0u - 3u
        }
    ]
    let internal remainingKnightMoves = [
        {
            FileDelta = 3u
            RankDelta = 2u
        }
        {
            FileDelta = 3u
            RankDelta = 0u - 2u
        }
        {
            FileDelta = 0u - 3u
            RankDelta = 2u
        }
        {
            FileDelta = 0u - 3u
            RankDelta = 0u - 2u
        }
        {
            FileDelta = 2u
            RankDelta = 3u
        }
        {
            FileDelta = 0u - 2u
            RankDelta = 3u
        }
        {
            FileDelta = 2u
            RankDelta = 0u - 3u
        }
        {
            FileDelta = 0u - 2u
            RankDelta = 0u - 3u
        }
    ]