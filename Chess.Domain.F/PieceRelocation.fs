namespace Chess.Domain

[<Struct>]
type public PieceRelocation = { // Смещение фигуры
    FileDelta: uint32           // Дельта файла
    RankDelta: uint32           // Дельта ранка
} with
    member internal this.ApplyTo location = {
        File = location.File + this.FileDelta
        Rank = location.Rank + this.RankDelta
    }
    member internal this.IsHorseLike =
        this.FileDelta <> this.RankDelta &&
        this.FileDelta <> 0u             &&
                    0u <> this.RankDelta
    member internal this.IsStraight = not this.IsHorseLike
    member internal this.IsNonMoving =
        this.FileDelta =  0u &&
        this.RankDelta =  0u