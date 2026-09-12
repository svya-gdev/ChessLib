namespace Chess.Domain;

public        class DomainBrokenException()        : Exception();

public sealed class OccupationCollisionException() : DomainBrokenException();
public sealed class OccupationAbsenceException()   : DomainBrokenException();

public sealed class PieceCollisionException()      : DomainBrokenException();
public sealed class PieceAbsenceException()        : DomainBrokenException();
public sealed class PieceRepetitionException()     : DomainBrokenException();



public        class RuleBrokenException()                         : Exception();

public sealed class PieceAlreadyAddedSomewhereException()         : Exception();
public sealed class LocationAlreadyOccupiedBySomethingException() : Exception();
public sealed class LocationAlreadyNotOccupiedByPieceException()  : Exception();

public sealed class PieceNotFoundException()                      : Exception();

public sealed class NoPieceToMoveException()                      : Exception();
public sealed class NoMoveToApplyException()                      : Exception();
public sealed class NoSpaceToMoveException()                      : Exception();

public sealed class RelocationUnknownException()                  : Exception();
public sealed class NewLocationOccupiedBySomethingException()     : Exception();

public sealed class DislodgementUnknownException()                : Exception();
public sealed class NewLocationNotOccupiedByPieceException()      : Exception();
public sealed class NoWillToCaptureException()                    : Exception();