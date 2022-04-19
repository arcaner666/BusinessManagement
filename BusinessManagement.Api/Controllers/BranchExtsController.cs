using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class BranchExtsController : ControllerBase
{
    private readonly IBranchExtBl _branchExtBl;

    public BranchExtsController(
        IBranchExtBl branchExtBl
    )
    {
        _branchExtBl = branchExtBl;
    }

    [HttpPost("addext")]
    public IActionResult AddExt(BranchExtDto branchExtDto)
    {
        var result = _branchExtBl.AddExt(branchExtDto);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpDelete("deleteext/{id}")]
    public IActionResult DeleteExt(long id)
    {
        var result = _branchExtBl.DeleteExt(id);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(int id)
    {
        var result = _branchExtBl.GetExtById(id);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _branchExtBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("updateext")]
    public IActionResult UpdateExt(BranchExtDto branchExtDto)
    {
        var result = _branchExtBl.UpdateExt(branchExtDto);
        if (result.Success) 
            return Ok(result);
        return BadRequest(result);
    }
}
