using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr_server.Models;
using keepr_server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr_server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  // REVIEW[epic=Authentication] this tag enforces the user must be logged in
  [Authorize]
  public class AccountController : ControllerBase
  {
    private readonly ProfilesService _ps;

    public AccountController(ProfilesService ps)
    {
      _ps = ps;
    }

    [HttpGet]
    // REVIEW[epic=Authentication] async calls must return a System.Threading.Tasks, this is equivalent to a promise in JS
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        // REVIEW[epic=Authentication] how to get the user info from the request token
        // same as to req.userInfo
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_ps.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}