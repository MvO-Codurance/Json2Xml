using System.IO;
using System.Threading.Tasks;
using Bogus;
using Newtonsoft.Json;

namespace GenerateJson.Benchmarks;

public class NewtonsoftJsonGenerator
{
    public void Generate_Serialise(string outputPath, int numberOfItems)
    {
        var f = new Faker();
        var serialiser = JsonSerializer.Create();
        
        using var streamWriter = new StreamWriter(outputPath);
        using var jsonWriter = new JsonTextWriter(streamWriter);
        jsonWriter.CloseOutput = true;
        
        jsonWriter.WriteStartArray();
        
        for (var index = 0; index < numberOfItems; index++)
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
    
    public async Task Generate_JsonTextWriterAsync(string outputPath, int numberOfItems)
    {
        var f = new Faker();
        
        await using var streamWriter = new StreamWriter(outputPath);
        using var jsonWriter = new JsonTextWriter(streamWriter);
        
        await jsonWriter.WriteStartArrayAsync();
        
        for (var index = 0; index < numberOfItems; index++)
        {
            await jsonWriter.WriteStartObjectAsync();
            
            await jsonWriter.WritePropertyNameAsync("FirstName");
            await jsonWriter.WriteValueAsync(f.Name.FirstName());
            
            await jsonWriter.WritePropertyNameAsync("LastName");
            await jsonWriter.WriteValueAsync(f.Name.LastName());
            
            await jsonWriter.WritePropertyNameAsync("DateOfBirth");
            await jsonWriter.WriteValueAsync(f.Date.Past().Date);
            
            await jsonWriter.WritePropertyNameAsync("Nationality");
            await jsonWriter.WriteValueAsync(f.Address.Country());
            
            await jsonWriter.WriteEndObjectAsync();
        }
        
        await jsonWriter.WriteEndArrayAsync();
        await jsonWriter.FlushAsync();
    }
    
    public void Generate_JsonTextWriterSync(string outputPath, int numberOfItems)
    {
        var f = new Faker();
        
        using var streamWriter = new StreamWriter(outputPath);
        using var jsonWriter = new JsonTextWriter(streamWriter);
        
        jsonWriter.WriteStartArray();
        
        for (var index = 0; index < numberOfItems; index++)
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