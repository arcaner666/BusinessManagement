using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/sections/")]
[ApiController]
public class SectionsController : ControllerBase
{
    private readonly ISectionAdvBl _sectionAdvBl;
    private readonly ISectionBl _sectionBl;

    public SectionsController(
        ISectionAdvBl sectionAdvBl,
        ISectionBl sectionBl
    )
    {
        _sectionAdvBl = sectionAdvBl;
        _sectionBl = sectionBl;
    }

    [HttpPost("add")]
    public IActionResult Add(SectionExtDto sectionExtDto)
    {
        var result = _sectionAdvBl.Add(sectionExtDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var result = _sectionAdvBl.Delete(id);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _sectionBl.GetByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(int id)
    {
        var result = _sectionBl.GetExtById(id);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _sectionBl.GetExtsByBusinessId(businessId);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("update")]
    public IActionResult Update(SectionExtDto sectionExtDto)
    {
        var result = _sectionAdvBl.Update(sectionExtDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
