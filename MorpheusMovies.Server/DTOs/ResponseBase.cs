using MorpheusMovies.Server.Utilities;
using Newtonsoft.Json;

namespace MorpheusMovies.Server.DTOs;

public class ResponseBase
{
    [JsonProperty("status")]
    public string Status { get; set; }

    public ResponseBase()
    { }

    public ResponseBase(bool isOk)
        => Status = isOk ? MorpheusMoviesConstants.ResponseConstants.STATUS_OK : MorpheusMoviesConstants.ResponseConstants.STATUS_KO;
}

public class OkResponse<T> : ResponseBase
{
    [JsonProperty("Response")]
    public T Response { get; set; }

    public OkResponse()
    { }

    public OkResponse(T response) : base(true)
        => Response = response;
}

public class KoResponse : ResponseBase
{
    [JsonProperty("Error")]
    public ErrorResponseObject Error { get; set; }

    public KoResponse()
    { }

    public KoResponse(ErrorResponseObject error) : base(false)
        => Error = error;
}
