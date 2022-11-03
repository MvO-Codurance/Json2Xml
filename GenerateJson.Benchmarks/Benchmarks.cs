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
    [Params(100_000, 500_000, 1_000_000)]
    public int NumberOfItems { get; set; }
    
    [GlobalSetup]
    public void GlobalSetup()
    {
        Directory.CreateDirectory(GetBasePath<NewtonsoftJsonGenerator>());
        Directory.CreateDirectory(GetBasePath<SystemTextJsonGenerator>());
    }
    
    [Benchmark(Baseline = true)]
    public void Newtonsoft_Serialise()
    {
        var generator = new NewtonsoftJsonGenerator();
        generator.Generate_Serialise(GetOutputPath<NewtonsoftJsonGenerator>(), NumberOfItems);
    }
    
    [Benchmark]
    public async Task Newtonsoft_Writer_Async()
    {
        var generator = new NewtonsoftJsonGenerator();
        await generator.Generate_JsonTextWriterAsync(GetOutputPath<NewtonsoftJsonGenerator>(), NumberOfItems);
    }
    
    [Benchmark]
    public void Newtonsoft_Writer_Sync()
    {
        var generator = new NewtonsoftJsonGenerator();
        generator.Generate_JsonTextWriterSync(GetOutputPath<NewtonsoftJsonGenerator>(), NumberOfItems);
    }

    [Benchmark]
    public void SystemTextJson_Serialise()
    {
        var generator = new SystemTextJsonGenerator();
        generator.Generate_Serialise(GetOutputPath<SystemTextJsonGenerator>(), NumberOfItems);
    }
    
    [Benchmark]
    public void SystemTextJson_Writer_Sync()
    {
        var generator = new SystemTextJsonGenerator();
        generator.Generate_Utf8JsonWriterSync(GetOutputPath<SystemTextJsonGenerator>(), NumberOfItems);
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