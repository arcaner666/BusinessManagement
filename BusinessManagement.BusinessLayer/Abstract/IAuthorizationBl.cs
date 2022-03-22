using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract
{
    public interface IAuthorizationBl
    {
        IResult RegisterSectionManager(ManagerExtDto managerExtDto);
    }
}
