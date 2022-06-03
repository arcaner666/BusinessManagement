using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class BranchExtBl : IBranchExtBl
{
    private readonly IBranchBl _branchBl;
    private readonly IBranchDal _branchDal;
    private readonly IFullAddressBl _fullAddressBl;

    public BranchExtBl(
        IBranchBl branchBl,
        IBranchDal branchDal,
        IFullAddressBl fullAddressBl
    )
    {
        _branchBl = branchBl;
        _branchDal = branchDal;
        _fullAddressBl = fullAddressBl;
    }

    [TransactionScopeAspect]
    public IResult AddExt(BranchExtDto branchExtDto)
    {
        // Şubenin adresi eklenir.
        FullAddressDto fullAddressDto = new()
        {
            CityId = branchExtDto.CityId,
            DistrictId = branchExtDto.DistrictId,
            AddressTitle = branchExtDto.AddressTitle,
            PostalCode = branchExtDto.PostalCode,
            AddressText = branchExtDto.AddressText,
        };
        var addFullAddressResult = _fullAddressBl.Add(fullAddressDto);
        if (!addFullAddressResult.Success)
            return addFullAddressResult;

        // Şube eklenir.
        BranchDto branchDto = new()
        {
            BusinessId = branchExtDto.BusinessId,
            FullAddressId = addFullAddressResult.Data.FullAddressId,
            BranchOrder = branchExtDto.BranchOrder,
            BranchName = branchExtDto.BranchName,
            BranchCode = branchExtDto.BranchCode,
        };
        var addBranchResult = _branchBl.Add(branchDto);
        if (!addBranchResult.Success)
            return addBranchResult;

        return new SuccessResult(Messages.BranchExtAdded);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(long id)
    {
        // Şube getirilir.
        var getBranchResult = _branchBl.GetById(id);
        if (!getBranchResult.Success)
            return getBranchResult;

        // Merkez şubenin silinmesi engellenir.
        if (getBranchResult.Data.BranchName == "Merkez")
            return new ErrorResult(Messages.BranchCanNotDeleteMainBranch);

        // Şube silinir.
        var deleteBranchResult = _branchBl.Delete(id);
        if (!deleteBranchResult.Success)
            return deleteBranchResult;

        // Şubenin adresi silinir.
        var deleteFullAddressResult = _fullAddressBl.Delete(getBranchResult.Data.FullAddressId);
        if (!deleteFullAddressResult.Success)
            return deleteFullAddressResult;

        return new SuccessResult(Messages.BranchExtDeleted);
    }

    public IDataResult<BranchExtDto> GetExtById(long id)
    {
        BranchExtDto branchExtDto = _branchDal.GetExtById(id);
        if (branchExtDto is null)
            return new ErrorDataResult<BranchExtDto>(Messages.BranchNotFound);

        return new SuccessDataResult<BranchExtDto>(branchExtDto, Messages.BranchExtListedById);
    }

    public IDataResult<IEnumerable<BranchExtDto>> GetExtsByBusinessId(int businessId)
    {
        IEnumerable<BranchExtDto> branchExtDtos = _branchDal.GetExtsByBusinessId(businessId);
        if (!branchExtDtos.Any())
            return new ErrorDataResult<IEnumerable<BranchExtDto>>(Messages.BranchesNotFound);

        return new SuccessDataResult<IEnumerable<BranchExtDto>>(branchExtDtos, Messages.BranchExtsListedByBusinessId);
    }

    [TransactionScopeAspect]
    public IResult UpdateExt(BranchExtDto branchExtDto)
    {
        FullAddressDto fullAddressDto = new()
        {
            FullAddressId = branchExtDto.FullAddressId,
            CityId = branchExtDto.CityId,
            DistrictId = branchExtDto.DistrictId,
            AddressTitle = branchExtDto.AddressTitle,
            PostalCode = branchExtDto.PostalCode,
            AddressText = branchExtDto.AddressText,
        };
        var updateFullAddressResult = _fullAddressBl.Update(fullAddressDto);
        if (!updateFullAddressResult.Success)
            return updateFullAddressResult;

        BranchDto branchDto = new()
        {
            BranchId = branchExtDto.BranchId,
            BranchName = branchExtDto.BranchName,
        };
        var updateBranchResult = _branchBl.Update(branchDto);
        if (!updateBranchResult.Success)
            return updateBranchResult;

        return new SuccessResult(Messages.BranchExtUpdated);
    }
}
