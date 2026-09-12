namespace Chess.Domain

module internal Convert =
    let internal toDislocation(l) = [
        for c in l -> {
            FileDelta = c.C
            RankDelta = c.R
        }
    ]
    let internal toClassicalDislodgements(l) : List<PieceDislodgement> = [
        for c in l -> {
            Relocation = {
                FileDelta = c.C
                RankDelta = c.R
            }
            Feud = Feud.WithOpposite
        }
    ]