using System.IO.Compression;
using Newtonsoft.Json;

namespace GenerateJson;

public class WrappedJsonTextWriter : IDisposable
{
    private ZipArchive? _archive = null;
    private Stream? _jsonFileEntryStream = null;
    private StreamWriter? _streamWriter = null;

    public JsonTextWriter Writer { get; private set; }
    
    public static WrappedJsonTextWriter Create(string outputPath, bool zipped, bool indent)
    {
        var wrapper = new WrappedJsonTextWriter();
        var fileStream = File.Open(outputPath, FileMode.Create, FileAccess.Write);
        
        if (zipped)
        {
            wrapper._archive = new ZipArchive(fileStream, ZipArchiveMode.Create);
            var jsonFileEntry = wrapper._archive.CreateEntry(Path.GetFileNameWithoutExtension(outputPath) + ".json");
            wrapper._jsonFileEntryStream = jsonFileEntry.Open();
            wrapper._streamWriter = new StreamWriter(wrapper._jsonFileEntryStream);
        }
        else
        {
            wrapper._streamWriter = new StreamWriter(fileStream);   
        }
        wrapper.Writer = new JsonTextWriter(wrapper._streamWriter);
        if (indent)
        {
            wrapper.Writer.Formatting = Formatting.Indented;
        }

        return wrapper;
    }

#pragma warning disable CS8618
    private WrappedJsonTextWriter()
#pragma warning restore CS8618
    {
    }
    
    public void Dispose()
    {
        Writer.Flush();
        Writer.Close();
        _streamWriter?.Dispose();
        _jsonFileEntryStream?.Dispose();
        _archive?.Dispose();
    }
}