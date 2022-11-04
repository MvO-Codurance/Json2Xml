using CommandLine;

namespace ConvertJson2Xml;

// CS8618: "Non-nullable property 'Input' is uninitialized. Consider declaring the property as nullable."
#pragma warning disable CS8618

// ReSharper disable once ClassNeverInstantiated.Global
public class Options
{
    // ReSharper disable UnusedAutoPropertyAccessor.Global
    
    [Option('i', "input", Required = true, HelpText = "Full path to the input JSON file.")]
    public string Input { get; set; }
    
    [Option('o', "output", Required = true, HelpText = "Full path to the output XML file.")]
    public string Output { get; set; }
    
    [Option('r', "rootElementName", Required = true, HelpText = "Name of the root XML element.")]
    public string RootElementName { get; set; }
    
    [Option('e', "elementName", Required = true, HelpText = "Name of the item XML element.")]
    public string ElementName { get; set; }
    
    [Option('i', "indent", Required = false, HelpText = "Indent the generated XML.", Default = false)]
    public bool Indent { get; set; }
    
    [Option('z', "zip", Required = false, HelpText = "Zip the generated XML file.", Default = false)]
    public bool Zip { get; set; }
}

#pragma warning restore CS8618