using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface IBusinessDal
{
    int Add(BusinessDto businessDto);
    BusinessDto GetByBusinessName(string businessName);
    BusinessDto GetById(int id);
    BusinessDto GetByOwnerSystemUserId(long ownerSystemUserId);
}
