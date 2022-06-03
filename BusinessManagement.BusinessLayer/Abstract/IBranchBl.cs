using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IBranchBl
{
    IDataResult<BranchDto> Add(BranchDto branchDto);
    IResult Delete(long id);
    IDataResult<BranchCodeDto> GenerateBranchCode(int businessId);
    IDataResult<IEnumerable<BranchDto>> GetByBusinessId(int businessId);
    IDataResult<BranchDto> GetById(long id);
    IResult Update(BranchDto branchDto);
}
