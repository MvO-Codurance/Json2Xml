using CommandLine;

namespace GenerateJson;

// CS8618: "Non-nullable property 'Input' is uninitialized. Consider declaring the property as nullable."
#pragma warning disable CS8618

// ReSharper disable once ClassNeverInstantiated.Global
public class Options
{
    // ReSharper disable UnusedAutoPropertyAccessor.Global

    [Option('o', "output", Required = true, HelpText = "Full path to the output file.")]
    public string Output { get; set; }
    
    [Option('n', "number", Required = true, HelpText = "Number of items to generate.")]
    public int Number { get; set; }
    
    [Option('i', "indent", Required = false, HelpText = "Indent the generated JSON.", Default = false)]
    public bool Indent { get; set; }
    
    [Option('z', "zip", Required = false, HelpText = "Zip the generated JSON file.", Default = false)]
    public bool Zip { get; set; }
}

#pragma warning restore CS8618