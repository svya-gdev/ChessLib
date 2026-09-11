namespace Chess.Domain

[<Struct>]
type public PieceMindset = {
    Team: Team
    Feud: Feud
} with
    member internal this.isHostileTo mindset =
        this.Feud.isTeam1HostileToTeam2 this.Team mindset.Team