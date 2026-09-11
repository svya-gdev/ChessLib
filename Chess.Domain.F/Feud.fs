namespace Chess.Domain

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
    member internal this.IsTeam1HostileToTeam2 team1 team2 =
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
