using System.Text.RegularExpressions;

internal class Program
{
    static int totalSum;

    static void SumOfThePower(string line)
    {
        string newLine = line.Split(':')[1];
        string[] cubes = newLine.Split(';', ',');

        int red = 0;
        int green = 0;
        int blue = 0;

        string pattern = @"\d+";
        Regex regex = new Regex(pattern);

        Match match;

        for (int i = 0; i < cubes.Length; i++)
        {
            match = regex.Match(cubes[i]);

            if (cubes[i].Contains("red") && red <= Convert.ToInt32(match.Value))
            {
                red = Convert.ToInt32(match.Value);
            }
            else if (cubes[i].Contains("green") && green <= Convert.ToInt32(match.Value))
            {
                green = Convert.ToInt32(match.Value);
            }
            else if (cubes[i].Contains("blue") && blue <= Convert.ToInt32(match.Value))
            {
                blue = Convert.ToInt32(match.Value);
            }
        }

        totalSum += (red * green * blue);
    }

    private static void Main(string[] args)
    {
        string[] allLines = File.ReadAllLines("Puzzle.txt");

        foreach (var line in allLines)
        {
            SumOfThePower(line);
        }

        Console.WriteLine($"totalSum => {totalSum}");
    }
}