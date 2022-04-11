using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class AccountGroupsController : ControllerBase
{
    private readonly IAccountGroupBl _accountGroupBl;

    public AccountGroupsController(
        IAccountGroupBl accountGroupBl
    )
    {
        _accountGroupBl = accountGroupBl;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _accountGroupBl.GetAll();
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("getbyaccountgroupcodes")]
    public IActionResult GetByAccountGroupCodes(AccountGroupCodesDto accountGroupCodesDto)
    {
        var result = _accountGroupBl.GetByAccountGroupCodes(accountGroupCodesDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
