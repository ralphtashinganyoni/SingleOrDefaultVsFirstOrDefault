using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SingleOrDefaultVsFirstOrDefault.Demos
{
    public static class EfCoreSqlDemo
    {
        public static void Run()
        {
            Console.WriteLine("=== 3. What EF Core sends to SQL Server ===");

            var capture = new SqlCaptureInterceptor();
            using var db = new AppDbContext(capture);
            const string name = "Ralph";

            // These are the real calls; the interceptors grab the SQL instead of hitting a database
            db.Users.FirstOrDefault(u => u.Name == name);
            Console.WriteLine("FirstOrDefault ->");
            Console.WriteLine(capture.LastSql);
            Console.WriteLine();

            db.Users.SingleOrDefault(u => u.Name == name);
            Console.WriteLine("SingleOrDefault ->");
            Console.WriteLine(capture.LastSql);
            Console.WriteLine();

            Console.WriteLine("Single only asks for one extra row (TOP(2)) so it can detect a duplicate.");
            Console.WriteLine();
        }

        private class AppDbContext(SqlCaptureInterceptor capture) : DbContext
        {
            public DbSet<User> Users => Set<User>();

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
                optionsBuilder
                    .UseSqlServer("Server=localhost;Database=FirstVsSingle;Trusted_Connection=True")
                    .AddInterceptors(new SkipConnectionOpenInterceptor(), capture);
        }

        // Pretends the connection opened, so the demo runs without SQL Server installed
        private class SkipConnectionOpenInterceptor : DbConnectionInterceptor
        {
            public override InterceptionResult ConnectionOpening(
                DbConnection connection, ConnectionEventData eventData, InterceptionResult result) =>
                InterceptionResult.Suppress();
        }

        // Records the generated SQL and returns an empty result instead of executing it
        private class SqlCaptureInterceptor : DbCommandInterceptor
        {
            public string? LastSql { get; private set; }

            public override InterceptionResult<DbDataReader> ReaderExecuting(
                DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
            {
                LastSql = command.CommandText;
                return InterceptionResult<DbDataReader>.SuppressWithResult(new DataTable().CreateDataReader());
            }
        }
    }
}
