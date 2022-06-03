using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IDistrictDal
{
    IEnumerable<District> GetAll();
    IEnumerable<District> GetByCityId(short cityId);
}
