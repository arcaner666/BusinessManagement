using BusinessManagement.BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly IDistrictBl _districtBl;

        public DistrictsController(
            IDistrictBl districtBl
        )
        {
            _districtBl = districtBl;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _districtBl.GetAll();
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getbycityid/{cityId}")]
        public IActionResult GetByCityId(short cityId)
        {
            var result = _districtBl.GetByCityId(cityId);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
