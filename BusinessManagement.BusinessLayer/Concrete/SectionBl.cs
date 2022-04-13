using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class SectionBl : ISectionBl
{
    private readonly ISectionDal _sectionDal;

    public SectionBl(
        ISectionDal sectionDal
    )
    {
        _sectionDal = sectionDal;
    }

    public IDataResult<SectionDto> Add(SectionDto sectionDto)
    {
        Section searchedSection = _sectionDal.GetBySectionCode(sectionDto.SectionCode);
        if (searchedSection is not null)
            return new ErrorDataResult<SectionDto>(Messages.SectionAlreadyExists);

        Section addedSection = new()
        {
            SectionGroupId = sectionDto.SectionGroupId,
            BusinessId = sectionDto.BusinessId,
            BranchId = sectionDto.BranchId,
            ManagerId = sectionDto.ManagerId,
            FullAddressId = sectionDto.FullAddressId,
            SectionName = sectionDto.SectionName,
            SectionCode = sectionDto.SectionCode,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _sectionDal.Add(addedSection);

        SectionDto addedSectionDto = FillDto(addedSection);

        return new SuccessDataResult<SectionDto>(addedSectionDto, Messages.SectionAdded);
    }

    public IResult Delete(int id)
    {
        _sectionDal.Delete(id);

        return new SuccessResult(Messages.SectionDeleted);
    }

    public IDataResult<List<SectionDto>> GetByBusinessId(int businessId)
    {
        List<Section> searchedSections = _sectionDal.GetByBusinessId(businessId);
        if (searchedSections.Count == 0)
            return new ErrorDataResult<List<SectionDto>>(Messages.SectionsNotFound);

        List<SectionDto> searchedSectionDtos = FillDtos(searchedSections);

        return new SuccessDataResult<List<SectionDto>>(searchedSectionDtos, Messages.SectionsListedByBusinessId);
    }

    public IDataResult<SectionDto> GetById(int id)
    {
        Section searchedSection = _sectionDal.GetById(id);
        if (searchedSection is null)
            return new ErrorDataResult<SectionDto>(Messages.SectionNotFound);

        SectionDto searchedSectionDto = FillDto(searchedSection);

        return new SuccessDataResult<SectionDto>(searchedSectionDto, Messages.SectionListedById);
    }

    public IResult Update(SectionDto sectionDto)
    {
        Section searchedSection = _sectionDal.GetById(sectionDto.SectionId);
        if (searchedSection is null)
            return new ErrorDataResult<SectionDto>(Messages.SectionNotFound);

        searchedSection.SectionId = sectionDto.SectionId;
        searchedSection.SectionGroupId = sectionDto.SectionGroupId;
        searchedSection.BranchId = sectionDto.BranchId;
        searchedSection.ManagerId = sectionDto.ManagerId;
        searchedSection.SectionName = sectionDto.SectionName;
        searchedSection.UpdatedAt = DateTimeOffset.Now;
        _sectionDal.Update(searchedSection);

        return new SuccessResult(Messages.SectionUpdated);
    }

    private SectionDto FillDto(Section section)
    {
        SectionDto sectionDto = new()
        {
            SectionId = section.SectionId,
            SectionGroupId = section.SectionGroupId,
            BusinessId = section.BusinessId,
            BranchId = section.BranchId,
            ManagerId = section.ManagerId,
            FullAddressId = section.FullAddressId,
            SectionName = section.SectionName,
            SectionCode = section.SectionCode,
            CreatedAt = section.CreatedAt,
            UpdatedAt = section.UpdatedAt,
        };

        return sectionDto;
    }

    private List<SectionDto> FillDtos(List<Section> sections)
    {
        List<SectionDto> sectionDtos = sections.Select(section => FillDto(section)).ToList();

        return sectionDtos;
    }
}
