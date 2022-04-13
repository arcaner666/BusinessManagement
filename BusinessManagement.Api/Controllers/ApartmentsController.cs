using BusinessManagement.BusinessLayer.Abstract;
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

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _apartmentBl.GetByBusinessId(businessId);
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
}
