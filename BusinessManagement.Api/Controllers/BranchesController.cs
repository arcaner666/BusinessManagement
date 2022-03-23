using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchBl _branchBl;

        public BranchesController(
            IBranchBl branchBl
        )
        {
            _branchBl = branchBl;
        }

        [HttpPost("addext")]
        public IActionResult AddExt(BranchExtDto branchExtDto)
        {
            var result = _branchBl.AddExt(branchExtDto);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("deleteext/{id}")]
        public IActionResult DeleteExt(long id)
        {
            var result = _branchBl.DeleteExt(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("generatebranchcode/{businessId}")]
        public IActionResult GenerateBranchCode(int businessId)
        {
            var result = _branchBl.GenerateBranchCode(businessId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getbybusinessid/{businessId}")]
        public IActionResult GetByBusinessId(int businessId)
        {
            var result = _branchBl.GetByBusinessId(businessId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getextbyid/{id}")]
        public IActionResult GetExtById(int id)
        {
            var result = _branchBl.GetExtById(id);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("updateext")]
        public IActionResult UpdateExt(BranchExtDto branchExtDto)
        {
            var result = _branchBl.UpdateExt(branchExtDto);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
