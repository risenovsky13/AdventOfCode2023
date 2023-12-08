internal class Program
{
    static int sumOfAllNumbers;
    public enum WordsToNumbers
    {
        one = 1,
        two = 2,
        three = 3,
        four = 4,
        five = 5,
        six = 6,
        seven = 7,
        eight = 8,
        nine = 9
    }

    public static List<string> numbersAndLetters = new List<string>()
    {
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
    };

    static string FindFirst(string line)
    {
        string firstDigit = string.Empty;
        List<int> indexes = new List<int>();
        int lowestIndex = line.Length;

        for (int i = 0; i < numbersAndLetters.Count; i++)
        {
            indexes.Add(line.IndexOf(numbersAndLetters[i]));
        }

        for (int i = 0; i < indexes.Count; i++)
        {
            if (indexes[i] != -1 && lowestIndex > indexes[i])
            {
                lowestIndex = indexes[i];
            }
        }

        for (int i = 0; i < numbersAndLetters.Count; i++)
        {
            if (lowestIndex == indexes[i])
            {
                firstDigit = numbersAndLetters[i];
            }
        }

        if (firstDigit.Length == 1)
        {
            return firstDigit;
        }
        else
        {
            Enum.TryParse<WordsToNumbers>(firstDigit, true, out WordsToNumbers result);
            firstDigit = ((int)result).ToString();
        }

        return firstDigit;
    }

    static string FindLast(string line)
    {
        string lastDigit = string.Empty;
        List<int> indexes = new List<int>();
        int bigestIndex = 0;

        for (int i = 0; i < numbersAndLetters.Count; i++)
        {
            indexes.Add(line.LastIndexOf(numbersAndLetters[i]));
        }

        for (int i = 0; i < indexes.Count; i++)
        {
            if (bigestIndex < indexes[i])
            {
                bigestIndex = indexes[i];
            }
        }

        for (int i = 0; i < numbersAndLetters.Count; i++)
        {
            if (bigestIndex == indexes[i])
            {
                lastDigit = numbersAndLetters[i];
            }
        }

        if (lastDigit.Length == 1)
        {
            return lastDigit;
        }
        else
        {
            Enum.TryParse<WordsToNumbers>(lastDigit, true, out WordsToNumbers result);
            lastDigit = ((int)result).ToString();
        }

        return lastDigit;
    }

    static string FindFirstAndLastDigit(string line)
    {
        string pairOfDigits = string.Empty;

        pairOfDigits += FindFirst(line);
        pairOfDigits += FindLast(line);

        sumOfAllNumbers += Convert.ToInt32(pairOfDigits);


        Console.WriteLine(pairOfDigits);
        return pairOfDigits;
    }

    private static void Main(string[] args)
    {
        // string line = "eight6twojtzlvlhgjncvx";
        // FindFirstAndLastDigit(line);

        string[] allLines = File.ReadAllLines("CalibrationDoc.txt");

        foreach (var line in allLines)
        {
            FindFirstAndLastDigit(line);
        }

        Console.WriteLine($"sum => {sumOfAllNumbers}");
    }
}

/* 
    two1nine
    eightwothree
    abcone2threexyz
    xtwone3four
    4nineeightseven2
    zoneight234
    7pqrstsixteen

    1oneninegspfm3four43
 */

/* 
   error 87
 */