namespace Chess.Domain

[<Struct>]
type public Team =
    | Undefined // Matching is Undefined // Neutrals are Undefined // Opposite is Undefined //
    | White     // Matching is White     // Neutrals are Grays     // Opposite is Black     //
    | Grays     // Matching is Undefined // Neutrals are Undefined // Opposite is Undefined //
    | Black     // Matching is Black     // Neutrals are Grays     // Opposite is White     //
    with
    member public this.SameTeam =
        match this with
        | White -> White
        | Black -> Black
        | _ -> Undefined
    member public this.Neutrals =
        match this with
        | White -> Grays
        | Black -> Grays
        | _ -> Undefined
    member public this.Opposite =
        match this with
        | White -> Black
        | Black -> White
        | _ -> Undefined

[<Struct>]
type public Feud =
    | WithSameTeam
    | WithNeutrals
    | WithOpposite
    | WithSameTeamAndNeutrals
    | WithNeutralsAndOpposite
    | WithSameTeamAndOpposite
    | WithEveryone
    with
    member public this.IsTeam1HostileToTeam2(team1: Team, team2) =
        if team1 = Undefined || team2 = Undefined then
            false
        else
            match this with
            | WithSameTeam -> team1.SameTeam = team2
            | WithNeutrals -> team1.Neutrals = team2
            | WithOpposite -> team1.Opposite = team2
            | WithSameTeamAndNeutrals -> team1.SameTeam = team2 || team1.Neutrals = team2
            | WithNeutralsAndOpposite -> team1.Neutrals = team2 || team1.Opposite = team2
            | WithSameTeamAndOpposite -> team1.SameTeam = team2 || team1.Opposite = team2
            | WithEveryone -> true

[<Struct>]
type public PieceMindset = {
    Team: Team
    Feud: Feud
} with
    member public this.IsHostileTo(mindset) =
        this.Feud.IsTeam1HostileToTeam2(this.Team, mindset.Team)