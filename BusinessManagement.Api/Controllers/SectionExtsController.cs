using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class SectionExtsController : ControllerBase
{
    private readonly ISectionExtBl _sectionExtBl;

    public SectionExtsController(
        ISectionExtBl sectionExtBl
    )
    {
        _sectionExtBl = sectionExtBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(SectionExtDto sectionExtDto)
    {
        var result = _sectionExtBl.AddExt(sectionExtDto);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(int id)
    {
        var result = _sectionExtBl.DeleteExt(id);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(int id)
    {
        var result = _sectionExtBl.GetExtById(id);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _sectionExtBl.GetExtsByBusinessId(businessId);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(SectionExtDto sectionExtDto)
    {
        var result = _sectionExtBl.UpdateExt(sectionExtDto);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }
}
