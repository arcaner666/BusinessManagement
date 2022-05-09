using BusinessManagement.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class HouseOwnersController : ControllerBase
{
    private readonly IHouseOwnerBl _houseOwnerBl;

    public HouseOwnersController(
        IHouseOwnerBl houseOwnerBl
    )
    {
        _houseOwnerBl = houseOwnerBl;
    }

    [HttpGet("getbyaccountid/{accountId}")]
    public IActionResult GetByAccountId(long accountId)
    {
        var result = _houseOwnerBl.GetByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _houseOwnerBl.GetByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
