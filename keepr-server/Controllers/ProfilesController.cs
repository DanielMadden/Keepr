using System;
using System.Collections.Generic;
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
  [Authorize]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _service;
    private readonly KeepsService _serviceKeep;
    private readonly VaultsService _serviceVault;

    public ProfilesController(ProfilesService service, KeepsService serviceKeep, VaultsService serviceVault)
    {
      _service = service;
      _serviceKeep = serviceKeep;
      _serviceVault = serviceVault;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile profile = _service.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/keeps")]
    [AllowAnonymous]
    public ActionResult<IEnumerable<Keep>> GetKeeps(string id)
    {
      try { return Ok(_serviceKeep.GetByProfileId(id)); }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpGet("{id}/vaults")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Vault>>> GetVaults(string id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        if (userInfo == null || userInfo.Id != id) { return Ok(_serviceVault.GetByProfileId(id, true)); }
        return Ok(_serviceVault.GetByProfileId(id));
      }
      catch (Exception err) { return BadRequest(err.Message); }
    }

  }
}