using System.Collections.Generic;
using System.Linq;

namespace Core;

public class Hand
{
    public List<Card> Cards { get; } = [];

    public void Add(Card card) => Cards.Add(card);

    public bool CanSplit => Cards.Count == 2 && Cards[0].Equals(Cards[1]);

    /// <summary>
    /// Value (with Aces counting as 11).
    /// </summary>
    public int Value
    {
        get
        {
            int sum = Cards.Sum(c => c.Rank switch
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

            var aceCount = Cards.Count(c => c.Rank == Rank.Ace);

            while (aceCount > 0 && sum + 10 <= 21)
            {
                sum += 10;  // An ace is worth 11, we already added 1 from the sum previously.
                aceCount--;
            }

            return sum;
        }
    }
}
