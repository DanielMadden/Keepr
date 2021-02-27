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
    public ActionResult<Vault> GetById(int id)
    {
      try { return Ok(_serviceVault.GetById(id)); }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpGet("{vaultId}/keeps")]
    public ActionResult<IEnumerable<Keep>> GetKeepsByVaultId(int vaultId)
    {
      try { return Ok(_serviceVaultKeep.GetKeepsByVaultId(vaultId)); }
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
    public ActionResult<Vault> Edit(int id, [FromBody] JsonElement edits)
    {
      try { return Ok(_serviceVault.Edit(id, edits)); }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public ActionResult<string> Delete(int id)
    {
      try { return Ok(_serviceVault.Delete(id) + " rows deleted"); }
      catch (Exception err) { return BadRequest(err.Message); }
    }
  }
}