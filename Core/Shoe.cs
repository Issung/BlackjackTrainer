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

    public bool AlmostEmpty => decks.Sum(d => d.Count)

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
