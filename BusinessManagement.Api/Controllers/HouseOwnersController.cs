using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Api.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class HouseOwnersController : ControllerBase
{
    private readonly IHouseOwnerBl _houseOwnerBl;

    public HouseOwnersController(
        IHouseOwnerBl houseOwnerBl
    )
    {
        _houseOwnerBl = houseOwnerBl;
    }

}
