using CommandLine;
using GenerateJson;

Parser.Default.ParseArguments<Options>(args)
    .WithParsedAsync(async options =>
    {
        var generator = new JsonGenerator();
        await generator.Generate(options);
        Console.WriteLine($"Generated {options.Number} items to {options.Output}");
    });