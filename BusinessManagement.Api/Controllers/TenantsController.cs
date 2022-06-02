using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class TenantsController : ControllerBase
{
    private readonly ITenantBl _tenantBl;

    public TenantsController(
        ITenantBl tenantBl
    )
    {
        _tenantBl = tenantBl;
    }

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _tenantBl.GetByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
