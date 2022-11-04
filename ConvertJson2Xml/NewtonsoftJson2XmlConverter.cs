using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConvertJson2Xml;

public class NewtonsoftJson2XmlConverter
{
    public async Task ConvertViaSerialisation(Options options)
    {
        using var wrappedJsonReader = WrappedJsonTextReader.Create(options.Input);
        await using var wrappedXmlWriter = WrappedXmlWriter.Create(options.Output, options.Zip, options.Indent);
        var xmlWriter = wrappedXmlWriter.Writer;
        
        var serialiser = new JsonSerializer();
        var xmlNodeConverter = new XmlNodeConverter
        {
            DeserializeRootElementName = options.ElementName
        };

        await xmlWriter.WriteStartDocumentAsync();
        await xmlWriter.WriteStartElementAsync(null, options.RootElementName, null);
        
        while (await wrappedJsonReader.Reader.ReadAsync())
        {
            if (wrappedJsonReader.Reader.TokenType == JsonToken.StartObject)
            {
                if (xmlNodeConverter.ReadJson(wrappedJsonReader.Reader, typeof(XElement), null, serialiser) is XElement xmlElement)
                {
                    await xmlElement.WriteToAsync(xmlWriter, CancellationToken.None);        
                }
            }
        }
        
        await xmlWriter.WriteEndElementAsync(); // options.RootElementName
        await xmlWriter.WriteEndDocumentAsync();
        await xmlWriter.FlushAsync();
    }
}