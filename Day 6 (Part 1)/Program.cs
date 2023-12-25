using System.Numerics;
using System.Text.RegularExpressions;

internal class Program
{
    public static BigInteger[] GetValues(string line)
    {
        Regex regex = new(@"\d\d*");
        MatchCollection match = regex.Matches(line);
        BigInteger[] values = new BigInteger[match.Count];

        for (int i = 0; i < values.Length; i++)
        {
            values[i] = BigInteger.Parse(match[i].Value);
        }
        
        return values;
    }

    public static int GetNumberOfWays(BigInteger time, BigInteger record)
    {
        List<BigInteger> distance = new();
        BigInteger speed;

        for (int i = 1; i < time; i++)
        {
            speed = i;
            distance.Add(speed * (time - speed));
        }

        return distance.Count(x => x > record);
    }

    private static void Main(string[] args)
    {
        string[] puzzle = File.ReadAllLines("puzzle.txt");
        BigInteger[] time = GetValues(puzzle[0]);
        BigInteger[] distance = GetValues(puzzle[1]);
        List<int> NumberOfWays = new();

        for (int i = 0; i < time.Length; i++)
        {
            NumberOfWays.Add(GetNumberOfWays(time[i], distance[i]));
        }
        // int marginOfError = NumberOfWays.Aggregate((x, y) => x * y);
    }
}