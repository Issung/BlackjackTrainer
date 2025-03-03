using Core;

namespace BlackjackBasicStrategy;

enum Move { Hit, Stand, Double, Split, Surrender }

class Program
{
    const int shoeDecks = 6;

    static void Main()
    {
        var shoe = new Shoe(shoeDecks);

        while (true)
        {
            PlayHand(shoe);

            if (shoe.AlmostEmpty)
            {
                Console.WriteLine("Shoe is almost empty. Restarting...");
                shoe = new Shoe(shoeDecks);
            }
        }
    }

    static void PlayHand(Shoe shoe)
    {
        var playerHand = new Hand(shoe);
        var dealerHand = new Hand(shoe);
        var dealerUpCard = dealerHand.Cards[0];

        Console.WriteLine($"Dealer's upcard: {dealerUpCard}");
        Console.WriteLine($"Your hand: {string.Join(", ", playerHand.Cards)} (Total: {playerHand.Value})");

        while (true)
        {
            //var correctMove = GetBasicStrategyMove(playerHand, dealerUpCard);
            Console.Write("Enter your move (Hit/Stand/Double/Split/Surrender): ");
            var moveInput = Console.ReadLine()?.Trim().ToLower() ?? throw new Exception("moveInput unexpectedly null.");
            var move = Enum.Parse<Move>(moveInput, true);

            if (true)// && move == correctMove)
            {
                //Console.WriteLine("Correct move!");

                if (move == Move.Hit || move == Move.Double)
                {
                    playerHand.Add(shoe.Draw());

                    Console.WriteLine($"New hand: {string.Join(", ", playerHand.Cards)} (Total: {playerHand.Value})");

                    if (playerHand.Value == 21)
                    {
                        Console.WriteLine("Blackjack! You win.");
                        return;
                    }
                    else if (playerHand.Value > 21)
                    {
                        Console.WriteLine("Bust! You lose.");
                        return;
                    }

                    if (move == Move.Double)
                    {
                        break;
                    }
                }
                else if (move == Move.Stand)
                {
                    break;
                }
            }
            else
            {
                //Console.WriteLine($"Incorrect! The correct move was: {correctMove}. Restarting...");
                return;
            }
        }

        Console.WriteLine("Dealer's turn...");
        while (dealerHand.Value < 17)
        {
            var newCard = shoe.Draw();
            Console.WriteLine($"Dealer draws {newCard}.");
            dealerHand.Add(newCard);
        }

        Console.WriteLine($"Dealer's final hand: {string.Join(", ", dealerHand.Cards)} (Total: {dealerHand.Value})");
        ResolveGame(playerHand, dealerHand);
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
        Console.Clear();
    }

    static void ResolveGame(Hand player, Hand dealer)
    {
        if (dealer.Value > 21 || player.Value > dealer.Value)
        {
            Console.WriteLine("You win!");
        }
        else if (player.Value < dealer.Value)
        {
            Console.WriteLine("Dealer wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }

    static int HandValue(List<int> hand)
    {
        int sum = hand.Sum();
        int aceCount = hand.Count(c => c == 1);
        while (aceCount > 0 && sum + 10 <= 21)
        {
            sum += 10;
            aceCount--;
        }
        return sum;
    }

    /*static Move GetBasicStrategyMove(List<int> playerHand, int dealerUpCard)
    {
        int playerTotal = HandValue(playerHand);
        bool isSoft = playerHand.Contains(1) && playerTotal <= 21;
        bool canSplit = playerHand.Count == 2 && playerHand[0] == playerHand[1];

        if (canSplit)
        {
            if (playerHand[0] == 1 || playerHand[0] == 8) return Move.Split;
            if (playerHand[0] == 9 && dealerUpCard is not (7 or 10 or 1)) return Move.Split;
            if (playerHand[0] == 7 && dealerUpCard <= 7) return Move.Split;
            if (playerHand[0] == 6 && dealerUpCard <= 6) return Move.Split;
            if (playerHand[0] == 4 && dealerUpCard is (5 or 6)) return Move.Split;
            if (playerHand[0] == 3 || playerHand[0] == 2) return dealerUpCard <= 7 ? Move.Split : Move.Hit;
            if (playerHand[0] == 5) return dealerUpCard is (2 or 9 or 10 or 1) ? Move.Hit : Move.Double;
        }

        if (isSoft)
        {
            if (playerTotal >= 19) return Move.Stand;
            if (playerTotal == 18)
            {
                if (dealerUpCard <= 6) return Move.Double;
                if (dealerUpCard is (9 or 10 or 1)) return Move.Hit;
                return Move.Stand;
            }
            if (playerTotal == 17) return dealerUpCard <= 6 ? Move.Double : Move.Hit;
            if (playerTotal == 16 || playerTotal == 15) return dealerUpCard is (4 or 5 or 6) ? Move.Double : Move.Hit;
            if (playerTotal == 14 || playerTotal == 13) return dealerUpCard is (5 or 6) ? Move.Double : Move.Hit;
        }

        if (playerTotal >= 17) return Move.Stand;
        if (playerTotal == 16)
        {
            if (dealerUpCard >= 9) return Move.Surrender;
            return dealerUpCard <= 6 ? Move.Stand : Move.Hit;
        }
        if (playerTotal == 15 && dealerUpCard == 10) return Move.Surrender;
        if (playerTotal >= 13 && dealerUpCard >= 2 && dealerUpCard <= 6) return Move.Stand;
        if (playerTotal == 12 && dealerUpCard is (4 or 5 or 6)) return Move.Stand;
        if (playerTotal == 11) return Move.Double;
        if (playerTotal == 10) return dealerUpCard <= 9 ? Move.Double : Move.Hit;
        if (playerTotal == 9) return dealerUpCard is (3 or 4 or 5 or 6) ? Move.Double : Move.Hit;
        return Move.Hit;
    }*/
}
