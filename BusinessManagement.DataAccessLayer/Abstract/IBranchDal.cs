using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IBranchDal
{
    long Add(BranchDto branchDto);
    void Delete(long id);
    BranchDto GetByBranchCode(string branchCode);
    List<BranchDto> GetByBusinessId(int businessId);
    BranchDto GetByBusinessIdAndBranchName(int businessId, string branchName);
    BranchDto GetByBusinessIdAndBranchOrderOrBranchCode(int businessId, int branchOrder, string branchCode);
    BranchDto GetByBusinessIdAndMaxBranchOrder(int businessId);
    BranchDto GetById(long id);
    BranchExtDto GetExtById(long id);
    List<BranchExtDto> GetExtsByBusinessId(int businessId);
    void Update(BranchDto branchDto);
}
