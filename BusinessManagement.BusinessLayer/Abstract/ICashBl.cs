using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ICashBl
{
    IDataResult<CashDto> Add(CashDto cashDto);
    IResult Delete(long id);
    IDataResult<CashDto> GetByAccountId(long accountId);
    IDataResult<IEnumerable<CashDto>> GetByBusinessId(int businessId);
    IDataResult<CashDto> GetById(long id);
    IResult Update(CashDto cashDto);
}
