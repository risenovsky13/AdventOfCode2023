using System.Text.RegularExpressions;
using System.Linq;

public static class Extension
{
    public static bool InRange(this long value, long minValue, long maxValue)
    {
        return value >= minValue && value <= maxValue;
    }
}
internal class Program
{
    #region Lists
    static string[] file = File.ReadAllLines("puzzle.txt");
    static List<long> seed = new(ToListInt(file[0]));
    static List<long> seedToSoil = new(ToListInt(GetMapValues(file, @"seed-to-soil map:")));
    static List<long> soilToFertilizer = new(ToListInt(GetMapValues(file, @"soil-to-fertilizer map:")));
    static List<long> fertilizerToWater = new(ToListInt(GetMapValues(file, @"fertilizer-to-water map:")));
    static List<long> waterToLight = new(ToListInt(GetMapValues(file, @"water-to-light map:")));
    static List<long> lightToTemperature = new(ToListInt(GetMapValues(file, @"light-to-temperature map:")));
    static List<long> temperatureToHumidity = new(ToListInt(GetMapValues(file, @"temperature-to-humidity map:")));
    static List<long> humidityTLocation = new(ToListInt(GetMapValues(file, @"humidity-to-location map:")));
    #endregion
    static List<List<long>> maps = new()
    {
        seedToSoil,
        soilToFertilizer,
        fertilizerToWater,
        waterToLight,
        lightToTemperature,
        temperatureToHumidity,
        humidityTLocation
    };
    static List<long> ToListInt(string values)
    {
        List<long> list = new();
        Regex regex = new(@"\d+");
        MatchCollection match = regex.Matches(values);

        for (int i = 0; i < match.Count; i++)
        {
            list.Add(long.Parse(match[i].Value));
        }

        return list;
    }
    static string GetMapValues(string[] file, string pattern)
    {
        string map = string.Empty;
        Regex stop = new("^[a-z]+");
        Regex regex = new(pattern);
        Match match;
        Match matchStop;

        for (int i = 0; i < file.Length; i++)
        {
            match = regex.Match(file[i]);
            if (match.Success)
            {
                for (int j = i + 1; j < file.Length; j++)
                {
                    matchStop = stop.Match(file[j]);
                    regex = new(@"\d+(?: \d+)*");
                    match = regex.Match(file[j]);

                    if (matchStop.Success)
                    {
                        break;
                    }
                    else
                    {
                        map += $"{match.Value} ";
                    }
                }
                break;
            }
        }
        // Console.WriteLine(map);
        return map;
    }
    static long ConvertToDestination(long source, int index)
    {
        for (int i = 0; i < maps[index].Count;)
        {
            long destinationRange = maps[index][i];
            long sourceRange = maps[index][i + 1];
            long rangeLenght = maps[index][i + 2];

            // Console.WriteLine($"{destinationRange} {sourceRange} {rangeLenght}");

            if (source.InRange(sourceRange, sourceRange + rangeLenght - 1))
            {
                // Console.WriteLine("true");
                return source - sourceRange + destinationRange;
            }
            i += 3;
        }

        // Console.WriteLine("false");
        return source;
    }
    private static void Main(string[] args)
    {
        long[] source = new long[seed.Count];
        for (int j = 0; j < seed.Count; j++)
        {
            source[j] = seed[j];
            Console.Write($"{source[j]} => ");
            for (int i = 0; i < maps.Count; i++)
            {
                source[j] = ConvertToDestination(source[j], i);
            }

            Console.WriteLine($"{source[j]}\n");
        }

        Console.WriteLine($"smalest => {source.Min()}");
    }
}

/*  
    Seed [79], soil [81], fertilizer [81], water [81], light [74], temperature [78], humidity [78], location [82].
    Seed [14], soil [14], fertilizer [53], water [49], light [42], temperature [42], humidity [43], location [43].
    Seed [55], soil [57], fertilizer [57], water [53], light [46], temperature [82], humidity [82], location [86].
    Seed [13], soil [13], fertilizer [52], water [41], light [34], temperature [34], humidity [35], location [35].

    So, the lowest location number in this example is 35.

    seeds: [79] 14 55 13

    seed-to-soil map:
    50 98 2
    52 50 48        81

    soil-to-fertilizer map:
    0 15 37
    37 52 2         81
    39 0 15     

    fertilizer-to-water map:
    49 53 8
    0 11 42         81
    42 0 7
    57 7 4

    water-to-light map:
    88 18 7         
    18 25 70        74

    light-to-temperature map:
    45 77 23
    81 45 19
    68 64 13    78

    temperature-to-humidity map:
    0 69 1
    1 0 69      78

    humidity-to-location map:
    60 56 37
    56 93 4     82
*/