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

}
