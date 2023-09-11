using System.Text.Json.Serialization;

namespace Ecosfera.Blazor.UI.Models;

public class GitHubRepository
{
    [JsonPropertyName("stargazers_count")]
    public int StargazersCount { get; set; }
}
