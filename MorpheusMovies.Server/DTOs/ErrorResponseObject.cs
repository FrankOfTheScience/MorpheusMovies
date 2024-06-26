using Newtonsoft.Json;

namespace MorpheusMovies.Server.DTOs;

public class ErrorResponseObject
{
    [JsonProperty("errorMessage")]
    public string ErrorMessage { get; set; }
    [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
    public string ErrorCode { get; set; }

    public ErrorResponseObject()
    { }

    public ErrorResponseObject(string errorMessage, string errorCode = null)
    {
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }
}
