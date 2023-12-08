using System.IO;

internal class Program
{
    public static int SumOfAllValues;

    public static int FindFirstAndLastDigit(string line)
    {
        string pairOfDigits = string.Empty;

        for (int i = 0; i < line.Length; i++)
        {
            bool isDigit = Char.IsNumber(line[i]);

            if (isDigit)
            {
                pairOfDigits = Char.ToString(line[i]);
                break;
            }
        }

        for (int i = line.Length - 1; i >= 0; i--)
        {
            bool isDigit = Char.IsNumber(line[i]);

            if (isDigit)
            {
                pairOfDigits += Char.ToString(line[i]);
                break;
            }
        }

        int values;
        return values = Convert.ToInt32(pairOfDigits);
    }

    public static int AddValues
    {
        set
        {
            SumOfAllValues += value;
        }
    }

    public static int DisplaySum
    {
        get
        {
            return SumOfAllValues;
        }
    }

    private static void Main(string[] args)
    {
        string calibrationDocPath = "CalibrationDoc.txt";

        string[] allLines = File.ReadAllLines(calibrationDocPath); 

        for (int i = 0; i < allLines.Length; i++)
        {
            AddValues = FindFirstAndLastDigit(allLines[i]);
        }

        Console.WriteLine(SumOfAllValues);
    }
}