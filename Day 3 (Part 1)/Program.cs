using System.Text.RegularExpressions;

internal class Program
{
    static int totalSum = 0;

    static void SumAllPartsNumbers(string[] lines)
    {
        string lookForSymbols = @"[^.\d]";
        Regex topSymbols = new Regex(lookForSymbols);
        MatchCollection matchTopSymbols = topSymbols.Matches(lines[0]);

        string lookForNumbers = @"\d+";
        Regex findNumbers = new Regex(lookForNumbers);
        MatchCollection matchNumbers = findNumbers.Matches(lines[1]);

        Regex botSymbols = new Regex(lookForSymbols);
        MatchCollection matchBotSymbols = botSymbols.Matches(lines[2]);

        for (int i = 0; i < matchNumbers.Count; i++)
        {
            int numberStartIndex = matchNumbers[i].Index - 1;
            int numberEndIndex = matchNumbers[i].Index + matchNumbers[i].Length;

            for (int j = 0; j < matchTopSymbols.Count; j++)
            {
                int symbolIndex = matchTopSymbols[j].Index;

                if (symbolIndex >= numberStartIndex && symbolIndex <= numberEndIndex)
                {
                    string partNumber = matchNumbers[i].ToString();
                    totalSum += int.Parse(partNumber);
                }
            }

            for (int j = 0; j < matchBotSymbols.Count; j++)
            {
                int symbolIndex = matchBotSymbols[j].Index;

                if (symbolIndex >= numberStartIndex && symbolIndex <= numberEndIndex)
                {
                    string partNumber = matchNumbers[i].ToString();
                    totalSum += int.Parse(partNumber);
                }
            }
        }

        Regex findSymbols = new Regex(lookForSymbols);
        MatchCollection matchSymbols = findSymbols.Matches(lines[1]);
        matchNumbers = findNumbers.Matches(lines[1]);

        for (int i = 0; i < matchNumbers.Count; i++)
        {
            int numberStartIndex = matchNumbers[i].Index - 1;
            int numberEndIndex = matchNumbers[i].Index + matchNumbers[i].Length;

            for (int j = 0; j < matchSymbols.Count; j++)
            {
                int symbolIndex = matchSymbols[j].Index;

                if (symbolIndex == numberStartIndex || symbolIndex == numberEndIndex)
                {
                    string partNumber = matchNumbers[i].ToString();
                    totalSum += int.Parse(partNumber);
                }
            }
        }

    }

    static void FirstAndLast(string[] lines)
    {
        string lookForNumbers = @"\d+";
        Regex findNumbers = new Regex(lookForNumbers);
        MatchCollection matchNumbers = findNumbers.Matches(lines[0]);

        string lookForSymbols = @"[^.\d]";
        Regex findSymbols = new Regex(lookForSymbols);
        MatchCollection matchSymbols = findSymbols.Matches(lines[1]);

        for (int i = 0; i < matchNumbers.Count; i++)
        {
            int numberStartIndex = matchNumbers[i].Index - 1;
            int numberEndIndex = matchNumbers[i].Index + matchNumbers[i].Length;

            for (int j = 0; j < matchSymbols.Count; j++)
            {
                int symbolIndex = matchSymbols[j].Index;

                if (symbolIndex >= numberStartIndex && symbolIndex <= numberEndIndex)
                {
                    string partNumber = matchNumbers[i].ToString();
                    totalSum += int.Parse(partNumber);
                }
            }
        }

        matchNumbers = findNumbers.Matches(lines[0]);
        matchSymbols = findSymbols.Matches(lines[0]);

        for (int i = 0; i < matchNumbers.Count; i++)
        {
            int numberStartIndex = matchNumbers[i].Index - 1;
            int numberEndIndex = matchNumbers[i].Index + matchNumbers[i].Length;

            for (int j = 0; j < matchSymbols.Count; j++)
            {
                int symbolIndex = matchSymbols[j].Index;

                if (symbolIndex == numberStartIndex || symbolIndex == numberEndIndex)
                {
                    string partNumber = matchNumbers[i].ToString();
                    totalSum += int.Parse(partNumber);
                }
            }
        }

    }

    private static void Main(string[] args)
    {
        string[] allLines = File.ReadAllLines("Puzzle.txt");
        string[] first = { allLines[0], allLines[1] };
        string[] last = { allLines[allLines.Length - 1], allLines[allLines.Length - 2] };

        FirstAndLast(first);
        FirstAndLast(last);

        for (int i = 0; i < allLines.Length; i++)
        {
            if(i == allLines.Length -2) break;
            SumAllPartsNumbers(new string[] {
                allLines[i],
                allLines[i+1],
                allLines[i+2]
            });
        }


        Console.WriteLine(totalSum);
    }
}

/*  
    467..114..
    ...*......
    ..35..633.
    ......#...
    617*......
    .....+.58.
    ..592.....
    ......755.
    ...$.*....
    .664.598..
*/