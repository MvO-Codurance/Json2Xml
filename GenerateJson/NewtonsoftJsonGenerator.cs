using Bogus;
using Newtonsoft.Json;

namespace GenerateJson;

public class NewtonsoftJsonGenerator : IGenerator
{
    public async Task Generate(Options options)
    {
        var f = new Faker();
        var serialiser = JsonSerializer.Create();
        
        await using var streamWriter = new StreamWriter(options.Output);
        
        using var jsonWriter = new JsonTextWriter(streamWriter);
        jsonWriter.CloseOutput = true;
        if (options.Indent)
        {
            jsonWriter.Formatting = Formatting.Indented;
        }

        await jsonWriter.WriteStartArrayAsync();
        
        for (var index = 0; index < options.Number; index++)
        {
            var p = new Person(
                f.Name.FirstName(),
                f.Name.LastName(),
                f.Date.Past().Date,
                f.Address.Country());

            serialiser.Serialize(jsonWriter, p);
        }
        
        await jsonWriter.WriteEndArrayAsync();
        await jsonWriter.FlushAsync();
    }
}