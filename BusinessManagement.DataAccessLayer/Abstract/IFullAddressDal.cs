using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IFullAddressDal
{
    long Add(FullAddressDto fullAddressDto);
    void Delete(long id);
    FullAddressDto GetByAddressText(string addressText);
    FullAddressDto GetById(long id);
    void Update(FullAddressDto fullAddressDto);
}
