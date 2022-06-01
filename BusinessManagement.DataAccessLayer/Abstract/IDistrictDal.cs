using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IDistrictDal
{
    List<DistrictDto> GetAll();
    List<DistrictDto> GetByCityId(short cityId);
}
