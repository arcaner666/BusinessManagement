using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Concrete;

public class SectionAdvBl : ISectionAdvBl
{
    private readonly IApartmentAdvBl _apartmentAdvBl;
    private readonly IApartmentBl _apartmentBl;
    private readonly IFlatBl _flatBl;
    private readonly IFullAddressBl _fullAddressBl;
    private readonly IKeyService _keyService;
    private readonly IMapper _mapper;
    private readonly ISectionBl _sectionBl;
    private readonly ISectionDal _sectionDal;

    public SectionAdvBl(
        IApartmentAdvBl apartmentAdvBl,
        IApartmentBl apartmentBl,
        IFlatBl flatBl,
        IFullAddressBl fullAddressBl,
        IKeyService keyService,
        IMapper mapper,
        ISectionBl sectionBl,
        ISectionDal sectionDal
    )
    {
        _apartmentAdvBl = apartmentAdvBl;
        _apartmentBl = apartmentBl;
        _fullAddressBl = fullAddressBl;
        _flatBl = flatBl;
        _keyService = keyService;
        _mapper = mapper;
        _sectionBl = sectionBl;
        _sectionDal = sectionDal;
    }

    [TransactionScopeAspect]
    public IResult Add(SectionExtDto sectionExtDto)
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
        List<Section> sections = _sectionDal.GetAll();

        var sectionDtos = _mapper.Map<List<SectionDto>>(sections);

        // Site kodu üretilir.
        string sectionCode = _keyService.GenerateSectionCode(sectionDtos);

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
    public IResult Delete(int id)
    {
        // Site getirilir. FullAddressId'ye ulaşmak için gereklidir.
        var getSectionResult = _sectionBl.GetById(id);
        if (!getSectionResult.Success)
            return getSectionResult;

        // Sitedeki apartmanlar getirilir.
        var getApartmentsResult = _apartmentBl.GetBySectionId(id);
        if (getApartmentsResult is null)
            return getApartmentsResult;

        // Sitedeki apartmanlar silinir.
        foreach (ApartmentDto apartmentDto in getApartmentsResult.Data)
        {
            _apartmentAdvBl.Delete(apartmentDto.ApartmentId);
        }

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

    [TransactionScopeAspect]
    public IResult Update(SectionExtDto sectionExtDto)
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
}
