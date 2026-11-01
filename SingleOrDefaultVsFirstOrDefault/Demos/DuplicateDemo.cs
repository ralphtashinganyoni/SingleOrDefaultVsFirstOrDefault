namespace SingleOrDefaultVsFirstOrDefault.Demos
{
    public static class DuplicateDemo
    {
        public static void Run(List<User> users)
        {
            Console.WriteLine("=== 1. Two users called Ralph ===");

            // FirstOrDefault quietly picks one of the duplicates
            var first = users.FirstOrDefault(u => u.Name == "Ralph");
            Console.WriteLine($"FirstOrDefault picked Id {first?.Id}");

            // SingleOrDefault refuses: there should be zero or one
            try
            {
                var single = users.SingleOrDefault(u => u.Name == "Ralph");
                Console.WriteLine($"SingleOrDefault returned Id {single?.Id}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"SingleOrDefault threw {nameof(InvalidOperationException)}: {ex.Message}");
            }

            Console.WriteLine();
        }
    }
}
