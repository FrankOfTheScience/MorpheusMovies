using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MorpheusMovies.Server.Services.Interfaces;

namespace MorpheusMovies.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ApplicationUserController : ControllerBase
{
    private readonly IUserService _userService;
    public ApplicationUserController(IUserService userService)
        => _userService = userService;


}
