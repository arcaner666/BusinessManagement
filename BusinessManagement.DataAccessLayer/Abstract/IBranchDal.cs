using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IBranchDal
{
    long Add(Branch branch);
    void Delete(long id);
    Branch GetByBranchCode(string branchCode);
    List<Branch> GetByBusinessId(int businessId);
    Branch GetByBusinessIdAndBranchName(int businessId, string branchName);
    Branch GetByBusinessIdAndBranchOrderOrBranchCode(int businessId, int branchOrder, string branchCode);
    Branch GetById(long id);
    BranchExt GetExtById(long id);
    List<BranchExt> GetExtsByBusinessId(int businessId);
    int GetMaxBranchOrderByBusinessId(int businessId);
    void Update(Branch branch);
}
