using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IHouseOwnerAdvBl
{
    IResult Add(HouseOwnerExtDto houseOwnerExtDto);
    IResult Delete(long id);
    IResult DeleteByAccountId(long accountId);
    IResult Update(HouseOwnerExtDto houseOwnerExtDto);
}
