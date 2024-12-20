// See https://aka.ms/new-console-template for more information
using System.Reflection.Emit;

Console.WriteLine("Hello, World!");

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
    var trend = 0;
    foreach (var level in levels[1..])
    {
        var levelValue = int.Parse(level);
        var diff = levelValue - lastLevelValue;
        
        // Not changes within 1 to 3 
        if (Math.Abs(diff) < 1 | Math.Abs(diff) > 3) isSafe = false;

        var currentTrend = levelValue.CompareTo(lastLevelValue);
        if (trend != 0)
        {
            if (trend != currentTrend) isSafe = false;
        }
        trend = currentTrend;
        lastLevelValue = levelValue;

        if (!isSafe) break;
    }

    if (isSafe) safe++;
}

 Console.WriteLine($"Safe: {safe}");