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
    
    [Option('r', "rootElementName", Required = true, HelpText = "Name of the root XML element.")]
#pragma warning disable CS8618
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string RootElementName { get; set; }
#pragma warning restore CS8618
    
    [Option('e', "elementName", Required = true, HelpText = "Name of the item XML element.")]
#pragma warning disable CS8618
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string ElementName { get; set; }
#pragma warning restore CS8618
}