using Microsoft.AspNetCore.Mvc;
using MorpheusMovies.Server.CustomException;
using MorpheusMovies.Server.DTOs;

namespace MorpheusMovies.Server.Utilities;

public static class ApiUtilities
{
    public static IActionResult GenerateKoResponse(ErrorResponseObject error) => error.ErrorCode switch
    {
        MorpheusMoviesConstants.ResponseConstants.AUTH_ERROR_CODE => new UnauthorizedObjectResult(new KoResponse(error)),
        MorpheusMoviesConstants.ResponseConstants.CLIENT_ERROR_CODE => new BadRequestObjectResult(new KoResponse(error)),
        MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE => new InternalServerErrorObjectResult(new KoResponse(error)),
        MorpheusMoviesConstants.ResponseConstants.TRANSIENT_ERROR_CODE => new InternalServerErrorObjectResult(new KoResponse(error)),
        _ => new InternalServerErrorObjectResult(new ErrorResponseObject(MorpheusMoviesConstants.ResponseConstants.GENERAL_ERROR, MorpheusMoviesConstants.ResponseConstants.SERVER_ERROR_CODE))
    };
}

