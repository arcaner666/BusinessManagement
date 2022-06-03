using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ICityDal
{
    IEnumerable<City> GetAll();
}
