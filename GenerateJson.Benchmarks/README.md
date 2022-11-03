# Generate JSON Benchmarks
Compare the various ways to efficiently generate JSON to a file.
 - Using Newtonsoft.Json
 - Using System.Text.Json (STJ)
 - Using serialisation
 - Using a raw JSON writer
 - Sync vs async

## Example Execution
```
./run.ps1
```

## Results

``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.1165/21H2)
12th Gen Intel Core i7-12700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK=6.0.402
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2


```
|                     Method | NumberOfItems |        Mean |     Error |    StdDev | Ratio | RatioSD | Rank |       Gen0 |      Gen1 |      Gen2 | Allocated | Alloc Ratio |
|--------------------------- |-------------- |------------:|----------:|----------:|------:|--------:|-----:|-----------:|----------:|----------:|----------:|------------:|
| SystemTextJson_Writer_Sync |        100000 |    79.65 ms |  1.354 ms |  1.200 ms |  0.66 |    0.02 |    1 |  2000.0000 | 1428.5714 | 1000.0000 |  46.11 MB |        1.31 |
|     Newtonsoft_Writer_Sync |        100000 |    90.42 ms |  1.795 ms |  2.741 ms |  0.76 |    0.03 |    2 |  1000.0000 |  500.0000 |         - |  12.36 MB |        0.35 |
|       Newtonsoft_Serialise |        100000 |   120.77 ms |  2.410 ms |  2.959 ms |  1.00 |    0.00 |    3 |  2750.0000 |  250.0000 |         - |  35.26 MB |        1.00 |
|    Newtonsoft_Writer_Async |        100000 |   163.08 ms |  3.218 ms |  5.197 ms |  1.36 |    0.05 |    4 |  1000.0000 |  333.3333 |         - |  13.67 MB |        0.39 |
|   SystemTextJson_Serialise |        100000 |   271.89 ms |  5.254 ms |  4.915 ms |  2.24 |    0.06 |    5 |  1000.0000 |  500.0000 |         - |  16.94 MB |        0.48 |
|                            |               |             |           |           |       |         |      |            |           |           |           |             |
| SystemTextJson_Writer_Sync |        500000 |   425.58 ms |  8.309 ms |  8.532 ms |  0.71 |    0.03 |    1 |  8000.0000 | 5000.0000 | 4000.0000 | 193.72 MB |        1.12 |
|     Newtonsoft_Writer_Sync |        500000 |   448.88 ms |  8.668 ms | 12.432 ms |  0.76 |    0.02 |    2 |  4000.0000 | 1000.0000 |         - |  58.78 MB |        0.34 |
|       Newtonsoft_Serialise |        500000 |   593.88 ms | 11.399 ms | 14.821 ms |  1.00 |    0.00 |    3 | 14000.0000 | 1000.0000 |         - | 173.21 MB |        1.00 |
|    Newtonsoft_Writer_Async |        500000 |   772.24 ms | 14.857 ms | 17.110 ms |  1.30 |    0.04 |    4 |  5000.0000 | 1000.0000 |         - |  65.35 MB |        0.38 |
|   SystemTextJson_Serialise |        500000 | 1,355.43 ms | 26.699 ms | 28.568 ms |  2.28 |    0.08 |    5 |  6000.0000 | 1000.0000 |         - |  81.67 MB |        0.47 |
|                            |               |             |           |           |       |         |      |            |           |           |           |             |
| SystemTextJson_Writer_Sync |       1000000 |   845.71 ms | 12.673 ms | 10.583 ms |  0.74 |    0.01 |    1 | 14000.0000 | 6000.0000 | 5000.0000 | 387.35 MB |        1.12 |
|     Newtonsoft_Writer_Sync |       1000000 |   861.49 ms | 12.615 ms | 11.183 ms |  0.76 |    0.01 |    2 |  9000.0000 | 1000.0000 |         - | 116.78 MB |        0.34 |
|       Newtonsoft_Serialise |       1000000 | 1,140.01 ms | 15.699 ms | 14.685 ms |  1.00 |    0.00 |    3 | 28000.0000 | 1000.0000 |         - | 345.67 MB |        1.00 |
|    Newtonsoft_Writer_Async |       1000000 | 1,521.79 ms | 13.079 ms | 11.594 ms |  1.33 |    0.02 |    4 | 10000.0000 | 1000.0000 |         - | 129.95 MB |        0.38 |
|   SystemTextJson_Serialise |       1000000 | 2,666.71 ms | 19.041 ms | 16.880 ms |  2.34 |    0.03 |    5 | 13000.0000 | 1000.0000 |         - | 162.58 MB |        0.47 |


NOTE: STJ doesn't offer async methods on it's writer.

## Summary:
- Using a writer directly beats serialisation in terms of time and allocations
- Sync beats async
  - Whilst both Newtonsoft and STJ offer some async methods (Newtonsoft has more and STJ doesn't offer any on the writer) these are just wrappers around the sync method, hence they are just an overhead.
 - Newtonsoft beats STJ
   - STJ/Writer/Sync is marginally faster than the Newtonsoft equivalent but allocated 3x to 4x more.
 - So, Newtonsoft/Writer/Sync is the best combination for both time and allocations, but it does require writing more code. 
The Newtonsoft/Serialisation/Sync method is much more convenient, especially if the model is changing frequently.
Although it is slower than the Writer method and allocates more.
Therefore the actual implementation offers both Newtonsoft/Writer/Sync and Newtonsoft/Serialisation/Sync methods.  