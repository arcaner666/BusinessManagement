using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class SectionsController : ControllerBase
{
    private readonly ISectionBl _sectionBl;

    public SectionsController(
        ISectionBl sectionBl
    )
    {
        _sectionBl = sectionBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(SectionExtDto sectionExtDto)
    {
        var result = _sectionBl.AddExt(sectionExtDto);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(int id)
    {
        var result = _sectionBl.DeleteExt(id);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(int id)
    {
        var result = _sectionBl.GetExtById(id);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _sectionBl.GetExtsByBusinessId(businessId);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(SectionExtDto sectionExtDto)
    {
        var result = _sectionBl.UpdateExt(sectionExtDto);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }
}
