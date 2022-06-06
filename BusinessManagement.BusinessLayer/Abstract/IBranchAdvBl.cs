using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IBranchAdvBl
{
    IResult Add(BranchExtDto branchExtDto);
    IResult Delete(long id);
    IResult Update(BranchExtDto branchExtDto);
}
