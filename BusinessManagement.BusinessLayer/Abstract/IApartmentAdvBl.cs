using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IApartmentAdvBl
{
    IResult Add(ApartmentExtDto apartmentExtDto);
    IResult Delete(long id);
    IResult Update(ApartmentExtDto apartmentExtDto);
}
