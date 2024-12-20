Console.WriteLine("Hello, World!");

//Setup
var input = File.OpenText(@".\input.csv");

var rightList = new List<int>();
var leftList = new List<int>();
string? row;

while ((row =input.ReadLine()) is not null)
{
    var columns = row.Split(" ",StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    rightList.Add(int.Parse(columns[0]));
    leftList.Add(int.Parse(columns[1]));
}  

rightList.Sort();
leftList.Sort();

var distance = 0;
for (int i = 0; i < rightList.Count; i++)
{
    distance += Math.Abs(rightList[i] - leftList[i]);
}

 Console.WriteLine($"Distance {distance}");