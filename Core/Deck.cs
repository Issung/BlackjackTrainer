using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core;

/// <summary>
/// A randomized deck of cards.
/// </summary>
public class Deck
{
    /// <summary>
    /// The total number of cards in a fresh deck (4 suites * 13 ranks).
    /// </summary>
    public const int TotalCards = 52;

    public bool Empty => cards.Count == 0;

    public int Count => cards.Count;

    private readonly List<Card> cards = InitializeDeck();

    public Card Draw()
    {
        var card = cards[0];
        cards.RemoveAt(0);
        return card;
    }

    private static List<Card> InitializeDeck()
    {
        return EnumHelper
            .GetValues<Suit>()
            .SelectMany(suit => EnumHelper.GetValues<Rank>().Select(rank => new Card(suit, rank)))
            .OrderBy(c => Guid.NewGuid())
            .ToList();
    }
}
