using Newtonsoft.Json;

namespace INF36307.TP3.Models;

public class Notification
{
    [JsonProperty("nom", Required = Required.Always)]
    public string Nom { get; set; } = string.Empty;
    
    [JsonProperty("email", Required = Required.AllowNull)]
    public string Email { get; set; } = string.Empty;
}