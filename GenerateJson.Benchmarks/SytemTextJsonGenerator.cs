using System.IO;
using System.Text.Json;
using Bogus;

namespace GenerateJson.Benchmarks;

public class SystemTextJsonGenerator
{
    public void Generate_Serialise(string outputPath, int numberOfItems)
    {
        var f = new Faker();
        
        using var stream = File.OpenWrite(outputPath);
        using var writer = new Utf8JsonWriter(stream);
    
        writer.WriteStartArray();
        
        for (var index = 0; index < numberOfItems; index++)
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
    
    public void Generate_Utf8JsonWriterSync(string outputPath, int numberOfItems)
    {
        var f = new Faker();
        
        using var stream = File.OpenWrite(outputPath);
        using var writer = new Utf8JsonWriter(stream);
        
        writer.WriteStartArray();
        
        for (var index = 0; index < numberOfItems; index++)
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