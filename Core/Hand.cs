using System.Collections.Generic;
using System.Linq;

namespace Core;

public class Hand
{
    public IReadOnlyList<Card> Cards => cards;

    private readonly List<Card> cards = [];

    public void Add(Card card) => cards.Add(card);

    public bool CanSplit => cards.Count == 2 && cards[0].Equals(cards[1]);

    public Hand(Shoe shoe)
    {
        Add(shoe.Draw());
        Add(shoe.Draw());
    }

    /// <summary>
    /// Value (with Aces counting as 11).
    /// </summary>
    public int Value
    {
        get
        {
            int sum = cards.Sum(c => c.Rank switch
            {
                Rank.Ace => 1,
                Rank.Two => 2,
                Rank.Three => 3,
                Rank.Four => 4,
                Rank.Five => 5,
                Rank.Six => 6,
                Rank.Seven => 7,
                Rank.Eight => 8,
                Rank.Nine => 9,
                Rank.Ten or Rank.Jack or Rank.Queen or Rank.King => 10,
                _ => throw new System.Exception("Invalid rank")
            });

            var aceCount = cards.Count(c => c.Rank == Rank.Ace);

            while (aceCount > 0 && sum + 10 <= 21)
            {
                sum += 10;  // An ace is worth 11, we already added 1 from the sum previously.
                aceCount--;
            }

            return sum;
        }
    }
}
