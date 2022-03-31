using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IDistrictDal
{
    List<District> GetAll();
    List<District> GetByCityId(short cityId);
}
