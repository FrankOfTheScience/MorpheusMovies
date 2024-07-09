using Microsoft.AspNetCore.Mvc;

namespace MorpheusMovies.Server.CustomException;

public class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object error) : base(error)
        => StatusCode = StatusCodes.Status500InternalServerError;
}
