namespace Chess.Domain;

public class DomainBrokenException()                 : Exception();

internal sealed class OccupationCollisionException() : DomainBrokenException();
internal sealed class OccupationAbsenceException()   : DomainBrokenException();
internal sealed class PieceRepetitionException()     : DomainBrokenException();
internal sealed class PieceCollisionException()      : DomainBrokenException();
internal sealed class PieceAbsenceException()        : DomainBrokenException();

public class RuleBrokenException() : Exception();