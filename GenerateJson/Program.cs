using System.Diagnostics;
using CommandLine;
using GenerateJson;

await Parser.Default.ParseArguments<Options>(args)
    .WithParsedAsync(async options =>
    {
        var sw = new Stopwatch();
        sw.Start();
        
        IGenerator generator = new NewtonsoftJsonGenerator();
        //IGenerator generator = new SystemTextJsonGenerator();
       
        await generator.Generate(options);

        sw.Stop();
        
        Console.WriteLine($"Generated {options.Number} items to {options.Output} in {sw.Elapsed.TotalSeconds} seconds.");
    });