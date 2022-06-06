using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/accounttypes/")]
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
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbyaccounttypename")]
    public IActionResult GetByAccountTypeName(string accountTypeName)
    {
        var result = _accountTypeBl.GetByAccountTypeName(accountTypeName);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("getbyaccounttypenames")]
    public IActionResult GetByAccountTypeNames(AccountTypeNamesDto accountTypeNamesDto)
    {
        var result = _accountTypeBl.GetByAccountTypeNames(accountTypeNamesDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
