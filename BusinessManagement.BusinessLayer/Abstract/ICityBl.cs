using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ICityBl
{
    IDataResult<IEnumerable<CityDto>> GetAll();
}
