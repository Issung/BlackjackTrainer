namespace BlackjackBasicStrategy;

enum Move { Hit, Stand, Double, Split, Surrender }

class Program
{
    static Random random = new();

    static void Main()
    {
        while (true)
        {
            PlayHand();
        }
    }

    static void PlayHand()
    {
        var playerHand = new List<int> { DrawCard(), DrawCard() };
        var dealerHand = new List<int> { DrawCard(), DrawCard() };
        var dealerUpCard = dealerHand[0];

        Console.WriteLine($"Dealer's upcard: {CardToString(dealerUpCard)}");
        Console.WriteLine($"Your hand: {string.Join(", ", playerHand.Select(CardToString))} (Total: {HandValue(playerHand)})");

        while (true)
        {
            var correctMove = GetBasicStrategyMove(playerHand, dealerUpCard);
            Console.Write("Enter your move (Hit/Stand/Double/Split/Surrender): ");
            var moveInput = Console.ReadLine()?.Trim().ToLower() ?? throw new Exception("moveInput unexpectedly null.");
            var move = Enum.Parse<Move>(moveInput, true);

            if (true)// && move == correctMove)
            {
                //Console.WriteLine("Correct move!");

                if (move == Move.Hit || move == Move.Double)
                {
                    playerHand.Add(DrawCard());

                    Console.WriteLine($"New hand: {string.Join(", ", playerHand.Select(CardToString))} (Total: {HandValue(playerHand)})");

                    var handValue = HandValue(playerHand);

                    if (handValue == 21)
                    {
                        Console.WriteLine("Blackjack! You win.");
                        return;
                    }
                    else if (handValue > 21)
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
                Console.WriteLine($"Incorrect! The correct move was: {correctMove}. Restarting...");
                return;
            }
        }

        Console.WriteLine("Dealer's turn...");
        while (HandValue(dealerHand) < 17)
        {
            var newCard = DrawCard();
            Console.WriteLine($"Dealer draws {newCard}.");
            dealerHand.Add(newCard);
        }

        Console.WriteLine($"Dealer's final hand: {string.Join(", ", dealerHand.Select(CardToString))} (Total: {HandValue(dealerHand)})");
        ResolveGame(playerHand, dealerHand);
    }

    static void ResolveGame(List<int> playerHand, List<int> dealerHand)
    {
        int playerTotal = HandValue(playerHand);
        int dealerTotal = HandValue(dealerHand);

        if (dealerTotal > 21 || playerTotal > dealerTotal)
        {
            Console.WriteLine("You win!");
        }
        else if (playerTotal < dealerTotal)
        {
            Console.WriteLine("Dealer wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }

    static int DrawCard() => random.Next(1, 11);

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

    static string CardToString(int card) => card == 1 ? "A" : card.ToString();

    static Move GetBasicStrategyMove(List<int> playerHand, int dealerUpCard)
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
    }
}
