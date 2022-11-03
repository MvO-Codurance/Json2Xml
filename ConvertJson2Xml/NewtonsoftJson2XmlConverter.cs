using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ConvertJson2Xml;

public class NewtonsoftJson2XmlConverter
{
    public async Task ConvertViaSerialisation(Options options)
    {
        await using var inputStream = File.OpenRead(options.Input);
        using var inputReader = new StreamReader(inputStream);
        using var reader = new JsonTextReader(inputReader);
        
        await using var outputStream = File.Open(options.Output, FileMode.Create, FileAccess.Write);
        await using var xmlWriter = XmlWriter.Create(outputStream, new XmlWriterSettings { Async = true, Indent = true });
        
        var serialiser = new JsonSerializer();
        var xmlNodeConverter = new XmlNodeConverter
        {
            DeserializeRootElementName = options.RootElementName
        };

        await xmlWriter.WriteStartDocumentAsync();
        await xmlWriter.WriteStartElementAsync(null, options.ElementName, null);
        
        while (await reader.ReadAsync())
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                if (xmlNodeConverter.ReadJson(reader, typeof(XElement), null, serialiser) is XElement xmlElement)
                {
                    await xmlElement.WriteToAsync(xmlWriter, CancellationToken.None);        
                }
            }
        }
        
        await xmlWriter.WriteEndElementAsync(); // People
        await xmlWriter.WriteEndDocumentAsync();
        await xmlWriter.FlushAsync();
    }
}