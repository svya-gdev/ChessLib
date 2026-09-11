namespace Chess.Domain

[<Struct>]
type public PieceMindset = {
    Team: Team
    Feud: Feud
} with
    member internal this.IsHostileTo mindset =
        this.Feud.IsTeam1HostileToTeam2 this.Team mindset.Team
