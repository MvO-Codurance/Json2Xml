using Bogus;
using Newtonsoft.Json;

namespace GenerateJson;

public class NewtonsoftJsonGenerator
{
    public void GenerateViaSerialisation(Options options)
    {
        var f = new Faker();
        var serialiser = JsonSerializer.Create();
        
        using var streamWriter = new StreamWriter(options.Output);
        using var jsonWriter = new JsonTextWriter(streamWriter);
        jsonWriter.CloseOutput = true;
        if (options.Indent)
        {
            jsonWriter.Formatting = Formatting.Indented;
        }

        jsonWriter.WriteStartArray();
        
        for (var index = 0; index < options.Number; index++)
        {
            var p = new Person(
                f.Name.FirstName(),
                f.Name.LastName(),
                f.Date.Past().Date,
                f.Address.Country());

            serialiser.Serialize(jsonWriter, p);
        }
        
        jsonWriter.WriteEndArray();
        jsonWriter.Flush();
    }
    
    public void GenerateViaWriter(Options options)
    {
        var f = new Faker();
        
        using var streamWriter = new StreamWriter(options.Output);
        using var jsonWriter = new JsonTextWriter(streamWriter);
        jsonWriter.CloseOutput = true;
        if (options.Indent)
        {
            jsonWriter.Formatting = Formatting.Indented;
        }

        jsonWriter.WriteStartArray();
        
        for (var index = 0; index < options.Number; index++)
        {
            jsonWriter.WriteStartObject();
            
            jsonWriter.WritePropertyName("FirstName");
            jsonWriter.WriteValue(f.Name.FirstName());
            
            jsonWriter.WritePropertyName("LastName");
            jsonWriter.WriteValue(f.Name.LastName());
            
            jsonWriter.WritePropertyName("DateOfBirth");
            jsonWriter.WriteValue(f.Date.Past().Date);
            
            jsonWriter.WritePropertyName("Nationality");
            jsonWriter.WriteValue(f.Address.Country());
            
            jsonWriter.WriteEndObject();
        }
        
        jsonWriter.WriteEndArray();
        jsonWriter.Flush();
    }
}