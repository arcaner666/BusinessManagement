using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICityDal
{
    List<CityDto> GetAll();
}
