using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class CurrenciesController : ControllerBase
{
    private readonly ICurrencyBl _currencyBl;

    public CurrenciesController(
        ICurrencyBl currencyBl
    )
    {
        _currencyBl = currencyBl;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _currencyBl.GetAll();
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }
}
