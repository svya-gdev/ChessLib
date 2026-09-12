#### Minor types / Малые типы

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

##### StandardPieces / Стандартные фигуры

###### .whitePawn / Белая пешка

###### .blackPawn / Чёрная пешка

###### .anyBishop / Любой слон

###### .anyRook / Любая ладья

###### .anyKing / Любой король

###### .anyQueen / Любой ферзь

###### .anyKnight / Любой конь

##### Board 




# OutDated




##### Team { White, Black }
Описывает две команды: белые и черные.

```F#
[<Struct>]
type public Team =
    | White
    | Black
```

###### .Opposite
Команда-противник. Для White — Black, и наоборот.

```F#
member public this.Opposite =
    match this with
    | White -> Black
    | Black -> White
```

##### Spite { WithOppositeTeam, WithEveryTeam }
Описывает кого может атаковать команда.

>[!WARNING]
> Имена громоздкие или не понятны интуитивно, а значит могут быть изменены в следующих версиях.

>[!NOTE]
> В следующих версиях может быть добавлено больше команд и их взаимоотношений.

```F#
[<Struct>]
type public Spite =
    | WithOppositeTeam
    | WithEveryTeam
```

###### .IsFromTeamToTeam(subject, object)
Определяет, враждебна ли команда `subject` по отношению к `object`, согласно этому значению `Spite`.

> [!WARNING]
> Имена громоздкие или не понятны интуитивно, а значит могут быть изменены в следующих версиях.

```F#
member public this.IsFromTeamToTeam(subject : Team, object : Team) =
    match this with
    | WithOppositeTeam -> subject.Opposite = object
    | WithEveryTeam -> true
```

##### PieceLocation(File, Rank)
Описывает положение фигуры на доске.

```F#
[<Struct>]
type public PieceLocation = {
    File: uint32
    Rank: uint32
}
```

##### PieceRelocation(FileDelta, RankDelta)
Описывает смещение фигуры с одной `PieceLocation(File, Rank)` к другой.

> [!NOTE]
> Отрицательные смещения кодируются через переполнение беззнакового `uint32` (например, смещение -1 хранится как `UInt32.MaxValue`). 

>[!CAUTION]
> Не собирайте `PieceRelocation` вручную — используйте готовые фигуры из `ClassicPieces`.

```F#
[<Struct>]
type public PieceRelocation = {
    FileDelta: uint32
    RankDelta: uint32
}
```
###### .ApplyTo(PieceLocation(File, Rank))
Возвращает клетку, в которой окажется фигура после применения этого смещения к исходной позиции.

```F#
member public this.ApplyTo(location) = {
    File = location.File + this.FileDelta
    Rank = location.Rank + this.RankDelta
}
```

###### .IsHorseLike
Истинно, если смещение имеет форму хода коня — обе оси ненулевые и не равны друг другу. Для таких смещений фигура перепрыгивает через промежуточные клетки, не проверяя их на занятость.

```F#
member public this.IsHorseLike =
    this.FileDelta <> this.RankDelta &&
    this.FileDelta <> 0u &&
    0u <> this.RankDelta
```

###### .IsStraight
Истинно, если смещение идёт по прямой линии — вдоль одной оси (горизонталь/вертикаль) либо строго по диагонали. Для таких смещений промежуточные клетки между исходной позицией и целью должны быть свободны.

```F#
member public this.IsStraight =
    this.FileDelta = this.RankDelta ||
    this.FileDelta = 0u ||
    0u = this.RankDelta
```

###### .IsNonMoving
Истинно только для нулевого смещения (`FileDelta = 0`, `RankDelta = 0`) — то есть когда фигура фактически остаётся на месте. Используется, чтобы отличить "не двигается" от смещений, которые двигают фигуру хотя бы на одну клетку.

```F#
member public this.IsNonMoving =
    this.FileDelta = 0u &&
    this.RankDelta = 0u
```

##### PieceDislodgement(Relocation, Spite)
Описывает одно конкретное взятие: куда сдвигается атакующая фигура (`Relocation`) и против каких команд это взятие применимо (`Spite`).

>[!WARNING]
> Имена громоздкие или не понятны интуитивно, а значит могут быть изменены в следующих версиях.

```F#
[<Struct>]
type public PieceDislodgement = {
    Relocation : PieceRelocation
    Spite : Spite
}
```

##### PieceDevelopment(Advances, Captures)
Полный набор ходов фигуры: все смещения, на которые она может продвинуться без взятия (`Advances`), и все взятия, которые она может совершить (`Captures`). Готовые наборы для классических фигур — в `ClassicPieces`.

```F#
[<Struct>]
type public PieceDevelopment = {
    Advances : Set<PieceRelocation>
    Captures : Set<PieceDislodgement>
}
```
