using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IBranchExtBl
{
    IResult AddExt(BranchExtDto branchExtDto);
    IResult DeleteExt(long id);
    IDataResult<BranchExtDto> GetExtById(long id);
    IDataResult<IEnumerable<BranchExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(BranchExtDto branchExtDto);
}
