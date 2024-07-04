namespace Challenge.Domain.Abstractions;

public interface IGame
{
    IBetResult Play(decimal bet);
}
