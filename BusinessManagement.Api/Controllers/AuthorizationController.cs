using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationBl _authorizationBl;

        public AuthorizationController(
            IAuthorizationBl authorizationBl
        )
        {
            _authorizationBl = authorizationBl;
        }

        [HttpPost("registersectionmanager")]
        public IActionResult RegisterSectionManager(ManagerExtDto managerExtDto)
        {
            var result = _authorizationBl.RegisterSectionManager(managerExtDto);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
