namespace Chess.Domain

// Everything here will come in handy for the first version

[<Struct>]
type public PieceLocation = {
    File: uint32
    Rank: uint32
} with
    member internal this.RoomCoordinates : RoomCoordinates = {
        A = uint8 (this.File / 4u)
        B = uint8 (this.Rank / 4u)
    }
    member internal this.HomeCoordinates : HomeCoordinates = {
        X = uint64 this.File * 2UL
        Y = uint64 this.Rank * 2UL
    }
    member internal this.TileCoordinates : TileCoordinates = {
        C = this.File
        R = this.Rank
    }

[<Struct>]
type public PieceRelocation = {
    FileDelta: uint32
    RankDelta: uint32
} with
    member public this.ApplyTo(location) = {
        File = location.File + this.FileDelta
        Rank = location.Rank + this.RankDelta
    }
    member public this.IsHorseLike =
        this.FileDelta <> this.RankDelta &&
        this.FileDelta <> 0u             &&
                    0u <> this.RankDelta
    member public this.IsStraight =
        this.FileDelta =  this.RankDelta ||
        this.FileDelta =  0u             ||
                    0u =  this.RankDelta
    member public this.IsNonMoving =
        this.FileDelta =  0u &&
        this.RankDelta =  0u

[<Struct>]
type public PieceDislodgement = {
    Relocation : PieceRelocation
    Spite : Spite
}