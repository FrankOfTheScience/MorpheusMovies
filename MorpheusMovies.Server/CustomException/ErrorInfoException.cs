using MorpheusMovies.Server.DTOs;

namespace MorpheusMovies.Server.CustomException;

public class ErrorInfoException : Exception
{
    public ErrorResponseObject ErrorResponse { get; set; }

    public ErrorInfoException() : base()
    { }

    public ErrorInfoException(string message) : base(message)
    { }

    public ErrorInfoException(string message, Exception innerException) : base(message, innerException)
    { }

    public ErrorInfoException(ErrorResponseObject errorResponse) : base()
        => ErrorResponse = errorResponse;

    public ErrorInfoException(ErrorResponseObject errorResponse, string message) : base(message)
        => ErrorResponse = errorResponse;

    public ErrorInfoException(ErrorResponseObject errorResponse, string message, Exception innerException) : base(message, innerException)
        => ErrorResponse = errorResponse;
}
