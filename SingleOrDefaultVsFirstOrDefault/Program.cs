using SingleOrDefaultVsFirstOrDefault;
using System.Diagnostics;
using System.Text.Json;

List<User> users =
[   new User
    {
        Id = 1,
        Name = "Ralph",
        Surname = "Nyoni"
    },
    new User
    {
        Id= 2,
        Name = "Ralph",
        Surname = "Nyoni"
    },
    new User
    {
        Id= 3,
        Name = "Rutendo",
        Surname = "Nyoni"
    },
    new User
    {
        Id= 4,
        Name = "Junior",
        Surname = "Nyoni"
    },
    new User
    {
        Id= 5,
        Name = "Olivia",
        Surname = "Nyoni"
    },
    new User
    {
        Id= 6,
        Name = "Rickie",
        Surname = "Nyoni"
    }
];

// Run FirstOrDefault test
var firstOrDefaultTime = MeasureExecutionTime(() => users.FirstOrDefault(a => a.Id == 1));
Console.WriteLine($"FirstOrDefault Execution Time: {firstOrDefaultTime} ticks");

// Run SingleOrDefault test
var singleOrDefaultTime = MeasureExecutionTime(() => users.SingleOrDefault(a => a.Name == "Rickie"));
Console.WriteLine($"SingleOrDefault Execution Time: {singleOrDefaultTime} ticks");

// FirstOrDefault

var firstOrDefaultUser = users.FirstOrDefault(a => a.Id == 1);

Console.WriteLine("FirstOrDefault Result: " + (firstOrDefaultUser != null
    ? JsonSerializer.Serialize(firstOrDefaultUser, new JsonSerializerOptions { WriteIndented = true })
    : "No match found"));


var singleOrDefaultUser = users.SingleOrDefault(a => a.Name == "Rickie");


Console.WriteLine("SingleOrDefault Result: " + (singleOrDefaultUser != null
    ? JsonSerializer.Serialize(singleOrDefaultUser, new JsonSerializerOptions { WriteIndented = true })
    : "No match found"));

static long MeasureExecutionTime<T>(Func<T> action, int iterations = 1000)
{
    var stopwatch = Stopwatch.StartNew();

    for (int i = 0; i < iterations; i++)
    {
        action();
    }

    stopwatch.Stop();
    return stopwatch.ElapsedTicks / iterations; // Average time per execution
}