using CommandLine;

namespace GenerateJson;

// ReSharper disable once ClassNeverInstantiated.Global
public class Options
{
    [Option('o', "output", Required = true, HelpText = "Full path to the output file.")]
#pragma warning disable CS8618
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string Output { get; set; }
#pragma warning restore CS8618
    
    [Option('n', "number", Required = true, HelpText = "Number of items to generate.")]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int Number { get; set; }
    
    [Option('i', "indent", Required = false, HelpText = "Indent the generated JSON.", Default = false)]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public bool Indent { get; set; }
    
    [Option('z', "zip", Required = false, HelpText = "Zip the generated JSON file.", Default = false)]
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public bool Zip { get; set; }
}