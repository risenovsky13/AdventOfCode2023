using System.Text.RegularExpressions;

internal class Program
{
    static List<string> cardLeft = new();
    static List<string> cardRight = new();

    static List<int[]> cardValuesLeft = new();
    static List<int[]> cardValuesRight = new();

    static int totalPoints;

    static int[] ToIntArray(string rowOfValues)
    {
        Regex regex = new(@"\d+");
        MatchCollection match = regex.Matches(rowOfValues);
        int[] values = new int[match.Count];

        for (int i = 0; i < match.Count; i++)
        {
            values[i] = int.Parse(match[i].Value);
        }
        return values;
    }

    static void CompareAndSumPoints(List<int[]> left, List<int[]> right)
    {
        for (int i = 0; i < left.Count; i++)
        {
            totalPoints += (int)Math.Pow(2,left[i].Intersect(right[i]).Count()-1);
        }
    }
    private static void Main(string[] args)
    {
        string[] cards = File.ReadAllLines("puzzle.txt");

        string patternRow = @"([0-9]+(\s+[0-9]+)+)\\|([0-9]+(\s+[0-9]+)+)";
        Regex regexToString = new(patternRow);
        MatchCollection matchString;

        foreach (var card in cards)
        {
            matchString = regexToString.Matches(card);
            for (int i = 0; i < matchString.Count; i++)
            {
                if (i + 1 > matchString.Count - 1) break;

                cardLeft.Add($"{matchString[i].Value}");
                cardRight.Add($"{matchString[i + 1].Value}");
            }
        }

        for (int i = 0; i < cardLeft.Count; i++)
        {

            cardValuesLeft.Add(ToIntArray(cardLeft[i]));
            cardValuesRight.Add(ToIntArray(cardRight[i]));
        }

        CompareAndSumPoints(cardValuesLeft, cardValuesRight);
        Console.WriteLine(totalPoints);
    }
}