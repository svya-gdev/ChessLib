namespace Chess.Domain

[<Struct>]
type public PieceLocation = {
    File: uint32
    Rank: uint32
} with
    member internal this.ToRoomCoordinates = {
        A = uint8 (this.File / 4u)
        B = uint8 (this.Rank / 4u)
    }
    member internal this.ToHomeCoordinates = {
        X = uint64 this.File * 2UL
        Y = uint64 this.Rank * 2UL
    }
    member internal this.ToTileCoordinates = {
        C = this.File
        R = this.Rank
    }
