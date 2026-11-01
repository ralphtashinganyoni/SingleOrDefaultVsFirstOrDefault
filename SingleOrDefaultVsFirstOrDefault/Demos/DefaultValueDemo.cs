namespace SingleOrDefaultVsFirstOrDefault.Demos
{
    public static class DefaultValueDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== 4. The default-value trap ===");

            var ages = new List<int> { 0, 25, 30 };

            // default(int) is 0, so "found someone aged 0" and "found nothing" look the same
            var age = ages.FirstOrDefault(a => a > 100);
            Console.WriteLine($"FirstOrDefault(a => a > 100)     = {age}");

            // Since .NET 6 you can pass your own default to tell them apart
            var ageOrMarker = ages.FirstOrDefault(a => a > 100, -1);
            Console.WriteLine($"FirstOrDefault(a => a > 100, -1) = {ageOrMarker} (means not found)");

            Console.WriteLine();
        }
    }
}
