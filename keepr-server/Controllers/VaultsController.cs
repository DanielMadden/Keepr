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
  [Authorize]
  public class VaultsController : ControllerBase
  {

    private readonly VaultsService _serviceVault;
    private readonly VaultKeepsService _serviceVaultKeep;

    public VaultsController(VaultsService serviceVault, VaultKeepsService serviceVaultKeep)
    {
      _serviceVault = serviceVault;
      _serviceVaultKeep = serviceVaultKeep;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<Vault>> GetById(int id)
    {
      try
      {
        if (_serviceVault.CheckPrivate(id))
        {
          Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
          return Ok(_serviceVault.GetById(id, userInfo.Id));
        }
        else
        {
          return Ok(_serviceVault.GetById(id));
        }
      }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpGet("{vaultId}/keeps")]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<Keep>>> GetKeepsByVaultId(int vaultId)
    {
      try
      {
        if (_serviceVault.CheckPrivate(vaultId))
        {
          Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
          _serviceVault.GetById(vaultId, userInfo.Id);
          return Ok(_serviceVaultKeep.GetKeepsByVaultId(vaultId));
        }
        else
        {
          _serviceVault.GetById(vaultId);
          return Ok(_serviceVaultKeep.GetKeepsByVaultId(vaultId));
        }
      }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newVault.CreatorId = userInfo.Id;
        return Ok(_serviceVault.Create(newVault));
      }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit(int id, [FromBody] JsonElement edits)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_serviceVault.Edit(id, edits, userInfo.Id));
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
        return Ok(_serviceVault.Delete(id, userInfo.Id) + " rows deleted");
      }
      catch (Exception err) { return BadRequest(err.Message); }
    }
  }
}