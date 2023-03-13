using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Application.Contracts;
using Restaurant.Core.Application.Dtos.Account;
using Restaurant.Presentation.WebApi.Core;

namespace Restaurant.Presentation.WebApi.Controllers.v1;

public class AccountController : BaseApiController {
  private readonly IAccountService _accountService;

  public AccountController(IAccountService accountService) {
    _accountService = accountService;
  }

  [HttpPost("authenticate")]
  public async Task<IActionResult> AuthenticateAsync([FromQuery] AuthenticationRequest request) {
    return Ok(await _accountService.AuthenticateAsync(request));
  }

  [HttpPost("register")]
  public async Task<IActionResult> RegisterAsync([FromQuery] RegisterRequest request) {
    var origin = Request.Headers["origin"];
    return Ok(await _accountService.RegisterBasicUserAsync(request, origin));
  }

}
