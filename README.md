# AdventOfCode2021

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
| [Day05](AdventOfCode/Day05.cs) | Puzzle1 |                      3ms |                   1ms |
|                                | Puzzle2 |                      5ms |                   2ms |
| [Day06](AdventOfCode/Day06.cs) | Puzzle1 |                     <1ms |                   6μs |
|                                | Puzzle2 |                     <1ms |                   7μs |
| [Day07](AdventOfCode/Day07.cs) | Puzzle1 |                     <1ms |                  78μs |
|                                | Puzzle2 |                     <1ms |                  79μs |
| [Day08](AdventOfCode/Day08.cs) | Puzzle1 |                     <1ms |                       |
| [Day09](AdventOfCode/Day09.cs) | Puzzle1 |                     <1ms |                 127μs |
|                                | Puzzle2 |                      1ms |                 768μs |
| [Day10](AdventOfCode/Day10.cs) | Puzzle1 |                     <1ms |                 120μs |
|                                | Puzzle2 |                     <1ms |                 124μs |
| [Day11](AdventOfCode/Day11.cs) | Puzzle1 |                     <1ms |                 247μs |
|                                | Puzzle2 |                      1ms |                 992μs |
| [Day12](AdventOfCode/Day12.cs) | Puzzle1 |                      2ms |                   1ms |
|                                | Puzzle2 |                     76ms |                  36ms |
| [Day13](AdventOfCode/Day13.cs) | Puzzle1 |                     <1ms |                 137μs |
|                                | Puzzle2 |                     <1ms |                 258μs |
| [Day14](AdventOfCode/Day14.cs) | Puzzle1 |                     <1ms |                  98μs |
|                                | Puzzle2 |                     <1ms |                 454μs |
| [Day15](AdventOfCode/Day15.cs) | Puzzle1 |                      2ms |                   1ms |
|                                | Puzzle2 |                     57ms |                  43ms |
| [Day16](AdventOfCode/Day16.cs) | Puzzle1 |                     <1ms |                 128μs |
|                                | Puzzle2 |                     <1ms |                 141μs |
| [Day17](AdventOfCode/Day17.cs) | Puzzle1 |                     <1ms |                  30μs |
|                                | Puzzle2 |                     <1ms |                 206μs |
| [Day18](AdventOfCode/Day18.cs) | Puzzle1 |                      7ms |                   2ms |
|                                | Puzzle2 |                    128ms |                  57ms |
<!--ResultTableEnd-->

1) Laptop Intel i9-11950H @ 2.6GHz. Visual Studio Test Explorer
2) Laptop Intel i9-11950H @ 2.6GHz. Using [DotNetBenchmark](https://github.com/dotnet/BenchmarkDotNet).