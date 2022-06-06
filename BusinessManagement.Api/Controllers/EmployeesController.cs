using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/employees/")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeAdvBl _employeeAdvBl;
    private readonly IEmployeeBl _employeeBl;

    public EmployeesController(
        IEmployeeAdvBl employeeAdvBl,
        IEmployeeBl employeeBl
    )
    {
        _employeeAdvBl = employeeAdvBl;
        _employeeBl = employeeBl;
    }

    [HttpPost("add")]
    public IActionResult Add(EmployeeExtDto employeeExtDto)
    {
        var result = _employeeAdvBl.Add(employeeExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(long id)
    {
        var result = _employeeAdvBl.Delete(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("deletebyaccountid/{accountId}")]
    public IActionResult DeleteByAccountId(long accountId)
    {
        var result = _employeeAdvBl.DeleteByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyaccountid/{accountId}")]
    public IActionResult GetExtByAccountId(long accountId)
    {
        var result = _employeeBl.GetExtByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _employeeBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _employeeBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("update")]
    public IActionResult Update(EmployeeExtDto employeeExtDto)
    {
        var result = _employeeAdvBl.Update(employeeExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
