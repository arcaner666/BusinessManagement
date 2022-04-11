using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountBl _accountBl;

    public AccountsController(
        IAccountBl accountBl
    )
    {
        _accountBl = accountBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(AccountExtDto accountExtDto)
    {
        var result = _accountBl.AddExt(accountExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(long id)
    {
        var result = _accountBl.DeleteExt(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("generateaccountcode/{businessId}/{branchId}/{accountGroupCode}")]
    public IActionResult GenerateAccountCode(int businessId, long branchId, string accountGroupCode)
    {
        var result = _accountBl.GenerateAccountCode(businessId, branchId, accountGroupCode);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _accountBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _accountBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("getextsbybusinessidandaccountgroupcodes")]
    public IActionResult GetExtsByBusinessIdAndAccountGroupCodes(AccountGetByAccountGroupCodesDto accountGetByAccountGroupCodesDto)
    {
        var result = _accountBl.GetExtsByBusinessIdAndAccountGroupCodes(accountGetByAccountGroupCodesDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(AccountExtDto accountExtDto)
    {
        var result = _accountBl.UpdateExt(accountExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
