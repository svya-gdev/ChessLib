namespace Chess.Domain

[<Struct>]
type public Feud =            // Вражда
    | WithSameTeam            // С той же командой
    | WithNeutrals            // С нейтральными командами
    | WithOpposite            // С командой соперника
    | WithSameTeamAndNeutrals // С той же командой и с нейтральными командами
    | WithNeutralsAndOpposite // С нейтральными командами и с командой соперника
    | WithSameTeamAndOpposite // С той же командой и с командой соперника
    | WithEveryone            // С каждой командой
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