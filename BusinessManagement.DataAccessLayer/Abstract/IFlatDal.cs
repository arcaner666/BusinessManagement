using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IFlatDal
{
    long Add(FlatDto flatDto);
    void Delete(long id);
    List<FlatDto> GetByApartmentId(long apartmentId);
    FlatDto GetByFlatCode(string flatCode);
    FlatDto GetById(long id);
    FlatExtDto GetExtById(long id);
    List<FlatExtDto> GetExtsByBusinessId(int businessId);
    void Update(FlatDto flatDto);
}
