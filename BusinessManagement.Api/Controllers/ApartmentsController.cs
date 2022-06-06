using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/apartments/")]
[ApiController]
public class ApartmentsController : ControllerBase
{
    private readonly IApartmentAdvBl _apartmentAdvBl;
    private readonly IApartmentBl _apartmentBl;

    public ApartmentsController(
        IApartmentAdvBl apartmentAdvBl,
        IApartmentBl apartmentBl
    )
    {
        _apartmentAdvBl = apartmentAdvBl;
        _apartmentBl = apartmentBl;
    }

    [HttpPost("add")]
    public IActionResult Add(ApartmentExtDto apartmentExtDto)
    {
        var result = _apartmentAdvBl.Add(apartmentExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var result = _apartmentAdvBl.Delete(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _apartmentBl.GetByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbysectionid/{sectionId}")]
    public IActionResult GetBySectionId(int sectionId)
    {
        var result = _apartmentBl.GetBySectionId(sectionId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _apartmentBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _apartmentBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("update")]
    public IActionResult Update(ApartmentExtDto apartmentExtDto)
    {
        var result = _apartmentAdvBl.Update(apartmentExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
