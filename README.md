# FirstOrDefault vs SingleOrDefault in C#

Companion code for my video **FirstOrDefault vs SingleOrDefault in C#: The Bug Hiding in Your Code**.

▶️ Video: _link coming soon_ · 📝 Article: _link coming soon_ · 📺 Channel: [@RalphNyoniDev](https://www.youtube.com/@RalphNyoniDev)

## The short version

| | No match | One match | Two or more matches |
|---|---|---|---|
| `FirstOrDefault` | default | that item | the **first** one |
| `SingleOrDefault` | default | that item | **throws** |

- **Should be unique** (an ID, an email, a username)? Use `SingleOrDefault`. Let it fail fast.
- **Any one will do**, or you want the first of a sorted result? Use `FirstOrDefault`, with an `OrderBy` so "first" means something.
- **Value types?** `default` is `0`, not `null`. Pass your own default: `FirstOrDefault(predicate, -1)`.

## Run it

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download). No database needed.

```bash
cd SingleOrDefaultVsFirstOrDefault

# The demos from the video
dotnet run

# The BenchmarkDotNet comparison (always run benchmarks in Release)
dotnet run -c Release -- bench
```

## What's in here

| File | What it shows |
|---|---|
| `Demos/DuplicateDemo.cs` | Two users called Ralph: `FirstOrDefault` picks one, `SingleOrDefault` throws |
| `Demos/OriginalStopwatchBenchmark.cs` | My first benchmark, kept on purpose. It's unfair: the two methods search for different items, and there's no warm-up. |
| `Benchmarks/FirstVsSingleBenchmark.cs` | The fair version with BenchmarkDotNet: same list, same predicate |
| `Demos/EfCoreSqlDemo.cs` | The SQL EF Core generates: `TOP(1)` for First, `TOP(2)` for Single. Interceptors capture the SQL, so no SQL Server is needed. |
| `Demos/DefaultValueDemo.cs` | Why `FirstOrDefault` on an `int` can't tell "found 0" from "found nothing" |

## Sample output

```text
=== 1. Two users called Ralph ===
FirstOrDefault picked Id 1
SingleOrDefault threw InvalidOperationException: Sequence contains more than one matching element

=== 3. What EF Core sends to SQL Server ===
FirstOrDefault ->
SELECT TOP(1) [u].[Id], [u].[Name], [u].[Surname]
FROM [Users] AS [u]
WHERE [u].[Name] = N'Ralph'

SingleOrDefault ->
SELECT TOP(2) [u].[Id], [u].[Name], [u].[Surname]
FROM [Users] AS [u]
WHERE [u].[Name] = N'Ralph'
```
