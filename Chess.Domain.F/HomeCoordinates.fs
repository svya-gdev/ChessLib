namespace Chess.Domain

[<Struct>]
type internal HomeCoordinates = {
    X : uint64
    Y : uint64
} with
    member internal this.ToRoomCoordinates = {
        A = uint8 (this.X / 8UL)
        B = uint8 (this.Y / 8UL)
    }
