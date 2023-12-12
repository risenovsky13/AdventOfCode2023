using System.Text.RegularExpressions;

internal class Program
{
    static int[] instances = new int[File.ReadAllLines("puzzle.txt").Count()];
    static int[] matchesQuantity = new int[instances.Length];
    static int total;

    static void CountInstances(int matches, int index)
    {
        index++;
        if (index > instances.Length - 1) return;

        for (int i = index; i < matches + index; i++)
        {
            if (i > instances.Length - 1) return;
            instances[i]++;
            if (matchesQuantity[i] > 0)
            {
                CountInstances(matchesQuantity[i], i);
            }
        }
    }
    public static void Main(string[] args)
    {
        string[] cards = File.ReadAllLines("puzzle.txt");
        int[] cardsMatchingNumbers = new int[cards.Length];

        Regex regexRows = new(@"([0-9]+(\s+[0-9]+)+)\\|([0-9]+(\s+[0-9]+)+)");
        MatchCollection match;

        for (int i = 0; i < cards.Length; i++)
        {
            match = regexRows.Matches(cards[i]);
            for (int j = 0; j < 1; j++)
            {
                Regex regexNumbers = new(@"\d+");
                MatchCollection leftRow = regexNumbers.Matches(match[0].Value);
                MatchCollection rightRow = regexNumbers.Matches(match[1].Value);

                int[] left = new int[leftRow.Count];
                int[] right = new int[rightRow.Count];

                for (int k = 0; k < left.Length; k++)
                {
                    left[k] = int.Parse(leftRow[k].Value);
                }

                for (int k = 0; k < right.Length; k++)
                {
                    right[k] = int.Parse(rightRow[k].Value);
                }

                cardsMatchingNumbers[i] = left.Intersect(right).Count();
            }
        }

        matchesQuantity = cardsMatchingNumbers;

        for (int i = 0; i < instances.Length; i++)
        {
            instances[i]++;
        }

        for (int i = 0; i < matchesQuantity.Length; i++)
        {
            CountInstances(matchesQuantity[i], i);
        }

        int count = 1;
        foreach (var item in instances)
        {
            Console.WriteLine($"Card {count}:  {item}");
            count++;
        }

        foreach (var amount in instances)
        {
            total += amount;
        }

        Console.WriteLine($"total {total}");

    }
}

/*  
    Card 1: 4
    Card 2: 2
    Card 3: 2
    Card 4: 1
    Card 5: null
    Card 6: null

    1 {
        2{
            3{
                4{
                    5
                }
                5
            }
            4{
                5
            }
        }
        3{
            4{
                5
            }
            5
        }
        4{
            5
        }
        5
    }


*/