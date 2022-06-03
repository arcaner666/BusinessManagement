using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class ManagerExtBl : IManagerExtBl
{
    private readonly IManagerBl _managerBl;
    private readonly IManagerDal _managerDal;

    public ManagerExtBl(
        IManagerBl managerBl,
        IManagerDal managerDal
    )
    {
        _managerBl = managerBl;
        _managerDal = managerDal;
    }

    public IDataResult<IEnumerable<ManagerExtDto>> GetExtsByBusinessId(int businessId)
    {
        IEnumerable<ManagerExtDto> managerExtDtos = _managerDal.GetExtsByBusinessId(businessId);
        if (!managerExtDtos.Any())
            return new ErrorDataResult<IEnumerable<ManagerExtDto>>(Messages.ManagerNotFound);

        return new SuccessDataResult<IEnumerable<ManagerExtDto>>(managerExtDtos, Messages.ManagerExtsListedByBusinessId);
    }
}
