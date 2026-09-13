#### Minor types / Малые типы

Self-explanatory structures and classes.
Структуры и классы, не требующие пояснений.

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

Modules and classes requiring explanation.
Модули и классы, требующие пояснения.

##### module public StandardPieces / Публичный модуль стандартные фигуры

Contains `PieceDevelopment`s for creating standard `Piece`s.
Содержит `PieceDevelopment`'ы для создания стандартных `Piece`.

Green indicates which `PieceRelocation`s are included in `Piece.Advances`.
Зеленый показывает, какие `PieceRelocation`'ы включены в `Piece.Advances`.

Red indicates which `PieceDislodgement`s are included in `Piece.Captures`.
Красный показывает, какие `PieceDislodgement`'ы включены в `Piece.Captures`.

###### .whitePawn / Белая пешка

![Vectors/WhitePawnMoves.svg](https://github.com/svya-gdev/ChessLib/blob/master/Vectors/WhitePawnMoves.svg)

###### .blackPawn / Чёрная пешка

![Vectors/BlackPawnMoves.svg](https://github.com/svya-gdev/ChessLib/blob/master/Vectors/BlackPawnMoves.svg)

###### .anyBishop / Любой слон

![Vectors/AnyBishopMoves.svg](https://github.com/svya-gdev/ChessLib/blob/master/Vectors/AnyBishopMoves.svg)

###### .anyRook / Любая ладья

![Vectors/AnyRookMoves.svg](https://github.com/svya-gdev/ChessLib/blob/master/Vectors/AnyRookMoves.svg)

###### .anyKing / Любой король

![Vectors/AnyKingMoves.svg](https://github.com/svya-gdev/ChessLib/blob/master/Vectors/AnyKingMoves.svg)

###### .anyQueen / Любой ферзь

![Vectors/AnyQueenMoves.svg](https://github.com/svya-gdev/ChessLib/blob/master/Vectors/AnyQueenMoves.svg)

###### .anyKnight / Любой конь

![Vectors/AnyKnightMoves.svg](https://github.com/svya-gdev/ChessLib/blob/master/Vectors/AnyKnightMoves.svg)

##### public sealed class Board / Публичный запечатанный класс доска

###### .CanAddPiece(Piece piece)

Returns **`true`** if the `piece` can be added; otherwise, **`false`**.
Возвращает **`true`**, если `piece` можно добавить; в противном случае — **`false`**.

###### .CanAddPiece(PieceLocation location)

Returns **`true`** if a `Piece` can be added to the `location`; otherwise, **`false`**.
Возвращает **`true`**, если `Piece` можно добавить в `location`; в противном случае — **`false`**.

###### .AddPiece(Piece piece, PieceLocation location)

Adds the `piece` to **`this`** `Board` if can add to the `location`; otherwise, throws.
Добавляет `piece` на **`this`** `Board`, если может добавить в `location`; в противном случае выбрасывает исключение.

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
