using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class BranchBl : IBranchBl
{
    private readonly IBranchDal _branchDal;

    public BranchBl(
        IBranchDal branchDal
    )
    {
        _branchDal = branchDal;
    }

    public IDataResult<BranchDto> Add(BranchDto branchDto)
    {
        BranchDto searchedBranchDto = _branchDal.GetByBusinessIdAndBranchOrderOrBranchCode(branchDto.BusinessId, branchDto.BranchOrder, branchDto.BranchCode);
        if (searchedBranchDto is not null)
            return new ErrorDataResult<BranchDto>(Messages.BranchAlreadyExists);

        branchDto.CreatedAt = DateTimeOffset.Now;
        branchDto.UpdatedAt = DateTimeOffset.Now;
        long id = _branchDal.Add(branchDto);
        branchDto.BranchId = id;

        return new SuccessDataResult<BranchDto>(branchDto, Messages.BranchAdded);
    }

    public IResult Delete(long id)
    {
        var getBranchResult = GetById(id);
        if (getBranchResult is null)
            return getBranchResult;

        _branchDal.Delete(id);

        return new SuccessResult(Messages.BranchDeleted);
    }

    public IDataResult<BranchCodeDto> GenerateBranchCode(int businessId)
    {
        BranchCodeDto branchCodeDto = new();

        branchCodeDto.BranchOrder = 1;
        BranchDto branchDto = _branchDal.GetByBusinessIdAndMaxBranchOrder(businessId);
        if (branchDto == null)
        {
            return new ErrorDataResult<BranchCodeDto>(Messages.BranchNotFound);
        }
        branchCodeDto.BranchOrder = branchDto.BranchOrder + 1;

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
        List<BranchDto> branchDtos = _branchDal.GetByBusinessId(businessId);
        if (branchDtos.Count == 0)
            return new ErrorDataResult<List<BranchDto>>(Messages.BranchesNotFound);

        return new SuccessDataResult<List<BranchDto>>(branchDtos, Messages.BranchsListedByBusinessId);
    }

    public IDataResult<BranchDto> GetById(long id)
    {
        BranchDto branchDto = _branchDal.GetById(id);
        if (branchDto is null)
            return new ErrorDataResult<BranchDto>(Messages.BranchNotFound);

        return new SuccessDataResult<BranchDto>(branchDto, Messages.BranchListedById);
    }

    public IResult Update(BranchDto branchDto)
    {
        var searchedBranchResult = GetById(branchDto.BranchId);
        if (!searchedBranchResult.Success)
            return searchedBranchResult;

        searchedBranchResult.Data.BranchName = branchDto.BranchName;
        searchedBranchResult.Data.UpdatedAt = DateTimeOffset.Now;
        _branchDal.Update(searchedBranchResult.Data);

        return new SuccessResult(Messages.BranchUpdated);
    }
}
