``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
11th Gen Intel Core i9-11950H 2.60GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.100
  [Host]     : .NET Core 3.1.21 (CoreCLR 4.700.21.51404, CoreFX 4.700.21.51508), X64 RyuJIT
  DefaultJob : .NET Core 3.1.21 (CoreCLR 4.700.21.51404, CoreFX 4.700.21.51508), X64 RyuJIT


```
| Method |      N |     Mean |    Error |   StdDev |
|------- |------- |---------:|---------:|---------:|
| D07_P1 | 100000 | 78.13 μs | 1.554 μs | 2.277 μs |
| D07_P2 | 100000 | 79.17 μs | 1.544 μs | 1.652 μs |
