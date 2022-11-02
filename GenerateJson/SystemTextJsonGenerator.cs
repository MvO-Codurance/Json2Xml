using System.Text.Json;
using Bogus;

namespace GenerateJson;

public class SystemTextJsonGenerator : IGenerator
{
    public async Task Generate(Options options)
    {
        var f = new Faker();
        
        await using var stream = File.OpenWrite(options.Output);
        await using var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = options.Indent });
    
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
        await writer.FlushAsync();
    }
}