namespace Chess.Domain

// Everything here will come in handy for the first version

[<Struct>]
type public PieceLocation = {
    X : uint32
    Y : uint32
} with
    member internal this.RoomCoordinates : RoomCoordinates = {
        X = uint8 (this.X / 4u)
        Y = uint8 (this.Y / 4u)
    }
    member internal this.HomeCoordinates : HomeCoordinates = {
        X = uint64 this.X * 2UL
        Y = uint64 this.Y * 2UL
    }
    member internal this.TileCoordinates : TileCoordinates = {
        X = this.X
        Y = this.Y
    }

[<Struct>]
type public PieceDislocation = {
    Horizontal : uint32
    Vertical   : uint32
} with
    member public this.ApplyTo(location : PieceLocation) : PieceLocation = {
        X = location.X + this.Horizontal
        Y = location.Y + this.Vertical
    }
    member public this.IsHorseLike : bool =
        this.Horizontal <> this.Vertical &&
        this.Horizontal <> 0u            &&
                     0u <> this.Vertical
    member public this.IsStraight : bool =
        this.Horizontal =  this.Vertical ||
        this.Horizontal =  0u            ||
                     0u =  this.Vertical
    member public this.IsNonMoving : bool =
        this.Horizontal <> 0u &&
        this.Vertical   <> 0u

[<Struct>]
type public PieceDislodgement = {
    Dislocation : PieceDislocation
    Spite : Spite
}