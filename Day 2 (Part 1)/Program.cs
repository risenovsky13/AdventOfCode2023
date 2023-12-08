using System.Text.RegularExpressions;

internal class Program
{
    static int sumOfIDs;

    static void AddID(string gameSet)
    {
        string[] gameID = gameSet.Split(':');

        string lookForNumbers = @"\d+";
        Regex regex = new Regex(lookForNumbers);

        Match match = regex.Match(gameID[0]);
        sumOfIDs += Convert.ToInt32(match.Value);
    }
    static bool IsPossible(string gameSet)
    {
        bool isPossible = true;
        string[] idAndGamesSplit = gameSet.Split(':');
        string[] games = idAndGamesSplit[1].Split(';');

        List<string> cubes = new();
        for (int i = 0; i < games.Length; i++)
        {
            string[] splitCubes = games[i].Split(',');
            for (int j = 0; j < splitCubes.Length; j++)
            {
                cubes.Add(splitCubes[j]);
            }
        }

        string lookForNumbers = @"\d+";
        Regex regex = new Regex(lookForNumbers);

        for (int i = 0; i < cubes.Count; i++)
        {
            Match match = regex.Match(cubes[i]);
            int value = Convert.ToInt32(match.Value);

            if (cubes[i].Contains("red") && value > 12)
            {
                return false;
            }
            else if (cubes[i].Contains("green") && value > 13)
            {
                return false;
            }
            else if (cubes[i].Contains("blue") && value > 14)
            {
                return false;
            }
        }

        return isPossible;
    }

    static void SumPossibleGamesIDs(string gameSet)
    {
        if (IsPossible(gameSet))
        {
            AddID(gameSet);
        }
        else
        {
            return;
        }
    }

    private static void Main(string[] args)
    {
        string[] allGameSets = File.ReadAllLines("GamesList.txt");

        foreach (var gameSet in allGameSets)
        {
            SumPossibleGamesIDs(gameSet);
        }

        Console.WriteLine(sumOfIDs);
    }
}