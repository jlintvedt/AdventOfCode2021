``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.22000
11th Gen Intel Core i9-11950H 2.60GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


```
| Method |    N |      Mean |     Error |    StdDev |
|------- |----- |----------:|----------:|----------:|
| D18_P1 | 1000 |  2.255 ms | 0.0429 ms | 0.0511 ms |
| D18_P2 | 1000 | 57.843 ms | 1.0361 ms | 0.9692 ms |
