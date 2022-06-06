using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/accounts/")]
[ApiController]
public class AccountExtsController : ControllerBase
{
    private readonly IAccountAdvBl _accountAdvBl;
    private readonly IAccountBl _accountBl;

    public AccountExtsController(
        IAccountAdvBl accountAdvBl,
        IAccountBl accountBl
    )
    {
        _accountAdvBl = accountAdvBl;
        _accountBl = accountBl;
    }

    [HttpPost("add")]
    public IActionResult Add(AccountExtDto accountExtDto)
    {
        var result = _accountAdvBl.Add(accountExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(long id)
    {
        var result = _accountAdvBl.Delete(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("generateaccountcode/{businessId}/{branchId}/{accountGroupCode}")]
    public IActionResult GenerateAccountCode(int businessId, long branchId, string accountGroupCode)
    {
        var result = _accountAdvBl.GenerateAccountCode(businessId, branchId, accountGroupCode);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _accountBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _accountBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("getextsbybusinessidandaccountgroupcodes")]
    public IActionResult GetExtsByBusinessIdAndAccountGroupCodes(AccountGetByAccountGroupCodesDto accountGetByAccountGroupCodesDto)
    {
        var result = _accountBl.GetExtsByBusinessIdAndAccountGroupCodes(accountGetByAccountGroupCodesDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("update")]
    public IActionResult Update(AccountExtDto accountExtDto)
    {
        var result = _accountAdvBl.Update(accountExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
