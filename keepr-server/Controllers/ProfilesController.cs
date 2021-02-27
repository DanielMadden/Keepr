using System;
using System.Collections.Generic;
using keepr_server.Models;
using keepr_server.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr_server.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
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
    public ActionResult<IEnumerable<Keep>> GetKeeps(string id)
    {
      try { return Ok(_serviceKeep.GetByProfileId(id)); }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpGet("{id}/vaults")]
    public ActionResult<IEnumerable<Vault>> GetVaults(string id)
    {
      try { return Ok(_serviceVault.GetByProfileId(id)); }
      catch (Exception err) { return BadRequest(err.Message); }
    }

  }
}