using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class FlatExtsController : ControllerBase
{
    private readonly IFlatExtBl _flatExtBl;

    public FlatExtsController(
        IFlatExtBl flatExtBl
    )
    {
        _flatExtBl = flatExtBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(FlatExtDto flatExtDto)
    {
        var result = _flatExtBl.AddExt(flatExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(long id)
    {
        var result = _flatExtBl.DeleteExt(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _flatExtBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _flatExtBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(FlatExtDto flatExtDto)
    {
        var result = _flatExtBl.UpdateExt(flatExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
