using BusinessManagement.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class AccountTypesController : ControllerBase
{
    private readonly IAccountTypeBl _accountTypeBl;

    public AccountTypesController(
        IAccountTypeBl accountTypeBl
    )
    {
        _accountTypeBl = accountTypeBl;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _accountTypeBl.GetAll();
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getbyaccountgroupname")]
    public IActionResult GetByAccountTypeName(string accountTypeName)
    {
        var result = _accountTypeBl.GetByAccountTypeName(accountTypeName);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
