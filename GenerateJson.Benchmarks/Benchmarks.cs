using System;
using System.IO;
using System.Threading.Tasks;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace GenerateJson.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Benchmarks
{
    [Params(10_000, 100_000, 1_000_000, 2_000_000)]
    public int NumberOfItems { get; set; }
    
    [GlobalSetup]
    public void GlobalSetup()
    {
        Directory.CreateDirectory(GetBasePath<NewtonsoftJsonGenerator>());
        Directory.CreateDirectory(GetBasePath<SystemTextJsonGenerator>());
    }
    
    [Benchmark(Baseline = true)]
    public async Task Newtonsoft()
    {
        var generator = new NewtonsoftJsonGenerator();
        var options = GetGeneratorOptions<NewtonsoftJsonGenerator>();
        await generator.Generate(options);
    }

    [Benchmark]
    public async Task SystemTextJson()
    {
        var generator = new SystemTextJsonGenerator();
        var options = GetGeneratorOptions<SystemTextJsonGenerator>();
        await generator.Generate(options);
    }

    private Options GetGeneratorOptions<T>()
    {
        return new Options
        {
            Output = GetOutputPath<T>(),
            Number = NumberOfItems
        };
    }

    private string GetOutputPath<T>()
    {
        return @$"{GetBasePath<T>()}\{NumberOfItems}.json";
    }
    
    private string GetBasePath<T>()
    {
        return @$"OutputFiles\{typeof(T).Name}";
    }
}