using AdventOfCode23.Utils;
using static AdventOfCode23.Utils.FilePathUtils;

namespace AdventOfCode23.Day7;

class Hand
{
    public string Cards { get; set; } = string.Empty;
    public int Bid { get; set; } = 0;
    public HandTypes HandType { get; set; } = 0;
}
enum HandTypes
{
    HighCard,
    Pair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind,
}
public static class Day7
{
    static List<Hand> Hands = new();

    public static async Task<int> PartOne(string filename = "input.txt")
    {
        Hands.Clear();
        var totalWinnings = 0;
        
        await PopulateHandsAsync(filename);

        SortHandsByType();

        for (int i = Hands.Count; i > 0; i--)
        {
            totalWinnings += Hands[i - 1].Bid * i;
        }
        
        return totalWinnings;
    }
    
    public static async Task<int> PartTwo(string filename = "input.txt")
    {
        Hands.Clear();
        var totalWinnings = 0;
        
        await PopulateHandsAsync(filename, true);

        SortHandsByType(true);

        for (int i = Hands.Count; i > 0; i--)
        {
            totalWinnings += Hands[i - 1].Bid * i;
        }
        
        return totalWinnings;
    }
    
    private static readonly string RankOrder = "AKQJT98765432";
    private static readonly string WildRankOrder = "AKQT98765432J";

    static void SortHandsByType(bool isWild = false)
    {
        Hands.Sort((hand1, hand2) =>
        {
            // compare on hand type
            int typeComparison = hand1.HandType.CompareTo(hand2.HandType);
            if (typeComparison != 0)
                return typeComparison;

            // if they are the same, get the first highest card
            for (int i = 0; i < hand1.Cards.Length && i < hand2.Cards.Length; i++)
            {
                int rank1Index;
                int rank2Index;

                if (isWild)
                {
                    rank1Index = WildRankOrder.IndexOf(hand1.Cards[i]);
                    rank2Index = WildRankOrder.IndexOf(hand2.Cards[i]);
                }
                else
                {
                    rank1Index = RankOrder.IndexOf(hand1.Cards[i]);
                    rank2Index = RankOrder.IndexOf(hand2.Cards[i]);
                }

                if (rank1Index != rank2Index)
                    return rank2Index.CompareTo(rank1Index); // Higher rank first
            }

            // then they are exactly equal
            return 0;
        });
    }

    private static async Task PopulateHandsAsync(string filename, bool isWild = false)
    {
        try
        {
            using StreamReader sr = new($"{GetWorkingDirectory()}Day7/{filename}");
            string? line;

            while ((line = await sr.ReadLineAsync()) != null)
            {
                var lineValues = line.Split(" ");
                
                if (lineValues.Length != 2) throw new FormatException("Invalid format.");

                var hand = new Hand { Cards = lineValues[0], Bid = int.Parse(lineValues[1]) };

                hand.GetCardType(isWild);

                // Add the hand to the list
                Hands.Add(hand);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception: {e}. \nStack trace: {e.StackTrace}");
        }
    }

    
}

static class HandHelper {
    public static void GetCardType(this Hand hand, bool isWild = false)
    {
        var cardHash = new Dictionary<char, int>();

        if (!isWild)
        {
            foreach (var card in hand.Cards)
            {
                if (cardHash.ContainsKey(card))
                    cardHash[card]++;
                else
                    cardHash[card] = 1;
            }
        }
        else
        {
            foreach (var card in hand.Cards)
            {
                // skip 'J's
                if (card == 'J') continue;
                if (cardHash.ContainsKey(card))
                    cardHash[card]++;
                else
                    cardHash[card] = 1;
            }
            
            // add num of J's to max value from hashset
            var numJokers = hand.Cards.Count(c => c == 'J');

            if (cardHash.Count == 0) // hand was all jokers
            {
                cardHash['J'] = 5;
            }
            else
            {
                var highCountCard = cardHash.MaxBy(x => x.Value);
    
                if (highCountCard.Key != default)
                {
                    cardHash[highCountCard.Key] += numJokers;
                }}
        }

        var maxCardCount = 0;
        if (cardHash.Count > 0)
        {
            maxCardCount = cardHash.MaxBy(x => x.Value).Value;
        }

        hand.HandType = cardHash.Count switch
        {
            1 => HandTypes.FiveOfAKind,
            2 => maxCardCount == 4 ? HandTypes.FourOfAKind : HandTypes.FullHouse,
            3 => maxCardCount == 3 ? HandTypes.ThreeOfAKind : HandTypes.TwoPair,
            4 => HandTypes.Pair,
            5 => HandTypes.HighCard,
            _ => throw new Exception("Unexpected hand configuration")
        };
    }
}