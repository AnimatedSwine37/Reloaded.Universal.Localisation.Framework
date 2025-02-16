namespace Reloaded.Universal.Localisation.Framework.FileEmulator;

public class LocalisedFile
{
    public Stream Stream { get; set; }
    
    public DateTime LastWriteTime { get; set; }

    public LocalisedFile(Stream stream, DateTime lastWriteTime)
    {
        Stream = stream;
        LastWriteTime = lastWriteTime;
    }
}