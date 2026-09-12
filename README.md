![3](https://github.com/svya-gdev/ChessLib/blob/master/Vectors/Group%202%20(3).svg)

![4](Vectors/Group 2 (4).svg)

![5](Vectors/Group 2 (5).svg)

![6](https://github.com/svya-gdev/ChessLib/blob/master/Vectors/Group%202%20(6).svg)

#### Minor types / Малые типы

Self-explanatory structures and classes. / Структуры и классы, не требующие пояснений.

##### PieceLocation / Локация фигуры

```F#
[<Struct>]
type public PieceLocation = { //Локация фигуры
    File: uint32              // Файл
    Rank: uint32              // Ранк
}
```

##### PieceRelocation / Смещение фигуры

```F#
[<Struct>]
type public PieceRelocation = { // Смещение фигуры
    FileDelta: uint32           // Дельта файла
    RankDelta: uint32           // Дельта ранка
}
```

##### Team / Команда

```F#
[<Struct>]
type public Team = // Команда
    | Undefined    // Не определена
    | White        // Чёрных
    | Grays        // Всех серых
    | Black        // Белых
```

##### Feud / Вражда

```F#
[<Struct>]
type public Feud =            // Вражда
    | WithSameTeam            // С той же командой
    | WithNeutrals            // С нейтральными командами
    | WithOpposite            // С командой соперника
    | WithSameTeamAndNeutrals // С той же командой и с нейтральными командами
    | WithNeutralsAndOpposite // С нейтральными командами и с командой соперника
    | WithSameTeamAndOpposite // С той же командой и с командой соперника
    | WithEveryone            // С каждой командой
```

##### PieceDislodgement / Вытеснение фигуры

```F#
[<Struct>]
type public PieceDislodgement = { // Вытеснение фигуры
    Relocation: PieceRelocation   // Смещение фигуры
    Feud:       Feud              // Вражда
}
```

##### PieceDevelopment / Создание и улучшение фигуры

```F#
[<Struct>]
type public PieceDevelopment = {     // Создание и улучшение фигуры
    Advances: Set<PieceRelocation>   // Продвижения
    Captures: Set<PieceDislodgement> // Взятия
}
```

##### Piece / Фигура

```C#
// Фигура ( Создание и улучшение фигуры , Команда )
public sealed class Piece(PieceDevelopment development, Team team)
{
    public readonly Guid Guid = Guid.NewGuid(); // Гуид
    public readonly Team Team = team;           // Команда

    public readonly HashSet<PieceRelocation>   Advances = development.Advances.ToHashSet(); // Продвижения
    public readonly HashSet<PieceDislodgement> Captures = development.Captures.ToHashSet(); // Взятия
}
```

#### Major types / Большие типы

Modules and classes requiring explanation. / Модули и классы, требующие пояснения.

##### module public StandardPieces / Публичный модуль стандартные фигуры

###### .whitePawn / Белая пешка

###### .blackPawn / Чёрная пешка

###### .anyBishop / Любой слон

###### .anyRook / Любая ладья

###### .anyKing / Любой король

###### .anyQueen / Любой ферзь

###### .anyKnight / Любой конь

##### public sealed class Board / Публичный запечатанный класс доска

###### .PieceIsAbleToAdd(Piece piece)

###### .PieceIsAbleToAddToLocation(PieceLocation location)

###### .PieceAddToLocation(Piece piece, PieceLocation location)

###### .PieceIsAbleToRemoveFromLocation(PieceLocation location)

###### .PieceRemoveFromLocation(PieceLocation location)

###### .PieceIsAbleToReadFromLocation(PieceLocation location)

###### .PieceReadFromLocation(PieceLocation location)

###### .MoveIsPossibleFromLocation(PieceLocation location)

###### .MoveIsChangingPiecePosition(PieceRelocation relocation)

###### .MoveIsChangingPiecePosition(PieceDislodgement dislodgement)

###### .MoveIsPossibleThroughSpace(PieceLocation location, PieceRelocation relocation)

###### .MoveIsPossibleThroughSpace(PieceLocation location, PieceDislodgement dislodgement)

###### .PieceIsInformedAboutRelocation(PieceLocation location, PieceRelocation relocation)

###### .PieceIsAbleToAdvanceToLocation(PieceLocation location)

###### .PieceIsInformedAboutDislodgement(PieceLocation location, PieceDislodgement dislodgement)

###### .PieceIsAbleToCaptureOnLocation(PieceLocation location)

###### .PieceIsWillingToCaptureByDislodgement(PieceLocation location, PieceDislodgement dislodgement)

###### .PieceAdvanceFromLocationByRelocation(PieceLocation oldLocation, PieceRelocation relocation)

###### .PieceCaptureFromLocationByDislodgement(PieceLocation oldLocation, PieceDislodgement dislodgement)

####
