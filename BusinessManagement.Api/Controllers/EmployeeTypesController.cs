using BusinessManagement.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/employeetypes/")]
[ApiController]
public class EmployeeTypesController : ControllerBase
{
    private readonly IEmployeeTypeBl _employeeTypeBl;

    public EmployeeTypesController(
        IEmployeeTypeBl employeeTypeBl
    )
    {
        _employeeTypeBl = employeeTypeBl;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _employeeTypeBl.GetAll();
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
