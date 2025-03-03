namespace Core;

public record Card(Suit Suit, Rank Rank)
{
    //public override string ToString() => $"{Rank} of {Suit}";
    public override string ToString() => $"{Suit.ToString()[0]}{Rank}";
}