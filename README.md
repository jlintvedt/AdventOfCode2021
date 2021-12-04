# AdventOfCode2020

### Running benchmarks
Update reference [BenchmarkRunner.Run<Day**17**Benchmark>(config)](AdventOfCodeBenchmark/Program.cs).

Run without debugger: `ctrl+f5` in VS Code. This stores the benchmark in [results](AdventOfCodeBenchmark\BenchmarkDotNet.Artifacts\results) folder and the [Results](Results.json) file, also updates the table below.

## Runtimes
<!--ResultTableStart-->
|                                |         | Test @2.6GHz<sup>1</sup> | Benchmark<sup>2</sup> |
|--------------------------------|---------|-------------------------:|----------------------:|
| [Day01](AdventOfCode/Day01.cs) | Puzzle1 |                     <1ms |                  59μs |
|                                | Puzzle2 |                     <1ms |                  57μs |
| [Day02](AdventOfCode/Day02.cs) | Puzzle1 |                     <1ms |                 160μs |
|                                | Puzzle2 |                     <1ms |                 156μs |
| [Day03](AdventOfCode/Day03.cs) | Puzzle1 |                     <1ms |                 125μs |
|                                | Puzzle2 |                     <1ms |                 285μs |
| [Day04](AdventOfCode/Day04.cs) | Puzzle1 |                     <1ms |                 218μs |
|                                | Puzzle2 |                     <1ms |                 318μs |
<!--ResultTableEnd-->

1) Laptop Intel i9-11950H @ 2.6GHz. Visual Studio Test Explorer
2) Laptop Intel i9-11950H @ 2.6GHz. Using [DotNetBenchmark](https://github.com/dotnet/BenchmarkDotNet).