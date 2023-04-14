using System.Runtime.Serialization;
//using Newtonsoft.Json;

namespace INF36307.TP3.Models;

public class User
{
    [DataMember(Name = "nom")]
    public string Nom { get; set; } = string.Empty;
}