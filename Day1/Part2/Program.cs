// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

//Setup
var input = File.OpenText(@".\input.csv");

var leftList = new Dictionary<int, int>();
var rightList = new Dictionary<int,int>();

string? row;

while ((row = input.ReadLine()) is not null)
{
    var columns = row.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    var leftNumber = int.Parse(columns[0]);
    var rightNumber = int.Parse(columns[1]);
    if (!leftList.TryAdd(leftNumber, 1)) leftList[leftNumber]++;
    if (!rightList.TryAdd(rightNumber, 1)) rightList[rightNumber]++;
}

var distance = 0;
 foreach (var leftNumber in leftList.Keys)
{
    var leftScore = leftList[leftNumber];
    var rightScore = 0;
    rightList.TryGetValue(leftNumber, out rightScore);

    distance += leftNumber * leftScore * rightScore;
}

Console.WriteLine($"Distance: {distance}");