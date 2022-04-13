using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class SectionExtBl : ISectionExtBl
{
    private readonly IFullAddressBl _fullAddressBl;
    private readonly IKeyService _keyService;
    private readonly ISectionBl _sectionBl;
    private readonly ISectionDal _sectionDal;

    public SectionExtBl(
        IFullAddressBl fullAddressBl,
        IKeyService keyService,
        ISectionBl sectionBl,
        ISectionDal sectionDal
    )
    {
        _fullAddressBl = fullAddressBl;
        _keyService = keyService;
        _sectionBl = sectionBl;
        _sectionDal = sectionDal;
    }

    [TransactionScopeAspect]
    public IResult AddExt(SectionExtDto sectionExtDto)
    {
        // Sitenin adresi eklenir.
        FullAddressDto fullAddressDto = new()
        {
            CityId = sectionExtDto.CityId,
            DistrictId = sectionExtDto.DistrictId,
            AddressTitle = "Site Adresi",
            PostalCode = sectionExtDto.PostalCode,
            AddressText = sectionExtDto.AddressText,
        };
        var addFullAddressResult = _fullAddressBl.Add(fullAddressDto);
        if (!addFullAddressResult.Success)
            return addFullAddressResult;

        // Eşsiz bir site kodu oluşturmak için tüm siteler getirilir.
        List<Section> allSections = _sectionDal.GetAll();

        // Site kodu üretilir.
        string sectionCode = _keyService.GenerateSectionCode(allSections);

        // Site eklenir.
        SectionDto sectionDto = new()
        {
            SectionGroupId = sectionExtDto.SectionGroupId,
            BusinessId = sectionExtDto.BusinessId,
            BranchId = sectionExtDto.BranchId,
            ManagerId = sectionExtDto.ManagerId,
            FullAddressId = addFullAddressResult.Data.FullAddressId,
            SectionName = sectionExtDto.SectionName,
            SectionCode = sectionCode,
        };
        var addSectionResult = _sectionBl.Add(sectionDto);
        if (!addSectionResult.Success)
            return addSectionResult;

        return new SuccessResult(Messages.SectionExtAdded);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(int id)
    {
        // Site getirilir. FullAddressId'ye ulaşmak için gereklidir.
        var getSectionResult = _sectionBl.GetById(id);
        if (!getSectionResult.Success)
            return getSectionResult;

        // Site silinir.
        var deleteSectionResult = _sectionBl.Delete(getSectionResult.Data.SectionId);
        if (!deleteSectionResult.Success)
            return deleteSectionResult;

        // Şubenin adresi silinir.
        var deleteFullAddressResult = _fullAddressBl.Delete(getSectionResult.Data.FullAddressId);
        if (!deleteFullAddressResult.Success)
            return deleteFullAddressResult;

        return new SuccessResult(Messages.SectionExtDeleted);
    }

    public IDataResult<SectionExtDto> GetExtById(int id)
    {
        Section searchedSection = _sectionDal.GetExtById(id);
        if (searchedSection is null)
            return new ErrorDataResult<SectionExtDto>(Messages.SectionNotFound);

        SectionExtDto searchedSectionExtDto = FillExtDto(searchedSection);

        return new SuccessDataResult<SectionExtDto>(searchedSectionExtDto, Messages.SectionExtListedById);
    }

    public IDataResult<List<SectionExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<Section> searchedSections = _sectionDal.GetExtsByBusinessId(businessId);
        if (searchedSections.Count == 0)
            return new ErrorDataResult<List<SectionExtDto>>(Messages.SectionsNotFound);

        List<SectionExtDto> searchedSectionExtDtos = FillExtDtos(searchedSections);

        return new SuccessDataResult<List<SectionExtDto>>(searchedSectionExtDtos, Messages.SectionExtsListedByBusinessId);
    }

    [TransactionScopeAspect]
    public IResult UpdateExt(SectionExtDto sectionExtDto)
    {
        // Site adresi güncellenir.
        FullAddressDto fullAddressDto = new()
        {
            FullAddressId = sectionExtDto.FullAddressId,
            CityId = sectionExtDto.CityId,
            AddressTitle = "Site Adresi",
            DistrictId = sectionExtDto.DistrictId,
            PostalCode = sectionExtDto.PostalCode,
            AddressText = sectionExtDto.AddressText,
        };
        var updateFullAddressResult = _fullAddressBl.Update(fullAddressDto);
        if (!updateFullAddressResult.Success)
            return updateFullAddressResult;

        // Site güncellenir.
        SectionDto sectionDto = new()
        {
            SectionId = sectionExtDto.SectionId,
            SectionGroupId = sectionExtDto.SectionGroupId,
            BranchId = sectionExtDto.BranchId,
            ManagerId = sectionExtDto.ManagerId,
            SectionName = sectionExtDto.SectionName,
        };
        var updateSectionResult = _sectionBl.Update(sectionDto);
        if (!updateSectionResult.Success)
            return updateSectionResult;

        return new SuccessResult(Messages.SectionExtUpdated);
    }

    private SectionExtDto FillExtDto(Section section)
    {
        SectionExtDto sectionExtDto = new()
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

            SectionGroupName = section.SectionGroup.SectionGroupName,
            ManagerNameSurname = section.Manager.NameSurname,
            CityId = section.FullAddress.CityId,
            DistrictId = section.FullAddress.DistrictId,
            AddressTitle = section.FullAddress.AddressTitle,
            PostalCode = section.FullAddress.PostalCode,
            AddressText = section.FullAddress.AddressText,
            CityName = section.FullAddress.City.CityName,
            DistrictName = section.FullAddress.District.DistrictName,
        };

        return sectionExtDto;
    }

    private List<SectionExtDto> FillExtDtos(List<Section> sections)
    {
        List<SectionExtDto> sectionExtDtos = sections.Select(section => FillExtDto(section)).ToList();

        return sectionExtDtos;
    }
}
