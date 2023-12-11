using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

struct Number
{
    public int value;
    public int index;
    public int lenght;
    public List<string> coordinates = new();
    public Number(int value, int index, int lenght, List<string> coordinates)
    {
        this.value = value;
        this.index = index;
        this.lenght = lenght;
        this.coordinates = coordinates;
    }
}
struct Symbol
{
    public string value;
    public int index;
    public int lenght;
    public List<string> coordinates = new();
    public Symbol(string value, int index, int lenght, List<string> coordinates)
    {
        this.value = value;
        this.index = index;
        this.lenght = lenght;
        this.coordinates = coordinates;
    }
}
struct FoundNumbers
{
    public int value;
    public string coordinates;
    public FoundNumbers(int value, string coordinates)
    {
        this.value = value;
        this.coordinates = coordinates;
    }
}
/* ################################################################################# */
internal class Program
{
    static int totalSum = 0;
    public static List<Number> ListOfNumbers = new();
    public static List<Symbol> ListOfSymbols = new();
    public static List<FoundNumbers> NumbersWithStars = new();
    /*#################################################################################*/
    static void GetListOfAllNumbers(string pattern, string[] lines)
    {
        Regex regex = new(pattern);
        MatchCollection match;
        for (int currentLine = 0; currentLine < lines.Length; currentLine++)
        {
            match = regex.Matches(lines[currentLine]);
            for (int i = 0; i < match.Count; i++)
            {
                Number number = new
                (
                    int.Parse(match[i].Value),
                    match[i].Index,
                    match[i].Length,
                    GetNumberCoordinates(match[i], currentLine)
                );
                ListOfNumbers.Add(number);
            }
        }
    }
    static void GetListOfAllStars(string pattern, string[] lines)
    {
        Regex regex = new(pattern);
        MatchCollection match;
        for (int currentLine = 0; currentLine < lines.Length; currentLine++)
        {
            match = regex.Matches(lines[currentLine]);
            for (int i = 0; i < match.Count; i++)
            {
                Symbol symbol = new
                (
                    match[i].Value,
                    match[i].Index,
                    match[i].Length,
                    GetSymbolCoordinates(match[i], currentLine)
                );
                ListOfSymbols.Add(symbol);
            }
        }
    }
    /*#################################################################################*/
    static List<string> GetNumberCoordinates(Match match, int currentLine)
    {
        List<string> coordinates = new();

        for (int i = currentLine - 1; i < currentLine + 2; i++)
        {
            for (int j = match.Index -1; j < match.Index + match.Length +1 ; j++)
            {
                coordinates.Add($"X {j}: Y {i}");
            }
        }
        return coordinates;
    }
    static List<string> GetSymbolCoordinates(Match match, int currentLine)
    {
        List<string> coordinates = new();
        for (int i = match.Index; i < match.Length + match.Index; i++)
        {
            coordinates.Add($"X {i}: Y {currentLine}");
        }
        return coordinates;
    }
    /*#################################################################################*/
    static void SumOfAllValidNumbers()
    {
        Number number = new();
        Symbol symbol = new();

        for (int i = 0; i < ListOfNumbers.Count; i++)
        {
            number = ListOfNumbers[i];
            for (int j = 0; j < ListOfSymbols.Count; j++)
            {
                symbol = ListOfSymbols[j];
                if(number.coordinates.Any(symbol.coordinates.Contains))
                {
                    FoundNumbers foundNumber = new
                    (
                        number.value,
                        symbol.coordinates[0]
                    );
                    NumbersWithStars.Add(foundNumber);
                }
            }
        }

        for (int i = 0; i < NumbersWithStars.Count; i++)
        {
            var currentNumber = NumbersWithStars[i];
            for (int j = 0; j < NumbersWithStars.Count; j++)
            {
                var comparedNumber = NumbersWithStars[j];
                if(currentNumber.coordinates.Equals(comparedNumber.coordinates)
                && currentNumber.value != comparedNumber.value)
                {
                    totalSum += currentNumber.value * comparedNumber.value; 
                }
            }
        }
    }
    private static void Main(string[] args)
    {
        string[] lines = File.ReadAllLines("Puzzle.txt");
        GetListOfAllNumbers(@"\d+", lines);
        GetListOfAllStars(@"\*", lines);
        SumOfAllValidNumbers();

        Console.WriteLine(totalSum / 2);
    }
}
