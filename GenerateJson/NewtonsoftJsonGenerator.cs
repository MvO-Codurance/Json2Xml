using System.IO.Compression;
using Bogus;
using Newtonsoft.Json;

namespace GenerateJson;

public class NewtonsoftJsonGenerator
{
    public void GenerateViaSerialisation(Options options)
    {
        var f = new Faker();
        var serialiser = JsonSerializer.Create();
        
        var fileStream = File.Open(options.Output, FileMode.Create, FileAccess.Write);
        ZipArchive? archive = null;
        Stream? jsonFileEntryStream = null;
        StreamWriter? streamWriter = null;
        JsonTextWriter? jsonWriter = null;
    
        try
        {
            if (options.Zip)
            {
                archive = new ZipArchive(fileStream, ZipArchiveMode.Create);
                var jsonFileEntry = archive.CreateEntry(Path.GetFileNameWithoutExtension(options.Output) + ".json");
                jsonFileEntryStream = jsonFileEntry.Open();
                streamWriter = new StreamWriter(jsonFileEntryStream);
            }
            else
            {
                streamWriter = new StreamWriter(fileStream);   
            }
            jsonWriter = new JsonTextWriter(streamWriter);
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
        }
        finally
        {
            jsonWriter?.Flush();
            jsonWriter?.Close();
            streamWriter?.Close();
            jsonFileEntryStream?.Close();
            archive?.Dispose();
        }
    }

    public void GenerateViaWriter(Options options)
    {
        var f = new Faker();
        
        var fileStream = File.Open(options.Output, FileMode.Create, FileAccess.Write);
        ZipArchive? archive = null;
        Stream? jsonFileEntryStream = null;
        StreamWriter? streamWriter = null;
        JsonTextWriter? jsonWriter = null;

        try
        {
            if (options.Zip)
            {
                archive = new ZipArchive(fileStream, ZipArchiveMode.Create);
                var jsonFileEntry = archive.CreateEntry(Path.GetFileNameWithoutExtension(options.Output) + ".json");
                jsonFileEntryStream = jsonFileEntry.Open();
                streamWriter = new StreamWriter(jsonFileEntryStream);
            }
            else
            {
                streamWriter = new StreamWriter(fileStream);   
            }
            jsonWriter = new JsonTextWriter(streamWriter);
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
        }
        finally
        {
            jsonWriter?.Flush();
            jsonWriter?.Close();
            streamWriter?.Close();
            jsonFileEntryStream?.Close();
            archive?.Dispose();
        }
    }
}