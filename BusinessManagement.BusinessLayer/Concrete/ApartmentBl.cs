using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class ApartmentBl : IApartmentBl
{
    private readonly IApartmentDal _apartmentDal;
    private readonly IKeyService _keyService;
    private readonly ISectionBl _sectionBl;

    public ApartmentBl(
        IApartmentDal apartmentDal,
        IKeyService keyService,
        ISectionBl sectionBl
    )
    {
        _apartmentDal = apartmentDal;
        _keyService = keyService;
        _sectionBl = sectionBl;
    }

    public IDataResult<ApartmentDto> Add(ApartmentDto apartmentDto)
    {
        Apartment searchedApartment = _apartmentDal.GetByApartmentCode(apartmentDto.ApartmentCode);
        if (searchedApartment is not null)
            return new ErrorDataResult<ApartmentDto>(Messages.ApartmentAlreadyExists);

        Apartment addedApartment = new()
        {
            SectionId = apartmentDto.SectionId,
            BusinessId = apartmentDto.BusinessId,
            BranchId = apartmentDto.BranchId,
            ManagerId = apartmentDto.ManagerId,
            ApartmentName = apartmentDto.ApartmentName,
            ApartmentCode = apartmentDto.ApartmentCode,
            BlockNumber = apartmentDto.BlockNumber,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _apartmentDal.Add(addedApartment);

        ApartmentDto addedApartmentDto = FillDto(addedApartment);

        return new SuccessDataResult<ApartmentDto>(addedApartmentDto, Messages.ApartmentAdded);
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
        var addApartmentResult = Add(apartmentDto);
        if (!addApartmentResult.Success)
            return addApartmentResult;

        return new SuccessResult(Messages.ApartmentExtAdded);
    }

    public IResult Delete(long id)
    {
        var getApartmentResult = GetById(id);
        if (getApartmentResult is null)
            return getApartmentResult;

        _apartmentDal.Delete(id);

        return new SuccessResult(Messages.ApartmentDeleted);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(long id)
    {
        //// Apartmandaki daireler silinir.
        //List<Flat> getFlatsResult = _flatDal.GetByApartmentId(apartmentDto.ApartmentId);
        //if (getFlatsResult == null)
        //{
        //    return BadRequest(new ErrorResult(Messages.FlatsNotFound));
        //}

        //foreach (Flat flat in getFlatsResult)
        //{
        //    _flatDal.Delete(flat.FlatId);
        //}

        // Apartman silinir.
        var deleteSectionResult = Delete(id);
        if (!deleteSectionResult.Success)
            return deleteSectionResult;

        return new SuccessResult(Messages.ApartmentExtDeleted);
    }

    public IDataResult<List<ApartmentDto>> GetByBusinessId(int businessId)
    {
        List<Apartment> searchedApartments = _apartmentDal.GetByBusinessId(businessId);
        if (searchedApartments.Count == 0)
            return new ErrorDataResult<List<ApartmentDto>>(Messages.ApartmentsNotFound);

        List<ApartmentDto> searchedApartmentDtos = FillDtos(searchedApartments);

        return new SuccessDataResult<List<ApartmentDto>>(searchedApartmentDtos, Messages.ApartmentsListedByBusinessId);
    }

    public IDataResult<ApartmentDto> GetById(long id)
    {
        Apartment searchedApartment = _apartmentDal.GetById(id);
        if (searchedApartment is null)
            return new ErrorDataResult<ApartmentDto>(Messages.ApartmentNotFound);

        ApartmentDto searchedApartmentDto = FillDto(searchedApartment);

        return new SuccessDataResult<ApartmentDto>(searchedApartmentDto, Messages.ApartmentListedById);
    }

    public IDataResult<List<ApartmentDto>> GetBySectionId(int sectionId)
    {
        List<Apartment> searchedApartments = _apartmentDal.GetBySectionId(sectionId);
        if (searchedApartments.Count == 0)
            return new ErrorDataResult<List<ApartmentDto>>(Messages.ApartmentsNotFound);

        List<ApartmentDto> searchedApartmentDtos = FillDtos(searchedApartments);

        return new SuccessDataResult<List<ApartmentDto>>(searchedApartmentDtos, Messages.ApartmentsListedBySectionId);
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

    public IResult Update(ApartmentDto apartmentDto)
    {
        Apartment searchedApartment = _apartmentDal.GetById(apartmentDto.ApartmentId);
        if (searchedApartment is null)
            return new ErrorDataResult<ApartmentDto>(Messages.ApartmentNotFound);

        searchedApartment.BranchId = apartmentDto.BranchId;
        searchedApartment.ManagerId = apartmentDto.ManagerId;
        searchedApartment.ApartmentName = apartmentDto.ApartmentName;
        searchedApartment.BlockNumber = apartmentDto.BlockNumber;
        searchedApartment.UpdatedAt = DateTimeOffset.Now;
        _apartmentDal.Update(searchedApartment);

        return new SuccessResult(Messages.ApartmentUpdated);
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
        var updateApartmentResult = Update(apartmentDto);
        if (!updateApartmentResult.Success)
            return updateApartmentResult;

        return new SuccessResult(Messages.ApartmentExtUpdated);
    }

    private ApartmentDto FillDto(Apartment apartment)
    {
        ApartmentDto apartmentDto = new()
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
        };

        return apartmentDto;
    }

    private List<ApartmentDto> FillDtos(List<Apartment> apartments)
    {
        List<ApartmentDto> apartmentDtos = apartments.Select(apartment => FillDto(apartment)).ToList();

        return apartmentDtos;
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
