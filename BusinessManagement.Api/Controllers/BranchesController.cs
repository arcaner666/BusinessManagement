using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/branches/")]
[ApiController]
public class BranchesController : ControllerBase
{
    private readonly IBranchAdvBl _branchAdvBl;
    private readonly IBranchBl _branchBl;

    public BranchesController(
        IBranchAdvBl branchAdvBl,
        IBranchBl branchBl
    )
    {
        _branchAdvBl = branchAdvBl;
        _branchBl = branchBl;
    }

    [HttpPost("add")]
    public IActionResult Add(BranchExtDto branchExtDto)
    {
        var result = _branchAdvBl.Add(branchExtDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(long id)
    {
        var result = _branchAdvBl.Delete(id);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("generatebranchcode/{businessId}")]
    public IActionResult GenerateBranchCode(int businessId)
    {
        var result = _branchBl.GenerateBranchCode(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getbybusinessid/{businessId}")]
    public IActionResult GetByBusinessId(int businessId)
    {
        var result = _branchBl.GetByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextbyid/{id}")]
    public IActionResult GetExtById(int id)
    {
        var result = _branchBl.GetExtById(id);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("getextsbybusinessid/{businessId}")]
    public IActionResult GetExtsByBusinessId(int businessId)
    {
        var result = _branchBl.GetExtsByBusinessId(businessId);
        if (result.Success)
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("update")]
    public IActionResult Update(BranchExtDto branchExtDto)
    {
        var result = _branchAdvBl.Update(branchExtDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
