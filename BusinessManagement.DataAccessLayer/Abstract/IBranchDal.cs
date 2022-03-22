using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract
{
    public interface IBranchDal
    {
        Branch Add(Branch branch);
        void Delete(long id);
        Branch GetByBranchCode(string branchCode);
        List<Branch> GetByBusinessId(int businessId);
        Branch GetByBusinessIdAndBranchName(int businessId, string branchName);
        Branch GetByBusinessIdAndBranchOrderOrBranchCode(int businessId, int branchOrder, string branchCode);
        Branch GetByBusinessIdAndMaxBranchOrder(int businessId);
        Branch GetById(long id);
        Branch GetExtById(int id);
        List<Branch> GetExtsByBusinessId(int businessId);
        void Update(Branch branch);
    }
}
