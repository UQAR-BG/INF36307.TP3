using System.Runtime.Serialization;

namespace INF36307.TP3.Models;

public class Etudiant
{
    [IgnoreDataMember]
    public int Id { get; set; }
    
    [DataMember(Name = "nom")]
    public string Nom { get; set; } = string.Empty;
    
    [DataMember(Name = "email")]
    public string Email { get; set; } = string.Empty;
}