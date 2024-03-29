﻿using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Concrete;

public class BranchBl : IBranchBl
{
    private readonly IBranchDal _branchDal;
    private readonly IMapper _mapper;

    public BranchBl(
        IBranchDal branchDal,
        IMapper mapper
    )
    {
        _branchDal = branchDal;
        _mapper = mapper;
    }

    public IDataResult<BranchDto> Add(BranchDto branchDto)
    {
        Branch searchedBranch = _branchDal.GetByBusinessIdAndBranchOrderOrBranchCode(branchDto.BusinessId, branchDto.BranchOrder, branchDto.BranchCode);
        if (searchedBranch is not null)
            return new ErrorDataResult<BranchDto>(Messages.BranchAlreadyExists);

        var addedBranch = _mapper.Map<Branch>(branchDto);

        addedBranch.CreatedAt = DateTimeOffset.Now;
        addedBranch.UpdatedAt = DateTimeOffset.Now;
        long id = _branchDal.Add(addedBranch);
        addedBranch.BranchId = id;

        var addedBranchDto = _mapper.Map<BranchDto>(addedBranch);

        return new SuccessDataResult<BranchDto>(addedBranchDto, Messages.BranchAdded);
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
        int branchOrder = 1;
        int maxBranchOrder = _branchDal.GetMaxBranchOrderByBusinessId(businessId);
        if (maxBranchOrder != 0)
            branchOrder = maxBranchOrder + 1;

        string branchCode;
        if (branchOrder < 10)
            branchCode = $"00000{branchOrder}";
        else if (branchOrder < 100)
            branchCode = $"0000{branchOrder}";
        else if (branchOrder < 1000)
            branchCode = $"000{branchOrder}";
        else if (branchOrder < 10000)
            branchCode = $"00{branchOrder}";
        else if (branchOrder < 100000)
            branchCode = $"0{branchOrder}";
        else
            branchCode = $"{branchOrder}";

        BranchCodeDto branchCodeDto = new()
        {
            BranchOrder = branchOrder,
            BranchCode = branchCode,
        };

        return new SuccessDataResult<BranchCodeDto>(branchCodeDto, Messages.BranchOrderAndCodeGenerated);
    }

    public IDataResult<List<BranchDto>> GetByBusinessId(int businessId)
    {
        List<Branch> branchs = _branchDal.GetByBusinessId(businessId);
        if (!branchs.Any())
            return new ErrorDataResult<List<BranchDto>>(Messages.BranchesNotFound);

        var branchDtos = _mapper.Map<List<BranchDto>>(branchs);

        return new SuccessDataResult<List<BranchDto>>(branchDtos, Messages.BranchsListedByBusinessId);
    }

    public IDataResult<BranchDto> GetById(long id)
    {
        Branch branch = _branchDal.GetById(id);
        if (branch is null)
            return new ErrorDataResult<BranchDto>(Messages.BranchNotFound);

        var branchDto = _mapper.Map<BranchDto>(branch);

        return new SuccessDataResult<BranchDto>(branchDto, Messages.BranchListedById);
    }

    public IDataResult<BranchExtDto> GetExtById(long id)
    {
        BranchExt branchExt = _branchDal.GetExtById(id);
        if (branchExt is null)
            return new ErrorDataResult<BranchExtDto>(Messages.BranchNotFound);

        var branchExtDto = _mapper.Map<BranchExtDto>(branchExt);

        return new SuccessDataResult<BranchExtDto>(branchExtDto, Messages.BranchExtListedById);
    }

    public IDataResult<List<BranchExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<BranchExt> branchExts = _branchDal.GetExtsByBusinessId(businessId);
        if (!branchExts.Any())
            return new ErrorDataResult<List<BranchExtDto>>(Messages.BranchesNotFound);

        var branchExtDtos = _mapper.Map<List<BranchExtDto>>(branchExts);

        return new SuccessDataResult<List<BranchExtDto>>(branchExtDtos, Messages.BranchExtsListedByBusinessId);
    }

    public IResult Update(BranchDto branchDto)
    {
        Branch branch = _branchDal.GetById(branchDto.BranchId);
        if (branch is null)
            return new ErrorDataResult<BranchDto>(Messages.BranchNotFound);

        branch.BranchName = branchDto.BranchName;
        branch.UpdatedAt = DateTimeOffset.Now;
        _branchDal.Update(branch);

        return new SuccessResult(Messages.BranchUpdated);
    }
}
