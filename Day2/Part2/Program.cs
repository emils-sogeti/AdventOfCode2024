// See https://aka.ms/new-console-template for more information
using System.Reflection.Emit;

Console.WriteLine("Day2, Part 2!");

// Setup
var input = File.OpenText(@".\input.csv");

var safe = 0;
string? report;

while ((report = input.ReadLine()) is not null)
{
    var levels = report.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

    var lastLevelValue = int.Parse(levels[0]);
    Console.WriteLine($"First level: {lastLevelValue}");
    var isSafe = true;
    var trend = -2;
    var dampener = true;
    foreach (var level in levels[1..])
    {
        var levelValue = int.Parse(level);
        var diff = levelValue - lastLevelValue;
        var currentTrend = levelValue.CompareTo(lastLevelValue);

        // Only changes within 1 to 3 
        if (Math.Abs(diff) < 1 | Math.Abs(diff) > 3) isSafe = false;

        if (trend != -2)
        {
            // Trend is shifting
            if (trend != currentTrend) isSafe = false;
        }

        if (!isSafe)
        {
            if (dampener)
            {
                // Disregard the faulty level
                isSafe = true;
                // Only allowed once
                dampener = false;
            }
            else
            {
                // Unsafe
                break;
            }
        }
        else
        {
            trend = currentTrend;
            lastLevelValue = levelValue;
        }
    }

    if (isSafe) safe++;
}

Console.WriteLine($"Safe: {safe}");