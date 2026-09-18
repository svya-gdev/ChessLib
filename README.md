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

![Vectors/WhitePawnMoves.svg](Vectors/WhitePawnMoves.svg)

###### .blackPawn / Чёрная пешка

![Vectors/BlackPawnMoves.svg](Vectors/BlackPawnMoves.svg)

###### .anyBishop / Любой слон

![Vectors/AnyBishopMoves.svg](Vectors/AnyBishopMoves.svg)

###### .anyRook / Любая ладья

![Vectors/AnyRookMoves.svg](Vectors/AnyRookMoves.svg)

###### .anyKing / Любой король

![Vectors/AnyKingMoves.svg](Vectors/AnyKingMoves.svg)

###### .anyQueen / Любой ферзь

![Vectors/AnyQueenMoves.svg](Vectors/AnyQueenMoves.svg)

###### .anyKnight / Любой конь

![Vectors/AnyKnightMoves.svg](Vectors/AnyKnightMoves.svg)

##### public sealed class Board / Публичный запечатанный класс доска

❔indicates
❔

❕indicates
❕

###### .CanAddPiece(Piece piece)❔

Checks whether the `piece` can be added to **`this`** `Board`.
Проверяет, можно ли добавить `piece` на **`this`** `Board`.

Returns **`true`** if the `piece`'s `Piece.Guid` has not been added yet; otherwise, **`false`**.
Возвращает **`true`**, если `Piece.Guid` параметра `piece` еще не был добавлен; в противном случае — **`false`**.

###### .CanAddPiece(PieceLocation location)❔

Checks whether a `Piece` can be added to the `location` on **`this`** `Board`.
Проверяет, можно ли добавить `Piece` на **`this`** `Board` в указанную `location`.

Returns **`true`** if the `location` on **`this`** `Board` has not been occupied; otherwise, **`false`**.
Возвращает **`true`**, если `location` на **`this`** `Board` не оккупирована; в противном случае — **`false`**.

###### .AddPiece(Piece piece, PieceLocation location)❕

Adds the `piece` to the `location` on **`this`** `Board`.
Добавляет `piece` на **`this`** `Board`, в указанную `location`.

###### .CanRemovePiece(PieceLocation location)❔

Checks whether a `Piece` can be removed from the `location` on **`this`** `Board`.
Проверяет, можно ли убрать `Piece` с`location` на **`this`** `Board`.

Returns **`true`** if the `location` on **`this`** `Board` has been occupied by a `Piece`; otherwise, **`false`**.
Возвращает **`true`**, если `location` на **`this`** `Board` занята`Piece`; в противном случае — **`false`**.

###### .RemovePiece(PieceLocation location)❕

Removes a `Piece` from the `location` on **`this`** `Board`.
Удаляет `Piece` из `location` на **`this`** `Board`.

###### .CanReadPiece(PieceLocation location)❔

Checks whether a `Piece` can be read from the `location` on **`this`** `Board`.
Проверяет, можно ли прочитать `Piece` из `location` на **`this`** `Board`.

Returns **`true`** if the `location` on **`this`** `Board` has been occupied by a `Piece`; otherwise, **`false`**.
Возвращает **`true`**, если `location` на **`this`** `Board` занята`Piece`; в противном случае — **`false`**.

###### .ReadPiece(PieceLocation location)❕

Reads a `Piece` from the `location` on **`this`** `Board`.
Считывает `Piece` из `location` на **`this`** `Board`.

###### .CanMoveFromLocation(PieceLocation location)❔

Checks whether a `Piece` can be moved from the `location` on **`this`** `Board`.
Проверяет, можно ли переместить `Piece` с указанной `location` на **`this`** `Board`.

Returns **`true`** if the `location` on **`this`** `Board` has been occupied by a `Piece`; otherwise, **`false`**.
Возвращает **`true`**, если `location` на **`this`** `Board` занята`Piece`; в противном случае — **`false`**.

###### .IsMoveChangingLocation(PieceRelocation relocation)❔

Checks whether the `relocation` is changing a `Piece`'s `PieceLocation` on **`this`** `Board`.
Проверяет, изменяет ли `relocation` `PieceLocation` `Piece`'ы на **`this`** `Board`.

Returns **`true`** if the `relocation` is not non-moving; otherwise, **`false`**.
Возвращает **`true`**, если `relocation` не является non-moving; в противном случае — **`false`**.PieceDislodgement

There is an overload for `PieceDislodgement`. Существует перегрузка для `PieceDislodgement`.

```C#
public bool IsMoveChangingLocation(PieceDislodgement dislodgement)
{
    return IsMoveChangingLocation(dislodgement.Relocation);
}
```

###### .CanMoveThrough(PieceLocation location, PieceRelocation relocation)❔



There is an overload for `PieceDislodgement`. Существует перегрузка для `PieceDislodgement`.

```C#
public bool CanMoveThrough(PieceLocation location, PieceDislodgement dislodgement)
{
    return CanMoveThrough(location, dislodgement.Relocation);
}
```

###### .IsPieceInformed(PieceLocation location, PieceRelocation relocation)



There is an overload for `PieceDislodgement`. Существует перегрузка для `PieceDislodgement`.

```C#
// Code hidden. Код скрыт.
```

###### .PieceIsAbleToAdvanceToLocation(PieceLocation location)

###### .PieceIsAbleToCaptureOnLocation(PieceLocation location)

###### .PieceIsWillingToCaptureByDislodgement(PieceLocation location, PieceDislodgement dislodgement)

###### .PieceAdvanceFromLocationByRelocation(PieceLocation oldLocation, PieceRelocation relocation)

###### .PieceCaptureFromLocationByDislodgement(PieceLocation oldLocation, PieceDislodgement dislodgement)
