using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IAccountAdvBl
{
    IResult Add(AccountExtDto accountExtDto);
    IResult Delete(long id);
    IDataResult<AccountCodeDto> GenerateAccountCode(int businessId, long branchId, string accountGroupCode);
    IResult Update(AccountExtDto accountExtDto);
}
