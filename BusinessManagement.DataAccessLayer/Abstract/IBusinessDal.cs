using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IBusinessDal
{
    Business Add(Business business);
    Business GetByBusinessName(string businessName);
    Business GetById(int id);
    Business GetByOwnerSystemUserId(long ownerSystemUserId);
}
