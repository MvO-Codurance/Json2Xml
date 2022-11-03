using CommandLine;

namespace ConvertJson2Xml;

// ReSharper disable once ClassNeverInstantiated.Global
public class Options
{
    [Option('i', "input", Required = true, HelpText = "Full path to the input JSON file.")]
#pragma warning disable CS8618
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string Input { get; set; }
#pragma warning restore CS8618
    
    [Option('o', "output", Required = true, HelpText = "Full path to the output XML file.")]
#pragma warning disable CS8618
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string Output { get; set; }
#pragma warning restore CS8618
}