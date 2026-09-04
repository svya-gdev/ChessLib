namespace Chess.Domain

// It would have overly complicated the documentation, so I hid it

[<Struct>]
type  internal RoomCoordinates = {
    X : uint8
    Y : uint8
}

[<Struct>]
type internal HomeCoordinates = {
    X : uint64
    Y : uint64
} with
    member internal this.ToRoomCoordinates() : RoomCoordinates = {
        X = uint8 (this.X / 8UL)
        Y = uint8 (this.Y / 8UL)
    }

[<Struct>]
type internal TileCoordinates = {
    X : uint32
    Y : uint32
} with
    member internal this.ToRoomCoordinates() : RoomCoordinates = {
        X = uint8 (this.X / 4u)
        Y = uint8 (this.Y / 4u)
    }
    member internal this.ToHomeCoordinates() : HomeCoordinates = {
        X = uint64 this.X * 2UL
        Y = uint64 this.Y * 2UL
    }

[<Struct>]
type internal CoordinateShift =
    | Horizontal
    | Vertical
    | Diagonal

[<Struct>]
type internal WallCoordinates = {
    C : TileCoordinates
    S : CoordinateShift
} with
    member internal this.ToRoomCoordinates() : RoomCoordinates = {
        X = uint8 (this.C.X / 4u)
        Y = uint8 (this.C.Y / 4u)
    }
    member internal this.ToHomeCoordinates() : HomeCoordinates = {
        X = uint64 this.C.X * 2UL + (match this.S with Vertical   -> 0UL | _ -> 1UL)
        Y = uint64 this.C.Y * 2UL + (match this.S with Horizontal -> 0UL | _ -> 1UL)
    }