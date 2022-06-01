using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.CrossCuttingConcerns.Logging;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class SectionGroupBl : ISectionGroupBl
{
    private readonly ILoggerManager _logger;
    private readonly ISectionGroupDal _sectionGroupDal;

    public SectionGroupBl(
        ILoggerManager logger,
        ISectionGroupDal sectionGroupDal
    )
    {
        _logger = logger;
        _sectionGroupDal = sectionGroupDal;
    }

    public IDataResult<SectionGroupDto> Add(SectionGroupDto sectionGroupDto)
    {
        SectionGroupDto searchedSectionGroupDto = _sectionGroupDal.GetByBusinessIdAndSectionGroupName(sectionGroupDto.BusinessId, sectionGroupDto.SectionGroupName);
        if (searchedSectionGroupDto is not null)
            return new ErrorDataResult<SectionGroupDto>(Messages.SectionGroupAlreadyExists);

        sectionGroupDto.CreatedAt = DateTimeOffset.Now;
        sectionGroupDto.UpdatedAt = DateTimeOffset.Now;
        long id = _sectionGroupDal.Add(sectionGroupDto);
        sectionGroupDto.SectionGroupId = id;

        return new SuccessDataResult<SectionGroupDto>(sectionGroupDto, Messages.SectionGroupAdded);
    }

    public IResult Delete(long id)
    {
        var getSectionGroupResult = GetById(id);
        if (!getSectionGroupResult.Success)
            return getSectionGroupResult;

        _sectionGroupDal.Delete(id);

        return new SuccessResult(Messages.SectionGroupDeleted);
    }

    public IDataResult<List<SectionGroupDto>> GetByBusinessId(int businessId)
    {
        List<SectionGroupDto> sectionGroupDtos = _sectionGroupDal.GetByBusinessId(businessId);
        if (sectionGroupDtos.Count == 0)
            return new ErrorDataResult<List<SectionGroupDto>>(Messages.SectionGroupsNotFound);

        return new SuccessDataResult<List<SectionGroupDto>>(sectionGroupDtos, Messages.SectionGroupsListedByBusinessId);
    }

    public IDataResult<SectionGroupDto> GetById(long id)
    {
        SectionGroupDto sectionGroupDto = _sectionGroupDal.GetById(id);
        if (sectionGroupDto is null)
            return new ErrorDataResult<SectionGroupDto>(Messages.SectionGroupNotFound);

        return new SuccessDataResult<SectionGroupDto>(sectionGroupDto, Messages.SectionGroupListedById);
    }

    public IResult Update(SectionGroupDto sectionGroupDto)
    {
        var searchedSectionGroupResult = GetById(sectionGroupDto.SectionGroupId);
        if (!searchedSectionGroupResult.Success)
            return searchedSectionGroupResult;

        searchedSectionGroupResult.Data.BranchId = sectionGroupDto.BranchId;
        searchedSectionGroupResult.Data.SectionGroupName = sectionGroupDto.SectionGroupName;
        searchedSectionGroupResult.Data.UpdatedAt = DateTimeOffset.Now;
        _sectionGroupDal.Update(searchedSectionGroupResult.Data);

        return new SuccessResult(Messages.SectionGroupUpdated);

    }
}
