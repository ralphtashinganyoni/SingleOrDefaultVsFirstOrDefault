using System.Diagnostics;

namespace SingleOrDefaultVsFirstOrDefault.Demos
{
    // ❌ My first attempt at comparing the two. Kept here on purpose because the video
    // walks through what's wrong with it:
    //   1. FirstOrDefault looks for Id 1 (the first item), SingleOrDefault looks for
    //      "Rickie" (the last item), so they aren't doing the same work.
    //   2. A Stopwatch with no warm-up isn't a reliable benchmark.
    // The fair version is Benchmarks/FirstVsSingleBenchmark.cs (dotnet run -c Release -- bench).
    public static class OriginalStopwatchBenchmark
    {
        public static void Run(List<User> users)
        {
            Console.WriteLine("=== 2. My original (unfair) Stopwatch benchmark ===");

            var firstOrDefaultTime = MeasureExecutionTime(() => users.FirstOrDefault(a => a.Id == 1));
            Console.WriteLine($"FirstOrDefault Execution Time: {firstOrDefaultTime} ticks");

            var singleOrDefaultTime = MeasureExecutionTime(() => users.SingleOrDefault(a => a.Name == "Rickie"));
            Console.WriteLine($"SingleOrDefault Execution Time: {singleOrDefaultTime} ticks");

            Console.WriteLine("Don't trust these numbers. Run the BenchmarkDotNet version instead.");
            Console.WriteLine();
        }

        private static long MeasureExecutionTime<T>(Func<T> action, int iterations = 1000)
        {
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < iterations; i++)
            {
                action();
            }

            stopwatch.Stop();
            return stopwatch.ElapsedTicks / iterations; // Average time per execution
        }
    }
}
