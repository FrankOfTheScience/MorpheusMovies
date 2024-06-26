using Newtonsoft.Json;

namespace MorpheusMovies.Server.DTOs;

public class ReccomendationResponse
{
    //TODO: Change response type to include title 
    [JsonProperty("SuggestedMovies")]
    public List<int> SuggestedMovies { get; set; }
}
