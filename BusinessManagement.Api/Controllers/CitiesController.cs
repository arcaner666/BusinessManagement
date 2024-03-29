﻿using BusinessManagement.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/cities/")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly ICityBl _cityBl;

    public CitiesController(
        ICityBl cityBl
    )
    {
        _cityBl = cityBl;
    }

    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _cityBl.GetAll();
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
