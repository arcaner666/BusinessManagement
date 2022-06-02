using BusinessManagement.BusinessLayer.Abstract;
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

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _sectionBl.GetByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
