using Bogus;
using Newtonsoft.Json;
using Person = Models.Person;

namespace GenerateJson;

public class NewtonsoftJsonGenerator
{
    public void GenerateViaSerialisation(Options options)
    {
        var f = new Faker();
        var serialiser = JsonSerializer.Create();

        using var wrappedJsonWriter = WrappedJsonTextWriter.Create(options.Output, options.Zip, options.Indent);
        var jsonWriter = wrappedJsonWriter.Writer;
        
        jsonWriter.WriteStartArray();
    
        for (var index = 0; index < options.Number; index++)
        {
            var p = new Person
            {
                FirstName = f.Name.FirstName(),
                LastName = f.Name.LastName(),
                DateOfBirth = f.Date.Past().Date,
                Nationality = f.Address.Country()
            };

            serialiser.Serialize(jsonWriter, p);
        }
    
        jsonWriter.WriteEndArray();
    }

    public void GenerateViaWriter(Options options)
    {
        var f = new Faker();
        var p = new Person();
        
        using var wrappedJsonWriter = WrappedJsonTextWriter.Create(options.Output, options.Zip, options.Indent);
        var jsonWriter = wrappedJsonWriter.Writer;
            
        jsonWriter.WriteStartArray();
    
        for (var index = 0; index < options.Number; index++)
        {
            jsonWriter.WriteStartObject();
            
            jsonWriter.WritePropertyName("@xmlns");
            jsonWriter.WriteValue(p.XmlNs);                    
        
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
    }
}