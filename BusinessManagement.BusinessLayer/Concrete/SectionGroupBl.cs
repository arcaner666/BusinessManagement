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
        SectionGroup searchedSectionGroup = _sectionGroupDal.GetByBusinessIdAndSectionGroupName(sectionGroupDto.BusinessId, sectionGroupDto.SectionGroupName);
        if (searchedSectionGroup is not null)
            return new ErrorDataResult<SectionGroupDto>(Messages.SectionGroupAlreadyExists);

        SectionGroup addedSectionGroup = new()
        {
            BusinessId = sectionGroupDto.BusinessId,
            BranchId = sectionGroupDto.BranchId,
            SectionGroupName = sectionGroupDto.SectionGroupName,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _sectionGroupDal.Add(addedSectionGroup);

        SectionGroupDto addedSectionGroupDto = FillDto(addedSectionGroup);

        return new SuccessDataResult<SectionGroupDto>(addedSectionGroupDto, Messages.SectionGroupAdded);
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
        List<SectionGroup> searchedSectionGroups = _sectionGroupDal.GetByBusinessId(businessId);
        if (searchedSectionGroups.Count == 0)
            return new ErrorDataResult<List<SectionGroupDto>>(Messages.SectionGroupsNotFound);

        List<SectionGroupDto> searchedSectionGroupDtos = FillDtos(searchedSectionGroups);

        return new SuccessDataResult<List<SectionGroupDto>>(searchedSectionGroupDtos, Messages.SectionGroupsListedByBusinessId);
    }

    public IDataResult<SectionGroupDto> GetById(long id)
    {
        SectionGroup searchedSectionGroup = _sectionGroupDal.GetById(id);
        if (searchedSectionGroup is null)
            return new ErrorDataResult<SectionGroupDto>(Messages.SectionGroupNotFound);

        SectionGroupDto searchedSectionGroupDto = FillDto(searchedSectionGroup);

        return new SuccessDataResult<SectionGroupDto>(searchedSectionGroupDto, Messages.SectionGroupListedById);
    }

    public IResult Update(SectionGroupDto sectionGroupDto)
    {
        SectionGroup searchedSectionGroup = _sectionGroupDal.GetById(sectionGroupDto.SectionGroupId);
        if (searchedSectionGroup is null)
            return new ErrorDataResult<SectionDto>(Messages.SectionGroupNotFound);

        searchedSectionGroup.BranchId = sectionGroupDto.BranchId;
        searchedSectionGroup.SectionGroupName = sectionGroupDto.SectionGroupName;
        searchedSectionGroup.UpdatedAt = DateTimeOffset.Now;
        _sectionGroupDal.Update(searchedSectionGroup);

        return new SuccessResult(Messages.SectionGroupUpdated);

    }

    private SectionGroupDto FillDto(SectionGroup sectionGroup)
    {
        SectionGroupDto sectionGroupDto = new()
        {
            SectionGroupId = sectionGroup.SectionGroupId,
            BusinessId = sectionGroup.BusinessId,
            BranchId = sectionGroup.BranchId,
            SectionGroupName = sectionGroup.SectionGroupName,
            CreatedAt = sectionGroup.CreatedAt,
            UpdatedAt = sectionGroup.UpdatedAt,
        };

        return sectionGroupDto;
    }

    private List<SectionGroupDto> FillDtos(List<SectionGroup> sectionGroups)
    {
        List<SectionGroupDto> sectionGroupDtos = sectionGroups.Select(sectionGroup => FillDto(sectionGroup)).ToList();

        return sectionGroupDtos;
    }
}
