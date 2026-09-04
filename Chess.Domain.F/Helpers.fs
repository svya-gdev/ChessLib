namespace Chess.Domain

// A place for types and modules that help describe dislocations

[<Struct>]
type internal Direction =
    | Up
    | UpRight
    | Right
    | DownRight
    | Down
    | DownLeft
    | Left
    | UpLeft
    with
    member internal this.ToOffset() =
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
type internal StraightPath = {
    Direction : Direction
    Length : uint32
} with
    member internal this.ToPawnDislocation() =
        let (h, v) = this.Direction.ToOffset()
        {
            Horizontal = h * this.Length
            Vertical   = v * this.Length
        }

[<Struct>]
type internal Dash  =
    | DashUp
    | DashRight
    | DashDown
    | DashLeft
    with
    member internal this.ToOffset() =
        match this with
        | DashUp        -> (0u     , 1u     )
        | DashRight     -> (1u     , 0u     )
        | DashDown      -> (0u     , 0u - 1u)
        | DashLeft      -> (0u - 1u, 0u     )

[<Struct>]
type internal HorseLikePath = {
    Direction : Direction
    Length : uint32
    Dash : Dash
} with
    member internal this.ToPawnDislocation() =
        let (lh, lv) = this.Direction.ToOffset()
        let (dh, dv) = this.Dash.ToOffset()
        {
            Horizontal = lh * this.Length + dh
            Vertical   = lv * this.Length + dv
        }