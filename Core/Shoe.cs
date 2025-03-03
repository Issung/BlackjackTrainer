using Core.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace Core;

/// <summary>
/// A shoe is a collection of decks from which the dealer draws cards.
/// </summary>
public class Shoe(int deckCount)
{
    private readonly List<Deck> decks = Enumerable.Range(0, deckCount).Select(_ => new Deck()).ToList();

    public int DeckCount => deckCount;

    public int TotalSize => deckCount * Deck.TotalCards;

    // TODO: Find out how the cut card actually works that triggers a shuffle.
    public bool AlmostEmpty => ((float)decks.Sum(d => d.Count) / TotalSize) < almostEmptyThreshold;

    private const float almostEmptyThreshold = 0.15f;

    public Card Draw()
    {
        var deck = decks.Random();

        var card = deck.Draw();

        if (deck.Empty)
        {
            decks.Remove(deck);
        }

        return card;
    }
}
