using JsonSerializer = Utf8Json.JsonSerializer;

namespace INF36307.TP3.Utils;

public class JsonUtils<T> where T : class
{
    public T? Deserialize(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<T>(json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public static string Serialize(T t)
    {
        return JsonSerializer.ToJsonString(t);
    }
}