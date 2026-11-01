using BenchmarkDotNet.Running;
using SingleOrDefaultVsFirstOrDefault;
using SingleOrDefaultVsFirstOrDefault.Benchmarks;
using SingleOrDefaultVsFirstOrDefault.Demos;

// dotnet run                  -> run the demos from the video
// dotnet run -c Release -- bench -> run the BenchmarkDotNet comparison
if (args.Contains("bench"))
{
    BenchmarkRunner.Run<FirstVsSingleBenchmark>();
    return;
}

var users = SampleData.Users;

DuplicateDemo.Run(users);
OriginalStopwatchBenchmark.Run(users);
EfCoreSqlDemo.Run();
DefaultValueDemo.Run();
