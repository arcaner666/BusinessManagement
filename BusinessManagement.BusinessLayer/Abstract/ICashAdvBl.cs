using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ICashAdvBl
{
    IResult Add(CashExtDto cashExtDto);
    IResult Delete(long id);
    IResult DeleteByAccountId(long accountId);
    IResult Update(CashExtDto cashExtDto);
}
