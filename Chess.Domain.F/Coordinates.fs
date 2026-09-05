namespace Chess.Domain

// It would have overly complicated the documentation, so I hid it

[<Struct>]
type  internal RoomCoordinates = {
    A : uint8
    B : uint8
}

[<Struct>]
type internal HomeCoordinates = {
    X : uint64
    Y : uint64
} with
    member internal this.ToRoomCoordinates() : RoomCoordinates = {
        A = uint8 (this.X / 8UL)
        B = uint8 (this.Y / 8UL)
    }

[<Struct>]
type internal TileCoordinates = {
    C : uint32
    R : uint32
} with
    member internal this.ToRoomCoordinates() : RoomCoordinates = {
        A = uint8 (this.C / 4u)
        B = uint8 (this.R / 4u)
    }
    member internal this.ToHomeCoordinates() : HomeCoordinates = {
        X = uint64 this.C * 2UL
        Y = uint64 this.R * 2UL
    }

[<Struct>]
type internal CoordinateShift =
    | Horizontal
    | Vertical
    | Diagonal

[<Struct>]
type internal WallCoordinates = {
    T : TileCoordinates
    S : CoordinateShift
} with
    member internal this.ToRoomCoordinates() : RoomCoordinates = {
        A = uint8 (this.T.C / 4u)
        B = uint8 (this.T.R / 4u)
    }
    member internal this.ToHomeCoordinates() : HomeCoordinates = {
        X = uint64 this.T.C * 2UL + (match this.S with Vertical   -> 0UL | _ -> 1UL)
        Y = uint64 this.T.R * 2UL + (match this.S with Horizontal -> 0UL | _ -> 1UL)
    }