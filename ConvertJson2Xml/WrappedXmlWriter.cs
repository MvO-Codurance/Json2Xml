using System.IO.Compression;
using System.Xml;

namespace ConvertJson2Xml;

public class WrappedXmlWriter : IDisposable
{
    private FileStream? _fileStream = null;
    private ZipArchive? _archive = null;
    private Stream? _xmlFileStream = null;

    public XmlWriter Writer { get; private set; }
    
    public static WrappedXmlWriter Create(string outputPath, bool zipped, bool indent)
    {
        var wrapper = new WrappedXmlWriter();
        wrapper._fileStream = File.Open(outputPath, FileMode.Create, FileAccess.Write);
        
        if (zipped)
        {
            wrapper._archive = new ZipArchive(wrapper._fileStream, ZipArchiveMode.Create);
            var xmlFileEntry = wrapper._archive.CreateEntry(Path.GetFileNameWithoutExtension(outputPath) + ".xml");
            wrapper._xmlFileStream = xmlFileEntry.Open();
        }
        else
        {
            wrapper._xmlFileStream = wrapper._fileStream;
        }
        wrapper.Writer = XmlWriter.Create(
            wrapper._xmlFileStream, 
            new XmlWriterSettings { Async = true, Indent = indent });

        return wrapper;
    }

#pragma warning disable CS8618
    private WrappedXmlWriter()
#pragma warning restore CS8618
    {
    }
    
    public void Dispose()
    {
        Writer.Flush();
        Writer.Close();
        _xmlFileStream?.Dispose();
        _archive?.Dispose();
        _fileStream?.Close();
    }
}