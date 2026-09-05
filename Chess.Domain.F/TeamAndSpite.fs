namespace Chess.Domain

// I need simplified team logic for the first version

[<Struct>]
type public Team =
    | White  // Opposite is Black  // Neutral is Grey  // The same is White  //
    //| Grey   // Opposite is Grey   // Neutral is Grey  // The same is Grey   //
    | Black  // Opposite is White  // Neutral is Grey  // The same is Black  //
    with
    member public this.Opposite =
        match this with
        | White -> Black
        //| Grey  -> Grey
        | Black -> White

[<Struct>]
type public Spite =
    | WithOppositeTeam
    //| WithNeutralTeam
    //| WithTheSameTeam
    //| WithOppositeAndNeutralTeams
    //| WithNeutralAndTheSameTeams
    //| WithTheSameAndOppositeTeams
    | WithEveryTeam
    with
    member public this.IsFronTeamToTeam(subject : Team, object : Team) =
        match this with
        | WithOppositeTeam            -> subject.Opposite = object
        //| WithNeutralTeam             -> Grey             = object
        //| WithTheSameTeam             -> subject          = object
        //| WithOppositeAndNeutralTeams -> subject.Opposite = object || Grey             = object
        //| WithNeutralAndTheSameTeams  -> Grey             = object || subject          = object
        //| WithTheSameAndOppositeTeams -> subject          = object || subject.Opposite = object
        | WithEveryTeam               -> true