using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class EmployeeExtsController : ControllerBase
{
    private readonly IEmployeeExtBl _employeeExtBl;

    public EmployeeExtsController(
        IEmployeeExtBl employeeExtBl
    )
    {
        _employeeExtBl = employeeExtBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(EmployeeExtDto employeeExtDto)
    {
        var result = _employeeExtBl.AddExt(employeeExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(long id)
    {
        var result = _employeeExtBl.DeleteExt(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteextbyaccountid/{accountId}")]
    public IActionResult DeleteExtByAccountId(long accountId)
    {
        var result = _employeeExtBl.DeleteExtByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyaccountid/{accountId}")]
    public IActionResult GetExtByAccountId(long accountId)
    {
        var result = _employeeExtBl.GetExtByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _employeeExtBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _employeeExtBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(EmployeeExtDto employeeExtDto)
    {
        var result = _employeeExtBl.UpdateExt(employeeExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
