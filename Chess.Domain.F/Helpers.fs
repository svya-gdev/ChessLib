namespace Chess.Domain

// A place for types and modules that help describe dislocations

[<Struct>]
type public Direction =
    | Up
    | UpRight
    | Right
    | DownRight
    | Down
    | DownLeft
    | Left
    | UpLeft
    with
    member public this.ToOffset() =
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
type public StraightPath = {
    Direction : Direction
    Length : uint32
} with
    member public this.ToPawnDislocation() =
        let (h, v) = this.Direction.ToOffset()
        {
            Horizontal = h * this.Length
            Vertical   = v * this.Length
        }

[<Struct>]
type public Dash  =
    | DashUp
    | DashRight
    | DashDown
    | DashLeft
    with
    member public this.ToOffset() =
        match this with
        | DashUp        -> (0u     , 1u     )
        | DashRight     -> (1u     , 0u     )
        | DashDown      -> (0u     , 0u - 1u)
        | DashLeft      -> (0u - 1u, 0u     )

[<Struct>]
type public HorseLikePath = {
    Direction : Direction
    Length : uint32
    Dash : Dash
} with
    member public this.ToPawnDislocation() =
        let (lh, lv) = this.Direction.ToOffset()
        let (dh, dv) = this.Dash.ToOffset()
        {
            Horizontal = lh * this.Length + dh
            Vertical   = lv * this.Length + dv
        }