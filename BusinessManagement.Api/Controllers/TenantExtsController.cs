using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class TenantExtsController : ControllerBase
{
    private readonly ITenantExtBl _tenantExtBl;

    public TenantExtsController(
        ITenantExtBl tenantExtBl
    )
    {
        _tenantExtBl = tenantExtBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(TenantExtDto tenantExtDto)
    {
        var result = _tenantExtBl.AddExt(tenantExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(long id)
    {
        var result = _tenantExtBl.DeleteExt(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteextbyaccountid/{accountId}")]
    public IActionResult DeleteExtByAccountId(long accountId)
    {
        var result = _tenantExtBl.DeleteExtByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyaccountid/{accountId}")]
    public IActionResult GetExtByAccountId(long accountId)
    {
        var result = _tenantExtBl.GetExtByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _tenantExtBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _tenantExtBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(TenantExtDto tenantExtDto)
    {
        var result = _tenantExtBl.UpdateExt(tenantExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
