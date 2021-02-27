using System;
using System.Collections.Generic;
using System.Text.Json;
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
  public class VaultKeepsController : ControllerBase
  {

    private readonly VaultKeepsService _serviceVaultKeep;

    public VaultKeepsController(VaultKeepsService serviceVaultKeep)
    {
      _serviceVaultKeep = serviceVaultKeep;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVaultKeep.CreatorId = userInfo.Id;
        return Ok(_serviceVaultKeep.Create(newVaultKeep));
      }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_serviceVaultKeep.Delete(id, userInfo.Id) + " rows deleted");
      }
      catch (Exception err) { return BadRequest(err.Message); }
    }
  }
}