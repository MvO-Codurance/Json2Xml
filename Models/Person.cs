using Newtonsoft.Json;

namespace Models;

public record struct Person
{
    [JsonProperty("@xmlns")]
    public string XmlNs => "https://tempuri.org/person";
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string Nationality { get; init; }
}