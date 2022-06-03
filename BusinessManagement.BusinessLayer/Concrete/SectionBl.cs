using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class SectionBl : ISectionBl
{
    private readonly IMapper _mapper;
    private readonly ISectionDal _sectionDal;

    public SectionBl(
        IMapper mapper,
        ISectionDal sectionDal
    )
    {
        _sectionDal = sectionDal;
        _mapper = mapper;
    }

    public IDataResult<SectionDto> Add(SectionDto sectionDto)
    {
        Section searchedSection = _sectionDal.GetBySectionCode(sectionDto.SectionCode);
        if (searchedSection is not null)
            return new ErrorDataResult<SectionDto>(Messages.SectionAlreadyExists);

        var addedSection = _mapper.Map<Section>(sectionDto);

        addedSection.CreatedAt = DateTimeOffset.Now;
        addedSection.UpdatedAt = DateTimeOffset.Now;
        int id = _sectionDal.Add(addedSection);
        addedSection.SectionId = id;

        var addedSectionDto = _mapper.Map<SectionDto>(addedSection);

        return new SuccessDataResult<SectionDto>(addedSectionDto, Messages.SectionAdded);
    }

    public IResult Delete(int id)
    {
        _sectionDal.Delete(id);

        return new SuccessResult(Messages.SectionDeleted);
    }

    public IDataResult<IEnumerable<SectionDto>> GetByBusinessId(int businessId)
    {
        IEnumerable<Section> sections = _sectionDal.GetByBusinessId(businessId);
        if (!sections.Any())
            return new ErrorDataResult<IEnumerable<SectionDto>>(Messages.SectionsNotFound);

        var sectionDtos = _mapper.Map<IEnumerable<SectionDto>>(sections);

        return new SuccessDataResult<IEnumerable<SectionDto>>(sectionDtos, Messages.SectionsListedByBusinessId);
    }

    public IDataResult<SectionDto> GetById(int id)
    {
        Section section = _sectionDal.GetById(id);
        if (section is null)
            return new ErrorDataResult<SectionDto>(Messages.SectionNotFound);

        var sectionDto = _mapper.Map<SectionDto>(section);

        return new SuccessDataResult<SectionDto>(sectionDto, Messages.SectionListedById);
    }

    public IResult Update(SectionDto sectionDto)
    {
        Section section = _sectionDal.GetById(sectionDto.SectionId);
        if (section is null)
            return new ErrorDataResult<SectionDto>(Messages.SectionNotFound);

        section.SectionId = sectionDto.SectionId;
        section.SectionGroupId = sectionDto.SectionGroupId;
        section.BranchId = sectionDto.BranchId;
        section.ManagerId = sectionDto.ManagerId;
        section.SectionName = sectionDto.SectionName;
        section.UpdatedAt = DateTimeOffset.Now;
        _sectionDal.Update(section);

        return new SuccessResult(Messages.SectionUpdated);
    }
}
