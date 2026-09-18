namespace Chess.Domain;

// Фигура ( Создание и улучшение фигуры , Команда )
public sealed class Piece(PieceDevelopment development, Team team)
{
    public readonly Guid Guid = Guid.NewGuid(); // Гуид
    public readonly Team Team = team;           // Команда

    private readonly HashSet<PieceRelocation>   Advances
        = development.Advances.ToHashSet();     // Продвижения
    private readonly HashSet<PieceDislodgement> Captures
        = development.Captures.ToHashSet();     // Взятия

    public bool ContainsAdvance(PieceRelocation relocation)
        => Advances.Contains(relocation);       // Есть ли продвижение?
    public bool ContainsCapture(PieceDislodgement dislodgement)
        => Captures.Contains(dislodgement);     // Есть ли взятие?
}