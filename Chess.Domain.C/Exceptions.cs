namespace Chess.Domain;

public        class DomainBrokenException()        : Exception();

public sealed class OccupationCollisionException() : DomainBrokenException();
public sealed class OccupationAbsenceException()   : DomainBrokenException();

public sealed class PieceCollisionException()      : DomainBrokenException();
public sealed class PieceAbsenceException()        : DomainBrokenException();
public sealed class PieceRepetitionException()     : DomainBrokenException();



public        class RuleBrokenException()                         : Exception();

public sealed class PieceAlreadyAddedSomewhereException()         : RuleBrokenException();
public sealed class LocationAlreadyOccupiedBySomethingException() : RuleBrokenException();
public sealed class LocationAlreadyNotOccupiedByPieceException()  : RuleBrokenException();

public sealed class PieceNotFoundException()                      : RuleBrokenException();

public sealed class NoPieceToMoveException()                      : RuleBrokenException();
public sealed class NoMoveToApplyException()                      : RuleBrokenException();
public sealed class NoSpaceToMoveException()                      : RuleBrokenException();

public sealed class RelocationUnknownException()                  : RuleBrokenException();
public sealed class NewLocationOccupiedBySomethingException()     : RuleBrokenException();

public sealed class DislodgementUnknownException()                : RuleBrokenException();
public sealed class NewLocationNotOccupiedByPieceException()      : RuleBrokenException();
public sealed class NoWillToCaptureException()                    : RuleBrokenException();
