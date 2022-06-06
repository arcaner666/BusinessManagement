using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IBankAdvBl
{
    IResult Add(BankExtDto bankExtDto);
    IResult Delete(long id);
    IResult DeleteByAccountId(long accountId);
    IResult Update(BankExtDto bankExtDto);
}
