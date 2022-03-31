using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class BranchBl : IBranchBl
{
    private readonly IBranchDal _branchDal;
    private readonly IFullAddressBl _fullAddressBl;

    public BranchBl(
        IBranchDal branchDal,
        IFullAddressBl fullAddressBl
    )
    {
        _branchDal = branchDal;
        _fullAddressBl = fullAddressBl;
    }

    public IDataResult<BranchDto> Add(BranchDto branchDto)
    {
        Branch getBranch = _branchDal.GetByBusinessIdAndBranchOrderOrBranchCode(branchDto.BusinessId, branchDto.BranchOrder, branchDto.BranchCode);
        if (getBranch != null)
            return new ErrorDataResult<BranchDto>(Messages.BranchAlreadyExists);

        Branch addBranch = new()
        {
            BusinessId = branchDto.BusinessId,
            FullAddressId = branchDto.FullAddressId,
            BranchOrder = branchDto.BranchOrder,
            BranchName = branchDto.BranchName,
            BranchCode = branchDto.BranchCode,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _branchDal.Add(addBranch);

        BranchDto addBranchDto = FillDto(addBranch);

        return new SuccessDataResult<BranchDto>(addBranchDto, Messages.BranchAdded);
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
        var addBranchResult = Add(branchDto);
        if (!addBranchResult.Success)
            return addBranchResult;

        return new SuccessResult(Messages.BranchExtAdded);
    }

    public IResult Delete(long id)
    {
        _branchDal.Delete(id);

        return new SuccessResult(Messages.BranchDeleted);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(long id)
    {
        // Şube getirilir.
        var getBranchResult = GetById(id);
        if (!getBranchResult.Success)
            return getBranchResult;

        // Merkez şubenin silinmesi engellenir.
        if (getBranchResult.Data.BranchName == "Merkez")
            return new ErrorResult(Messages.BranchCanNotDeleteMainBranch);

        // Şube silinir.
        var deleteBranchResult = Delete(id);
        if (!deleteBranchResult.Success)
            return deleteBranchResult;

        // Şubenin adresi silinir.
        var deleteFullAddressResult = _fullAddressBl.Delete(id);
        if (!deleteFullAddressResult.Success)
            return deleteFullAddressResult;

        return new SuccessResult(Messages.BranchExtDeleted);
    }

    public IDataResult<BranchCodeDto> GenerateBranchCode(int businessId)
    {
        BranchCodeDto branchCodeDto = new();

        branchCodeDto.BranchOrder = 1;
        Branch getBranchResult = _branchDal.GetByBusinessIdAndMaxBranchOrder(businessId);
        if (getBranchResult == null)
        {
            return new ErrorDataResult<BranchCodeDto>(Messages.BranchNotFound);
        }
        branchCodeDto.BranchOrder = getBranchResult.BranchOrder + 1;

        if (branchCodeDto.BranchOrder < 10)
            branchCodeDto.BranchCode = $"00000{branchCodeDto.BranchOrder}";
        else if (branchCodeDto.BranchOrder < 100)
            branchCodeDto.BranchCode = $"0000{branchCodeDto.BranchOrder}";
        else if (branchCodeDto.BranchOrder < 1000)
            branchCodeDto.BranchCode = $"000{branchCodeDto.BranchOrder}";
        else if (branchCodeDto.BranchOrder < 10000)
            branchCodeDto.BranchCode = $"00{branchCodeDto.BranchOrder}";
        else if (branchCodeDto.BranchOrder < 100000)
            branchCodeDto.BranchCode = $"0{branchCodeDto.BranchOrder}";
        else
            branchCodeDto.BranchCode = $"{branchCodeDto.BranchOrder}";

        return new SuccessDataResult<BranchCodeDto>(branchCodeDto, Messages.BranchOrderAndCodeGenerated);
    }

    public IDataResult<List<BranchDto>> GetByBusinessId(int businessId)
    {
        List<Branch> branches = _branchDal.GetByBusinessId(businessId);
        if (branches.Count == 0)
            return new ErrorDataResult<List<BranchDto>>(Messages.BranchesNotFound);

        List<BranchDto> branchDtos = FillDtos(branches);

        return new SuccessDataResult<List<BranchDto>>(branchDtos, Messages.BranchsListedByAccountId);
    }

    public IDataResult<BranchDto> GetById(long id)
    {
        Branch getBranch = _branchDal.GetById(id);
        if (getBranch == null)
            return new ErrorDataResult<BranchDto>(Messages.BranchNotFound);

        BranchDto getBranchDto = FillDto(getBranch);

        return new SuccessDataResult<BranchDto>(getBranchDto, Messages.BranchListedById);
    }

    public IDataResult<BranchExtDto> GetExtById(long id)
    {
        Branch getBranch = _branchDal.GetExtById(id);
        if (getBranch == null)
            return new ErrorDataResult<BranchExtDto>(Messages.BranchNotFound);

        BranchExtDto getBranchExtDto = FillExtDto(getBranch);

        return new SuccessDataResult<BranchExtDto>(getBranchExtDto, Messages.BranchExtListedById);
    }

    public IResult Update(BranchDto branchDto)
    {
        Branch getBranch = _branchDal.GetById(branchDto.BranchId);
        if (getBranch == null)
            return new ErrorDataResult<BranchDto>(Messages.BranchNotFound);

        getBranch.BranchName = branchDto.BranchName;
        getBranch.UpdatedAt = DateTimeOffset.Now;
        _branchDal.Update(getBranch);

        return new SuccessResult(Messages.BranchUpdated);
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
        var updateBranchResult = Update(branchDto);
        if (!updateBranchResult.Success)
            return updateBranchResult;

        return new SuccessResult(Messages.BranchExtUpdated);
    }

    private BranchDto FillDto(Branch branch)
    {
        BranchDto branchDto = new()
        {
            BranchId = branch.BranchId,
            BusinessId = branch.BusinessId,
            FullAddressId = branch.FullAddressId,
            BranchOrder = branch.BranchOrder,
            BranchName = branch.BranchName,
            BranchCode = branch.BranchCode,
            CreatedAt = branch.CreatedAt,
            UpdatedAt = branch.UpdatedAt,
        };

        return branchDto;
    }

    private List<BranchDto> FillDtos(List<Branch> branchs)
    {
        List<BranchDto> branchDtos = branchs.Select(branch => FillDto(branch)).ToList();

        return branchDtos;
    }

    private BranchExtDto FillExtDto(Branch branch)
    {
        BranchExtDto branchExtDto = new()
        {
            BranchId = branch.BranchId,
            BusinessId = branch.BusinessId,
            FullAddressId = branch.FullAddressId,
            BranchOrder = branch.BranchOrder,
            BranchName = branch.BranchName,
            BranchCode = branch.BranchCode,
            CreatedAt = branch.CreatedAt,
            UpdatedAt = branch.UpdatedAt,

            CityId = branch.FullAddress.CityId,
            DistrictId = branch.FullAddress.DistrictId,
            AddressTitle = branch.FullAddress.AddressTitle,
            PostalCode = branch.FullAddress.PostalCode,
            AddressText = branch.FullAddress.AddressText,
        };

        return branchExtDto;
    }

    private List<BranchExtDto> FillExtDtos(List<Branch> branches)
    {
        List<BranchExtDto> branchExtDtos = branches.Select(branch => FillExtDto(branch)).ToList();

        return branchExtDtos;
    }
}
