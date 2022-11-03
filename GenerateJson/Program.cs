using System.Diagnostics;
using CommandLine;
using GenerateJson;

Parser.Default.ParseArguments<Options>(args)
    .WithParsedAsync(options =>
    {
        var sw = new Stopwatch();
        sw.Start();
        
        var generator = new NewtonsoftJsonGenerator();
        generator.GenerateViaSerialisation(options);
        //generator.GenerateViaWriter(options);

        sw.Stop();
        
        Console.WriteLine($"Generated {options.Number} items to {options.Output} in {sw.Elapsed.TotalSeconds} seconds.");

        return null;
    });