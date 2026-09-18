namespace Chess.Domain;

public        class DomainBrokenException()        : Exception();

public sealed class OccupationCollisionException() : DomainBrokenException();
public sealed class OccupationAbsenceException()   : DomainBrokenException();

public sealed class PieceCollisionException()      : DomainBrokenException();
public sealed class PieceAbsenceException()        : DomainBrokenException();
public sealed class PieceRepetitionException()     : DomainBrokenException();



public class PieceBoardRuleBrokenException() : Exception();

public sealed class IDDuplicationException() : PieceBoardRuleBrokenException();
public sealed class PieceAdditionException() : PieceBoardRuleBrokenException();
public sealed class PieceDeletionException() : PieceBoardRuleBrokenException();
public sealed class PieceReadmentException() : PieceBoardRuleBrokenException();
public sealed class NoMoveToApplyException() : PieceBoardRuleBrokenException();
public sealed class NoPieceToMoveException() : PieceBoardRuleBrokenException();
public sealed class CannotAdvanceException() : PieceBoardRuleBrokenException();
public sealed class AdvanceUntoldException() : PieceBoardRuleBrokenException();
public sealed class NoneToCaptureException() : PieceBoardRuleBrokenException();
public sealed class WillNotAttackException() : PieceBoardRuleBrokenException();
public sealed class CaptureUntoldException() : PieceBoardRuleBrokenException();
public sealed class NoSpaceToMoveException() : PieceBoardRuleBrokenException();