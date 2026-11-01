namespace SingleOrDefaultVsFirstOrDefault
{
    public static class SampleData
    {
        // Two users called Ralph Nyoni: the duplicate that FirstOrDefault hides
        public static List<User> Users =>
        [
            new User { Id = 1, Name = "Ralph", Surname = "Nyoni" },
            new User { Id = 2, Name = "Ralph", Surname = "Nyoni" },
            new User { Id = 3, Name = "Rutendo", Surname = "Nyoni" },
            new User { Id = 4, Name = "Junior", Surname = "Nyoni" },
            new User { Id = 5, Name = "Olivia", Surname = "Nyoni" },
            new User { Id = 6, Name = "Rickie", Surname = "Nyoni" }
        ];
    }
}
