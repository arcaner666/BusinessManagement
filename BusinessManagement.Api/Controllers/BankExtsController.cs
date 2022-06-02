using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class BankExtsController : ControllerBase
{
    private readonly IBankExtBl _bankExtBl;

    public BankExtsController(
        IBankExtBl bankExtBl
    )
    {
        _bankExtBl = bankExtBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(BankExtDto bankExtDto)
    {
        var result = _bankExtBl.AddExt(bankExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(long id)
    {
        var result = _bankExtBl.DeleteExt(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("deleteextbyaccountid/{accountId}")]
    public IActionResult DeleteExtByAccountId(long accountId)
    {
        var result = _bankExtBl.DeleteExtByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyaccountid/{accountId}")]
    public IActionResult GetExtByAccountId(long accountId)
    {
        var result = _bankExtBl.GetExtByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _bankExtBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _bankExtBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(BankExtDto bankExtDto)
    {
        var result = _bankExtBl.UpdateExt(bankExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
