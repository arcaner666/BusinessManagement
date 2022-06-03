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
        SectionDto searchedSectionDto = _sectionDal.GetBySectionCode(sectionDto.SectionCode);
        if (searchedSectionDto is not null)
            return new ErrorDataResult<SectionDto>(Messages.SectionAlreadyExists);

        sectionDto.CreatedAt = DateTimeOffset.Now;
        sectionDto.UpdatedAt = DateTimeOffset.Now;
        int id = _sectionDal.Add(sectionDto);
        sectionDto.SectionId = id;

        return new SuccessDataResult<SectionDto>(sectionDto, Messages.SectionAdded);
    }

    public IResult Delete(int id)
    {
        _sectionDal.Delete(id);

        return new SuccessResult(Messages.SectionDeleted);
    }

    public IDataResult<IEnumerable<SectionDto>> GetByBusinessId(int businessId)
    {
        IEnumerable<SectionDto> sectionDtos = _sectionDal.GetByBusinessId(businessId);
        if (!sectionDtos.Any())
            return new ErrorDataResult<IEnumerable<SectionDto>>(Messages.SectionsNotFound);

        return new SuccessDataResult<IEnumerable<SectionDto>>(sectionDtos, Messages.SectionsListedByBusinessId);
    }

    public IDataResult<SectionDto> GetById(int id)
    {
        SectionDto sectionDto = _sectionDal.GetById(id);
        if (sectionDto is null)
            return new ErrorDataResult<SectionDto>(Messages.SectionNotFound);

        return new SuccessDataResult<SectionDto>(sectionDto, Messages.SectionListedById);
    }

    public IResult Update(SectionDto sectionDto)
    {
        var searchedSectionResult = GetById(sectionDto.SectionId);
        if (!searchedSectionResult.Success)
            return searchedSectionResult;

        searchedSectionResult.Data.SectionId = sectionDto.SectionId;
        searchedSectionResult.Data.SectionGroupId = sectionDto.SectionGroupId;
        searchedSectionResult.Data.BranchId = sectionDto.BranchId;
        searchedSectionResult.Data.ManagerId = sectionDto.ManagerId;
        searchedSectionResult.Data.SectionName = sectionDto.SectionName;
        searchedSectionResult.Data.UpdatedAt = DateTimeOffset.Now;
        _sectionDal.Update(searchedSectionResult.Data);

        return new SuccessResult(Messages.SectionUpdated);
    }
}
