using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class FlatsController : ControllerBase
{
    private readonly IFlatBl _flatBl;

    public FlatsController(
        IFlatBl flatBl
    )
    {
        _flatBl = flatBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(FlatExtDto flatExtDto)
    {
        var result = _flatBl.AddExt(flatExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(long id)
    {
        var result = _flatBl.DeleteExt(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _flatBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _flatBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(FlatExtDto flatExtDto)
    {
        var result = _flatBl.UpdateExt(flatExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
