using BenchmarkDotNet.Attributes;

namespace SingleOrDefaultVsFirstOrDefault.Benchmarks
{
    // The fair comparison: same list, same predicate, match right at the start.
    // FirstOrDefault stops at the match; SingleOrDefault has to scan the rest of the
    // list to prove there isn't a second one, so the gap grows with Count.
    [MemoryDiagnoser]
    public class FirstVsSingleBenchmark
    {
        private List<User> _users = default!;

        [Params(10, 10_000)]
        public int Count;

        [GlobalSetup]
        public void Setup() =>
            _users = Enumerable.Range(1, Count)
                .Select(i => new User { Id = i, Name = $"User{i}", Surname = "Test" })
                .ToList();

        [Benchmark(Baseline = true)]
        public User? First() => _users.FirstOrDefault(u => u.Id == 1);

        [Benchmark]
        public User? Single() => _users.SingleOrDefault(u => u.Id == 1);
    }
}
