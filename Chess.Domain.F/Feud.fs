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
    member internal this.IsTeamOneHostileToTeamTwo(teamOne, teamTwo) =
        if teamOne = Undefined || teamTwo = Undefined then
            false
        else
            match this with
            | WithSameTeam -> teamOne.SameTeam = teamTwo
            | WithNeutrals -> teamOne.Neutrals = teamTwo
            | WithOpposite -> teamOne.Opposite = teamTwo
            | WithSameTeamAndNeutrals -> teamOne.SameTeam = teamTwo || teamOne.Neutrals = teamTwo
            | WithNeutralsAndOpposite -> teamOne.Neutrals = teamTwo || teamOne.Opposite = teamTwo
            | WithSameTeamAndOpposite -> teamOne.SameTeam = teamTwo || teamOne.Opposite = teamTwo
            | WithEveryone -> true
