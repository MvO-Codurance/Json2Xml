using System.Diagnostics;
using CommandLine;
using ConvertJson2Xml;

await Parser.Default.ParseArguments<Options>(args)
    .WithParsedAsync(async options =>
    {
        var sw = new Stopwatch();
        sw.Start();
        
        var converter = new NewtonsoftJson2XmlConverter();
        await converter.ConvertViaSerialisation(options);

        sw.Stop();
        
        Console.WriteLine($"Converted {options.Input} to {options.Output} in {sw.Elapsed.TotalSeconds} seconds.");
    });