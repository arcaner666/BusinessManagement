using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class ApartmentsController : ControllerBase
{
    private readonly IApartmentBl _apartmentBl;

    public ApartmentsController(
        IApartmentBl apartmentBl
    )
    {
        _apartmentBl = apartmentBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(ApartmentExtDto apartmentExtDto)
    {
        var result = _apartmentBl.AddExt(apartmentExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(int id)
    {
        var result = _apartmentBl.DeleteExt(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getbyid/{id}")]
    public IActionResult GetById(int id)
    {
        var result = _apartmentBl.GetById(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getbysectionid/{sectionId}")]
    public IActionResult GetBySectionId(int sectionId)
    {
        var result = _apartmentBl.GetBySectionId(sectionId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _apartmentBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("update")]
    public IActionResult Update(ApartmentDto apartmentDto)
    {
        var result = _apartmentBl.Update(apartmentDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
