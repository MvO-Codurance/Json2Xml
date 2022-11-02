using BenchmarkDotNet.Running;
using GenerateJson.Benchmarks;

var summary = BenchmarkRunner.Run<Benchmarks>();
