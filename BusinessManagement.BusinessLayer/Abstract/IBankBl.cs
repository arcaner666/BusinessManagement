using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IBankBl
{
    IDataResult<BankDto> Add(BankDto bankDto);
    IResult Delete(long id);
    IDataResult<BankDto> GetByAccountId(long accountId);
    IDataResult<List<BankDto>> GetByBusinessId(int businessId);
    IDataResult<BankDto> GetById(long id);
    IDataResult<BankExtDto> GetExtByAccountId(long accountId);
    IDataResult<BankExtDto> GetExtById(long id);
    IDataResult<List<BankExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(BankDto bankDto);
}
