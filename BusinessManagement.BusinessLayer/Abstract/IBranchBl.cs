using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract
{
    public interface IBranchBl
    {
        IDataResult<BranchDto> Add(BranchDto branchDto);
    }
}
