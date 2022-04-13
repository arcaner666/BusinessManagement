using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class ApartmentExtBl : IApartmentExtBl
{
    private readonly IApartmentBl _apartmentBl;
    private readonly IApartmentDal _apartmentDal;
    private readonly IFlatBl _flatBl;
    private readonly IKeyService _keyService;
    private readonly ISectionBl _sectionBl;

    public ApartmentExtBl(
        IApartmentBl apartmentBl,
        IApartmentDal apartmentDal,
        IFlatBl flatBl,
        IKeyService keyService,
        ISectionBl sectionBl
    )
    {
        _apartmentBl = apartmentBl;
        _apartmentDal = apartmentDal;
        _flatBl = flatBl;
        _keyService = keyService;
        _sectionBl = sectionBl;
    }

    [TransactionScopeAspect]
    public IResult AddExt(ApartmentExtDto apartmentExtDto)
    {
        // Arayüzden gelen id'ye sahip bir site var mı kontrol edilir.
        var getSectionResult = _sectionBl.GetById(apartmentExtDto.SectionId);
        if (!getSectionResult.Success)
            return getSectionResult;

        // Eşsiz bir apartman kodu üretebilmek için ilgili sitedeki tüm apartmanlar getirilir.
        List<Apartment> allApartments = _apartmentDal.GetBySectionId(apartmentExtDto.SectionId);

        // Apartman kodu üretilir.
        string apartmentCode = _keyService.GenerateApartmentCode(allApartments, getSectionResult.Data.SectionCode);

        // Apartman eklenir.
        ApartmentDto apartmentDto = new()
        {
            SectionId = apartmentExtDto.SectionId,
            BusinessId = apartmentExtDto.BusinessId,
            BranchId = apartmentExtDto.BranchId,
            ManagerId = apartmentExtDto.ManagerId,
            ApartmentName = apartmentExtDto.ApartmentName,
            ApartmentCode = apartmentCode,
            BlockNumber = apartmentExtDto.BlockNumber,
        };
        var addApartmentResult = _apartmentBl.Add(apartmentDto);
        if (!addApartmentResult.Success)
            return addApartmentResult;

        return new SuccessResult(Messages.ApartmentExtAdded);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(long id)
    {
        //// Apartmandaki daireler silinir.
        //var getFlatsResult = _flatBl.GetByApartmentId(apartmentDto.ApartmentId);
        //if (getFlatsResult == null)
        //{
        //    return getFlatsResult;
        //}

        //foreach (Flat flat in getFlatsResult)
        //{
        //    _flatBl.Delete(flat.FlatId);
        //}

        // Apartman silinir.
        var deleteSectionResult = _apartmentBl.Delete(id);
        if (!deleteSectionResult.Success)
            return deleteSectionResult;

        return new SuccessResult(Messages.ApartmentExtDeleted);
    }

    public IDataResult<ApartmentExtDto> GetExtById(long id)
    {
        Apartment searchedApartment = _apartmentDal.GetExtById(id);
        if (searchedApartment is null)
            return new ErrorDataResult<ApartmentExtDto>(Messages.ApartmentNotFound);

        ApartmentExtDto searchedApartmentExtDto = FillExtDto(searchedApartment);

        return new SuccessDataResult<ApartmentExtDto>(searchedApartmentExtDto, Messages.ApartmentExtListedById);
    }

    public IDataResult<List<ApartmentExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<Apartment> searchedApartments = _apartmentDal.GetExtsByBusinessId(businessId);
        if (searchedApartments.Count == 0)
            return new ErrorDataResult<List<ApartmentExtDto>>(Messages.ApartmentsNotFound);

        List<ApartmentExtDto> searchedApartmentExtDtos = FillExtDtos(searchedApartments);

        return new SuccessDataResult<List<ApartmentExtDto>>(searchedApartmentExtDtos, Messages.ApartmentExtsListedByBusinessId);
    }

    public IResult UpdateExt(ApartmentExtDto apartmentExtDto)
    {
        ApartmentDto apartmentDto = new()
        {
            ApartmentId = apartmentExtDto.ApartmentId,
            BranchId = apartmentExtDto.BranchId,
            ManagerId = apartmentExtDto.ManagerId,
            ApartmentName = apartmentExtDto.ApartmentName,
            BlockNumber = apartmentExtDto.BlockNumber,
        };
        var updateApartmentResult = _apartmentBl.Update(apartmentDto);
        if (!updateApartmentResult.Success)
            return updateApartmentResult;

        return new SuccessResult(Messages.ApartmentExtUpdated);
    }

    private ApartmentExtDto FillExtDto(Apartment apartment)
    {
        ApartmentExtDto apartmentExtDto = new()
        {
            ApartmentId = apartment.ApartmentId,
            SectionId = apartment.SectionId,
            BusinessId = apartment.BusinessId,
            BranchId = apartment.BranchId,
            ManagerId = apartment.ManagerId,
            ApartmentName = apartment.ApartmentName,
            ApartmentCode = apartment.ApartmentCode,
            BlockNumber = apartment.BlockNumber,
            CreatedAt = apartment.CreatedAt,
            UpdatedAt = apartment.UpdatedAt,

            SectionName = apartment.Section.SectionName,
            ManagerNameSurname = apartment.Manager.NameSurname,
        };

        return apartmentExtDto;
    }

    private List<ApartmentExtDto> FillExtDtos(List<Apartment> apartments)
    {
        List<ApartmentExtDto> apartmentExtDtos = apartments.Select(apartment => FillExtDto(apartment)).ToList();

        return apartmentExtDtos;
    }
}
