using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Extensions;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

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

    [HttpPost("loginwithemail")]
    public IActionResult LoginWithEmail(AuthorizationDto authorizationDto)
    {
        var result = _authorizationBl.LoginWithEmail(authorizationDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("loginwithphone")]
    public IActionResult LoginWithPhone(AuthorizationDto authorizationDto)
    {
        var result = _authorizationBl.LoginWithPhone(authorizationDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpGet("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        var result = _authorizationBl.Logout(Convert.ToInt32(HttpContext.User.ClaimSystemUserId().FirstOrDefault()));
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("refreshaccesstoken")]
    [Authorize]
    public IActionResult RefreshAccessToken(AuthorizationDto authorizationDto)
    {
        var result = _authorizationBl.RefreshAccessToken(authorizationDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }

    [HttpPost("registersectionmanager")]
    public IActionResult RegisterSectionManager(RegisterSectionManagerDto registerSectionManagerDto)
    {
        var result = _authorizationBl.RegisterSectionManager(registerSectionManagerDto);
        if (result.Success) 
            return Ok(result);
        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}
