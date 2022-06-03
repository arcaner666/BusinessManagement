using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IDistrictDal
{
    IEnumerable<DistrictDto> GetAll();
    IEnumerable<DistrictDto> GetByCityId(short cityId);
}
