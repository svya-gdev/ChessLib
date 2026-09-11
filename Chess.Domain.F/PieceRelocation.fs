namespace Chess.Domain

[<Struct>]
type public PieceRelocation = {
    FileDelta: uint32
    RankDelta: uint32
} with
    member internal this.applyTo location = {
        File = location.File + this.FileDelta
        Rank = location.Rank + this.RankDelta
    }
    member internal this.isHorseLike =
        this.FileDelta <> this.RankDelta &&
        this.FileDelta <> 0u             &&
                    0u <> this.RankDelta
    member internal this.isStraight = not this.isHorseLike
    member internal this.isNonMoving =
        this.FileDelta =  0u &&
        this.RankDelta =  0u