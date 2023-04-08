using Newtonsoft.Json;

namespace INF36307.TP3.Models;

public class User
{
    [JsonProperty("nom", Required = Required.Always)]
    public string Nom { get; set; } = string.Empty;
}