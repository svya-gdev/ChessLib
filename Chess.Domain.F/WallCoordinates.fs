namespace Chess.Domain

[<Struct>]
type internal WallCoordinates = {
    T : TileCoordinates
    S : CoordinateShift
} with
    member internal this.toRoomCoordinates = {
        A = uint8 (this.T.C / 4u)
        B = uint8 (this.T.R / 4u)
    }
    member internal this.toHomeCoordinates = {
        X = uint64 this.T.C * 2UL + (match this.S with V -> 0UL | _ -> 1UL)
        Y = uint64 this.T.R * 2UL + (match this.S with H -> 0UL | _ -> 1UL)
    }