using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IBranchBl
{
    IDataResult<BranchDto> Add(BranchDto branchDto);
    IResult Delete(long id);
    IDataResult<BranchCodeDto> GenerateBranchCode(int businessId);
    IDataResult<List<BranchDto>> GetByBusinessId(int businessId);
    IDataResult<BranchDto> GetById(long id);
    IDataResult<BranchExtDto> GetExtById(long id);
    IDataResult<List<BranchExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(BranchDto branchDto);
}
