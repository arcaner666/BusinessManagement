using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IFlatAdvBl
{
    IResult Add(FlatExtDto flatExtDto);
    IResult Delete(long id);
    IResult Update(FlatExtDto flatExtDto);
}
