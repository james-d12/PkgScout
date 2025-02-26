using System.Text.Json;

namespace PkgScout.Shared.Filesystem;

public static class JsonFileWriter
{
    public static void WriteToFile<T>(string filePath, T data)
    {
        try
        {
            var content = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, content);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}