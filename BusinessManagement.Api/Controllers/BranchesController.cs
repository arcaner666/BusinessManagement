using BusinessManagement.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class BranchesController : ControllerBase
{
    private readonly IBranchBl _branchBl;

    public BranchesController(
        IBranchBl branchBl
    )
    {
        _branchBl = branchBl;
    }

    [HttpGet("generatebranchcode/{businessId}")]
    public IActionResult GenerateBranchCode(int businessId)
    {
        var result = _branchBl.GenerateBranchCode(businessId);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _branchBl.GetByBusinessId(businessId);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
