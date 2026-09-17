using System.Text.Json.Serialization;

namespace NetwiseTask.Models;

public record CatFact(
    [property: JsonPropertyName("fact")] string Fact,
    [property: JsonPropertyName("length")] int Length
);