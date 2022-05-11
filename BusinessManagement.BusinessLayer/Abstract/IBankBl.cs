using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IBankBl
{
    IDataResult<BankDto> Add(BankDto bankDto);
    IResult Delete(long id);
    IDataResult<BankDto> GetByAccountId(long accountId);
    IDataResult<List<BankDto>> GetByBusinessId(int businessId);
    IDataResult<BankDto> GetById(long id);
    IResult Update(BankDto bankDto);
}
