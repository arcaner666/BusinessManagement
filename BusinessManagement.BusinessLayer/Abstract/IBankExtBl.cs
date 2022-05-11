using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IBankExtBl
{
    IResult AddExt(BankExtDto bankExtDto);
    IResult DeleteExt(long id);
    IResult DeleteExtByAccountId(long accountId);
    IDataResult<BankExtDto> GetExtByAccountId(long accountId);
    IDataResult<BankExtDto> GetExtById(long id);
    IDataResult<List<BankExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(BankExtDto bankExtDto);
}
