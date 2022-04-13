using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class ApartmentExtsController : ControllerBase
{
    private readonly IApartmentExtBl _apartmentExtBl;

    public ApartmentExtsController(
        IApartmentExtBl apartmentExtBl
    )
    {
        _apartmentExtBl = apartmentExtBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(ApartmentExtDto apartmentExtDto)
    {
        var result = _apartmentExtBl.AddExt(apartmentExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(int id)
    {
        var result = _apartmentExtBl.DeleteExt(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _apartmentExtBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _apartmentExtBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(ApartmentExtDto apartmentExtDto)
    {
        var result = _apartmentExtBl.UpdateExt(apartmentExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
