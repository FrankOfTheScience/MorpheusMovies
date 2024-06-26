using Newtonsoft.Json;

namespace MorpheusMovies.Server.DTOs;

public class GeneralOkResponse
{
    [JsonProperty("message")]
    public string Message { get; set; }

    public GeneralOkResponse(string message)
        => Message = message;
}
