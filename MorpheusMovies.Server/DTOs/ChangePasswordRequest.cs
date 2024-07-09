namespace MorpheusMovies.Server.DTOs;

public class ChangePasswordRequest
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
}