namespace Chess.Domain

[<Struct>]
type internal TileCoordinates = {
    C : uint32
    R : uint32
} with
    member internal this.toRoomCoordinates = {
        A = uint8 (this.C / 4u)
        B = uint8 (this.R / 4u)
    }
    member internal this.toHomeCoordinates = {
        X = uint64 this.C * 2UL
        Y = uint64 this.R * 2UL
    }