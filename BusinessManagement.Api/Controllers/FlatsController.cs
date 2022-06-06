using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/flats/")]
[ApiController]
public class FlatsController : ControllerBase
{
    private readonly IFlatAdvBl _flatAdvBl;
    private readonly IFlatBl _flatBl;

    public FlatsController(
        IFlatAdvBl flatAdvBl,
        IFlatBl flatBl
    )
    {
        _flatAdvBl = flatAdvBl;
        _flatBl = flatBl;
    }

    [HttpPost("add")]
    public IActionResult Add(FlatExtDto flatExtDto)
    {
        var result = _flatAdvBl.Add(flatExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(long id)
    {
        var result = _flatAdvBl.Delete(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(long id)
    {
        var result = _flatBl.GetExtById(id);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _flatBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("update")]
    public IActionResult Update(FlatExtDto flatExtDto)
    {
        var result = _flatAdvBl.Update(flatExtDto);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
