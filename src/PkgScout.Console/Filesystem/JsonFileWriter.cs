using System.Text.Json;
using System.Text.Json.Serialization;

namespace PkgScout.Console.Filesystem;

public static class JsonFileWriter
{
    private static readonly JsonSerializerOptions Options = new()
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public static void WriteToFile<T>(string filePath, T data)
    {
        try
        {
            var content = JsonSerializer.Serialize(data, Options);
            File.WriteAllText(filePath, content);
        }
        catch (Exception exception)
        {
            global::System.Console.WriteLine(exception);
            throw;
        }
    }
}