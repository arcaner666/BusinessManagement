using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class CashExtsController : ControllerBase
{
    private readonly ICashExtBl _cashExtBl;

    public CashExtsController(
        ICashExtBl cashExtBl
    )
    {
        _cashExtBl = cashExtBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(CashExtDto cashExtDto)
    {
        var result = _cashExtBl.AddExt(cashExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(long id)
    {
        var result = _cashExtBl.DeleteExt(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _cashExtBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _cashExtBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(CashExtDto cashExtDto)
    {
        var result = _cashExtBl.UpdateExt(cashExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
