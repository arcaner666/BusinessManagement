using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IFullAddressDal
{
    long Add(FullAddress fullAddress);
    void Delete(long id);
    FullAddress GetByAddressText(string addressText);
    FullAddress GetById(long id);
    void Update(FullAddress fullAddress);
}
