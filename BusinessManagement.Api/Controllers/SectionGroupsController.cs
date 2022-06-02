using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class SectionGroupsController : ControllerBase
{
    private readonly ISectionGroupBl _sectionGroupBl;

    public SectionGroupsController(
        ISectionGroupBl sectionGroupBl
    )
    {
        _sectionGroupBl = sectionGroupBl;
    }

    [HttpPost("add")]
    public IActionResult Add(SectionGroupDto sectionGroupDto)
    {
        var result = _sectionGroupBl.Add(sectionGroupDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(long id)
    {
        var result = _sectionGroupBl.Delete(id);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _sectionGroupBl.GetByBusinessId(businessId);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbyid/{id}")]
    public IActionResult GetById(long id)
    {
        var result = _sectionGroupBl.GetById(id);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("update")]
    public IActionResult Update(SectionGroupDto sectionGroupDto)
    {
        var result = _sectionGroupBl.Update(sectionGroupDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
