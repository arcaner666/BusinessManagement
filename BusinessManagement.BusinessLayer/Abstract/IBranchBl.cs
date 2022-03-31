using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IBranchBl
{
    IDataResult<BranchDto> Add(BranchDto branchDto);
    IResult AddExt(BranchExtDto branchExtDto);
    IResult Delete(long id);
    IResult DeleteExt(long id);
    IDataResult<BranchCodeDto> GenerateBranchCode(int businessId);
    IDataResult<List<BranchDto>> GetByBusinessId(int businessId);
    IDataResult<BranchDto> GetById(long id);
    IDataResult<BranchExtDto> GetExtById(long id);
    IResult Update(BranchDto branchDto);
    IResult UpdateExt(BranchExtDto branchExtDto);
}
