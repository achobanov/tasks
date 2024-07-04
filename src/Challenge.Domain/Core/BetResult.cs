using Challenge.Domain.Abstractions;

namespace Challenge.Domain.Core;

public readonly record struct BetResult(decimal Delta, string Message) : IBetResult;