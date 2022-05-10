using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class HouseOwnerExtsController : ControllerBase
{
    private readonly IHouseOwnerExtBl _houseOwnerExtBl;

    public HouseOwnerExtsController(
        IHouseOwnerExtBl houseOwnerExtBl
    )
    {
        _houseOwnerExtBl = houseOwnerExtBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(HouseOwnerExtDto houseOwnerExtDto)
    {
        var result = _houseOwnerExtBl.AddExt(houseOwnerExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(long id)
    {
        var result = _houseOwnerExtBl.DeleteExt(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyaccountid/{accountId}")]
    public IActionResult GetExtByAccountId(long accountId)
    {
        var result = _houseOwnerExtBl.GetExtByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _houseOwnerExtBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _houseOwnerExtBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(HouseOwnerExtDto houseOwnerExtDto)
    {
        var result = _houseOwnerExtBl.UpdateExt(houseOwnerExtDto);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
