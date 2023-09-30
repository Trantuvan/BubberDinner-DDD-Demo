using BubberDinner.Application.Services.Authentication;
using BubberDinner.Contracts.Authentication;

using Microsoft.AspNetCore.Mvc;

namespace BubberDinner.Api.Controllers;

[ApiController]
[Route("auth")]
//* [ErrorHandlingFilter] manually applied error handling
public class AuthenticationController : ControllerBase
{
  private readonly IAuthenticationService _authenticationService;

  public AuthenticationController(IAuthenticationService authenticationService)
  {
    _authenticationService = authenticationService;
  }

  [HttpPost("register")]
  public IActionResult Register(RegisterRequest request)
  {
    var authResult = _authenticationService.Register(request.FirstName,
                                                     request.LastName,
                                                     request.Email,
                                                     request.Password);

    // * Map authResult to API contract response
    var response = new AuthenticationResponse(authResult.User.Id,
                                              authResult.User.FirstName,
                                              authResult.User.LastName,
                                              authResult.User.Email,
                                              authResult.Token);
    return Ok(response);
  }

  [HttpPost("login")]
  public IActionResult Login(LoginRequest request)
  {
    var authResult = _authenticationService.Login(request.Email,
                                                  request.Password);

    // * Map authResult to API contract response
    var response = new AuthenticationResponse(authResult.User.Id,
                                              authResult.User.FirstName,
                                              authResult.User.LastName,
                                              authResult.User.Email,
                                              authResult.Token);
    return Ok(response);
  }
}