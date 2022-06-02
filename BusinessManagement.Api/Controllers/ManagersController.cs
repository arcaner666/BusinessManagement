using BusinessManagement.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class ManagersController : ControllerBase
{
    private readonly IManagerBl _managerBl;

    public ManagersController(
        IManagerBl managerBl
    )
    {
        _managerBl = managerBl;
    }

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _managerBl.GetByBusinessId(businessId);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
