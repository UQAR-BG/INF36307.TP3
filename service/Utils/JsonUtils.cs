using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace INF36307.TP3.Utils;

public class JsonUtils<T> where T : class
{
    private readonly JSchema _schema;
    
    public JsonUtils()
    {
        var generator = new JSchemaGenerator();
        _schema = generator.Generate(typeof(T));
    }
    
    public T? Deserialize(string json)
    {
        JsonTextReader reader = new JsonTextReader(new StringReader(json));

        JSchemaValidatingReader validatingReader = new JSchemaValidatingReader(reader);
        validatingReader.Schema = _schema;

        JsonSerializer serializer = new JsonSerializer();

        try
        {
            return serializer.Deserialize<T>(validatingReader);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public static string Serialize(T t)
    {
        return JsonConvert.SerializeObject(t, Formatting.Indented);
    }
}