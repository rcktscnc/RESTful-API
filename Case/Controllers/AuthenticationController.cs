using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Case.Data;
using Case.Auth;

namespace Case.Controllers {
  [Route("api/auth")]
  [ApiController]
  public class AuthenticationController : ControllerBase {
    private readonly IJwtProvider _JwtProvider;

    public class ResultFormat {
      public string Token;
    }

    public AuthenticationController(IJwtProvider jwtProvider) {
      _JwtProvider = jwtProvider;
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult<ResultFormat> Get() {
      return new ResultFormat() { Token = _JwtProvider.GetToken() };
    }
  }
}
