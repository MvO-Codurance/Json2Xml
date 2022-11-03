using System.Text.Json;
using Bogus;

namespace GenerateJson;

public class SystemTextJsonGenerator
{
    public void GenerateViaSerialisation(Options options)
    {
        var f = new Faker();
        
        using var stream = File.Open(options.Output, FileMode.Create, FileAccess.Write);
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = options.Indent });
        
        writer.WriteStartArray();
        
        for (var index = 0; index < options.Number; index++)
        {
            var p = new Person(
                f.Name.FirstName(),
                f.Name.LastName(),
                f.Date.Past().Date,
                f.Address.Country());

            JsonSerializer.Serialize(writer, p);
        }
        
        writer.WriteEndArray();
        writer.Flush();
    }
    
    public void GenerateViaWriter(Options options)
    {
        var f = new Faker();
                
        using var stream = File.Open(options.Output, FileMode.Create, FileAccess.Write);
        using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = options.Indent });
        
        writer.WriteStartArray();
        
        for (var index = 0; index < options.Number; index++)
        {
            writer.WriteStartObject();
            
            writer.WritePropertyName("FirstName");
            writer.WriteStringValue(f.Name.FirstName());
            
            writer.WritePropertyName("LastName");
            writer.WriteStringValue(f.Name.LastName());
            
            writer.WritePropertyName("DateOfBirth");
            writer.WriteStringValue(f.Date.Past().Date);
            
            writer.WritePropertyName("Nationality");
            writer.WriteStringValue(f.Address.Country());
            
            writer.WriteEndObject();
        }
        
        writer.WriteEndArray();
        writer.Flush();
    }
}