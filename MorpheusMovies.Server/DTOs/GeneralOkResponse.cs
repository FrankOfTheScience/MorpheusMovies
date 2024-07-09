using Newtonsoft.Json;

namespace MorpheusMovies.Server.DTOs;

public class GeneralOkResponse
{
    [JsonProperty("message")]
    public string? Message { get; set; }
    public object? Data { get; set; }

    public GeneralOkResponse()
    { }
    public GeneralOkResponse(string message)
        => Message = message;
    public GeneralOkResponse(string message, object data)
    {
        Message = message;
        Data = data;
    }
}
