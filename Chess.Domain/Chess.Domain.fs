namespace Chess.Domain

[<Struct>]
type HomeCoordinates = {
    X : uint64
    Y : uint64
}

[<Struct>]
type TileCoordinates = {
    X : uint32
    Y : uint32
} with
    member this.ToHomeCoordinates() : HomeCoordinates = {
        X = uint64 this.X * 2UL
        Y = uint64 this.Y * 2UL
    }

[<Struct>]
type CoordinateShift =
    | Horizontal
    | Vertical
    | Diagonal

[<Struct>]
type WallCoordinates = {
    C : TileCoordinates
    S : CoordinateShift
} with
    member this.ToHomeCoordinates() : HomeCoordinates = {
        X = uint64 this.C.X * 2UL + (match this.S with Vertical   -> 0UL | _ -> 1UL)
        Y = uint64 this.C.Y * 2UL + (match this.S with Horizontal -> 0UL | _ -> 1UL)
    }

[<Struct>]
type RoomCoordinates = {
    X : uint8
    Y : uint8
} with
    member this.ToHomeCoordinates() : HomeCoordinates = {
        X = uint64 this.X * 8UL
        Y = uint64 this.Y * 8UL
    }

// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 

[<Struct>]
type PawnLocation = {
    TileCoordinates : TileCoordinates
} with
    member this.HomeCoordinates = this.TileCoordinates.ToHomeCoordinates()
    member this.RoomCoordinates = {
        X = uint8 (this.TileCoordinates.X / 4u)
        Y = uint8 (this.TileCoordinates.Y / 4u)
    }

[<Struct>]
type PawnDislocation = {
    Horizontal       : uint32
    Vertical         : uint32
} with
    member this.ApplyTo(location) = {
        TileCoordinates = {
            X = location.TileCoordinates.X + this.Horizontal
            Y = location.TileCoordinates.Y + this.Vertical
        }
    }

[<Struct>]
type Direction =
    | Up
    | UpRight
    | Right
    | DownRight
    | Down
    | DownLeft
    | Left
    | UpLeft
    with
    member this.ToOffset() =
        match this with
        | Up        -> (0u     , 1u     )
        | UpRight   -> (1u     , 1u     )
        | Right     -> (1u     , 0u     )
        | DownRight -> (1u     , 0u - 1u)
        | Down      -> (0u     , 0u - 1u)
        | DownLeft  -> (0u - 1u, 0u - 1u)
        | Left      -> (0u - 1u, 0u     )
        | UpLeft    -> (0u - 1u, 1u     )

[<Struct>]
type StraightPath = {
    Direction : Direction
    Length : uint32
} with
    member this.ToPawnDislocation() =
        let (h, v) = this.Direction.ToOffset()
        {
            Horizontal = h * this.Length
            Vertical   = v * this.Length
        }

[<Struct>]
type Dash  =
    | DashUp
    | DashRight
    | DashDown
    | DashLeft
    with
    member this.ToOffset() =
        match this with
        | DashUp        -> (0u     , 1u     )
        | DashRight     -> (1u     , 0u     )
        | DashDown      -> (0u     , 0u - 1u)
        | DashLeft      -> (0u - 1u, 0u     )

[<Struct>]
type HorseLikePath = {
    Direction : Direction
    Length : uint32
    Dash : Dash
} with
    member this.ToPawnDislocation() =
        let (lh, lv) = this.Direction.ToOffset()
        let (dh, dv) = this.Dash.ToOffset()
        {
            Horizontal = lh * this.Length + dh
            Vertical   = lv * this.Length + dv
        }

// // // // // // // // // // // // // // // // // // // // // // // // // // // // // // 

[<Struct>]
type Team =
    | White
    | Grey
    | Black
    with
    member this.IsAbleToCapture(other) =
        match (this, other) with
        | (a, b) when a = b -> false
        | _ -> true

// It's not dynamic enough if I want to level them up.
type Pawn = {
    Guid     : System.Guid
    Team     : Team
    Enemies  : Team[]
    Advances : PawnDislocation[]
    Captures : PawnDislocation[]
}