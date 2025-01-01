// See https://aka.ms/new-console-template for more information
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;

Console.WriteLine("Hello, World!");

// Setup
var lines = File.ReadAllLines(@".\input.csv").ToList();

var _safe = 0;

int _lastLevelValue;
int _trend;
bool _damper;
var originalLineCount = lines.Count;

for (int i = 0; i < lines.Count - 1; i++)
{
    var line = lines[i];

    var levels = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    _trend = 0;
    _damper = true;
    _lastLevelValue = int.Parse(levels[0]);
    bool isSafe = true;
    foreach (var level in levels[1..])
    {
        var levelValue = int.Parse(level);

        isSafe = CheckSafety(levelValue);
        if (!isSafe)
        {
            if (_damper)
            {
                _damper = false;
                isSafe = true;
            }
            else
            {
                break;
            }
        }
    }

    Console.WriteLine($"Level: {line} : {isSafe}");

    if (isSafe)
    {
        _safe++;
    }
    else if (i < originalLineCount)
    {
        var reversedLine = string.Join(" ", levels.Reverse());
        lines.Add(reversedLine);
    }
}

Console.WriteLine($"Safe: {_safe}");

bool CheckSafety(int levelValue)
{
    var diff = levelValue - _lastLevelValue;
    var isSafe = true;

    // Changes allowed between 1 and 3 
    if (Math.Abs(diff) < 1 | Math.Abs(diff) > 3) isSafe = false;

    var currentTrend = levelValue.CompareTo(_lastLevelValue);
    if (_trend != 0)
    {
        if (_trend != currentTrend) isSafe = false;
    }

    if (isSafe)
    {
        _trend = currentTrend;
        _lastLevelValue = levelValue;
    }

    return isSafe;
}