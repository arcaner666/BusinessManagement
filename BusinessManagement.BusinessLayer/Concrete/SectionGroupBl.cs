using AutoMapper;
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
    private readonly IMapper _mapper;
    private readonly ISectionGroupDal _sectionGroupDal;

    public SectionGroupBl(
        ILoggerManager logger,
        IMapper mapper,
        ISectionGroupDal sectionGroupDal
    )
    {
        _logger = logger;
        _mapper = mapper;
        _sectionGroupDal = sectionGroupDal;
    }

    public IDataResult<SectionGroupDto> Add(SectionGroupDto sectionGroupDto)
    {
        SectionGroup searchedSectionGroup = _sectionGroupDal.GetByBusinessIdAndSectionGroupName(sectionGroupDto.BusinessId, sectionGroupDto.SectionGroupName);
        if (searchedSectionGroup is not null)
            return new ErrorDataResult<SectionGroupDto>(Messages.SectionGroupAlreadyExists);

        var addedSectionGroup = _mapper.Map<SectionGroup>(sectionGroupDto);

        addedSectionGroup.CreatedAt = DateTimeOffset.Now;
        addedSectionGroup.UpdatedAt = DateTimeOffset.Now;
        long id = _sectionGroupDal.Add(addedSectionGroup);
        addedSectionGroup.SectionGroupId = id;

        var addedSectionGroupDto = _mapper.Map<SectionGroupDto>(addedSectionGroup);

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
        List<SectionGroup> sectionGroups = _sectionGroupDal.GetByBusinessId(businessId);
        if (!sectionGroups.Any())
            return new ErrorDataResult<List<SectionGroupDto>>(Messages.SectionGroupsNotFound);

        var sectionGroupDtos = _mapper.Map<List<SectionGroupDto>>(sectionGroups);

        return new SuccessDataResult<List<SectionGroupDto>>(sectionGroupDtos, Messages.SectionGroupsListedByBusinessId);
    }

    public IDataResult<SectionGroupDto> GetById(long id)
    {
        SectionGroup sectionGroup = _sectionGroupDal.GetById(id);
        if (sectionGroup is null)
            return new ErrorDataResult<SectionGroupDto>(Messages.SectionGroupNotFound);

        var sectionGroupDto = _mapper.Map<SectionGroupDto>(sectionGroup);

        return new SuccessDataResult<SectionGroupDto>(sectionGroupDto, Messages.SectionGroupListedById);
    }

    public IResult Update(SectionGroupDto sectionGroupDto)
    {
        SectionGroup sectionGroup = _sectionGroupDal.GetById(sectionGroupDto.SectionGroupId);
        if (sectionGroup is null)
            return new ErrorDataResult<SectionGroupDto>(Messages.SectionGroupNotFound);

        sectionGroup.BranchId = sectionGroupDto.BranchId;
        sectionGroup.SectionGroupName = sectionGroupDto.SectionGroupName;
        sectionGroup.UpdatedAt = DateTimeOffset.Now;
        _sectionGroupDal.Update(sectionGroup);

        return new SuccessResult(Messages.SectionGroupUpdated);

    }
}
