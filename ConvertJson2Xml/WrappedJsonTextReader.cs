using System.IO.Compression;
using Newtonsoft.Json;

namespace ConvertJson2Xml;

public class WrappedJsonTextReader : IDisposable
{
    private FileStream? _fileStream = null;
    private ZipArchive? _archive = null;
    private StreamReader? _streamReader = null;

    public JsonTextReader Reader { get; private set; }
    
    public static WrappedJsonTextReader Create(string inputPath)
    {
        var wrapper = new WrappedJsonTextReader();
        wrapper._fileStream = File.Open(inputPath, FileMode.Open, FileAccess.Read);

        bool isZipFile = string.Equals(Path.GetExtension(inputPath), ".zip", StringComparison.OrdinalIgnoreCase);
        if (isZipFile)
        {
            wrapper._archive = new ZipArchive(wrapper._fileStream, ZipArchiveMode.Read);
            var jsonFileEntry = wrapper._archive.Entries.FirstOrDefault();
            ArgumentNullException.ThrowIfNull(jsonFileEntry);
            wrapper._streamReader = new StreamReader(jsonFileEntry.Open());
        }
        else
        {
            wrapper._streamReader = new StreamReader(wrapper._fileStream);   
        }
        wrapper.Reader = new JsonTextReader(wrapper._streamReader);
        
        return wrapper;
    }

#pragma warning disable CS8618
    private WrappedJsonTextReader()
#pragma warning restore CS8618
    {
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            ((IDisposable)Reader).Dispose();
            _streamReader?.Dispose();
            _archive?.Dispose();
            _fileStream?.Dispose();
        }
    }
}