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
  public class KeepsController : ControllerBase
  {

    private readonly KeepsService _serviceKeep;

    public KeepsController(KeepsService serviceKeep)
    {
      _serviceKeep = serviceKeep;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Keep>> GetAll()
    {
      try { return Ok(_serviceKeep.GetAll()); }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpGet("{id}")]
    public ActionResult<Keep> GetById(int id)
    {
      try { return Ok(_serviceKeep.GetById(id)); }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        newKeep.CreatorId = userInfo.Id;
        return Ok(_serviceKeep.Create(newKeep));
      }
      catch (Exception err) { return BadRequest(err.Message); }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit(int id, [FromBody] JsonElement edits)
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_serviceKeep.Edit(id, edits, userInfo.Id));
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
        return Ok(_serviceKeep.Delete(id, userInfo.Id) + " rows deleted");
      }
      catch (Exception err) { return BadRequest(err.Message); }
    }
  }
}