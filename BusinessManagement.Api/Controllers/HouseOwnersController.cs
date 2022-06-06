using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/houseowners/")]
[ApiController]
public class HouseOwnersController : ControllerBase
{
    private readonly IHouseOwnerAdvBl _houseOwnerAdvBl;
    private readonly IHouseOwnerBl _houseOwnerBl;

    public HouseOwnersController(
        IHouseOwnerAdvBl houseOwnerAdvBl,
        IHouseOwnerBl houseOwnerBl
    )
    {
        _houseOwnerAdvBl = houseOwnerAdvBl;
        _houseOwnerBl = houseOwnerBl;
    }

    [HttpPost("add")]
    public IActionResult Add(HouseOwnerExtDto houseOwnerExtDto)
    {
        var result = _houseOwnerAdvBl.Add(houseOwnerExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(long id)
    {
        var result = _houseOwnerAdvBl.Delete(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("deletebyaccountid/{accountId}")]
    public IActionResult DeleteByAccountId(long accountId)
    {
        var result = _houseOwnerAdvBl.DeleteByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _houseOwnerBl.GetByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyaccountid/{accountId}")]
    public IActionResult GetExtByAccountId(long accountId)
    {
        var result = _houseOwnerBl.GetExtByAccountId(accountId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _houseOwnerBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _houseOwnerBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("update")]
    public IActionResult Update(HouseOwnerExtDto houseOwnerExtDto)
    {
        var result = _houseOwnerAdvBl.Update(houseOwnerExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
