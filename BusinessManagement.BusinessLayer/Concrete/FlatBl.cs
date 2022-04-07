using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class FlatBl : IFlatBl
{
    private readonly IApartmentBl _apartmentBl;
    private readonly IFlatDal _flatDal;
    private readonly IKeyService _keyService;

    public FlatBl(
        IApartmentBl apartmentBl,
        IFlatDal flatDal,
        IKeyService keyService
    )
    {
        _apartmentBl = apartmentBl;
        _flatDal = flatDal;
        _keyService = keyService;
    }

    public IDataResult<FlatDto> Add(FlatDto flatDto)
    {
        Flat searchedFlat = _flatDal.GetByFlatCode(flatDto.FlatCode);
        if (searchedFlat is not null)
            return new ErrorDataResult<FlatDto>(Messages.FlatAlreadyExists);

        Flat addedFlat = new()
        {
            SectionId = flatDto.SectionId,
            ApartmentId = flatDto.ApartmentId,
            BusinessId = flatDto.BusinessId,
            BranchId = flatDto.BranchId,
            HouseOwnerId = flatDto.HouseOwnerId,
            TenantId = flatDto.TenantId,
            FlatCode = flatDto.FlatCode,
            DoorNumber = flatDto.DoorNumber,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _flatDal.Add(addedFlat);

        FlatDto addedFlatDto = FillDto(addedFlat);

        return new SuccessDataResult<FlatDto>(addedFlatDto, Messages.FlatAdded);
    }

    [TransactionScopeAspect]
    public IResult AddExt(FlatExtDto flatExtDto)
    {
        // Arayüzden gelen id'ye sahip bir apartman var mı kontrol edilir.
        var getApartmentResult = _apartmentBl.GetById(flatExtDto.ApartmentId);
        if (!getApartmentResult.Success)
            return getApartmentResult;

        // Eşsiz bir daire kodu üretebilmek için ilgili apartmandaki tüm daireler getirilir.
        List<Flat> getFlatsResult = _flatDal.GetByApartmentId(flatExtDto.ApartmentId);

        // Daire kodu üretilir.
        string flatCode = _keyService.GenerateFlatCode(getFlatsResult, getApartmentResult.Data.ApartmentCode);

        // Daire eklenir.
        FlatDto flatDto = new()
        {
            SectionId = flatExtDto.SectionId,
            ApartmentId = flatExtDto.ApartmentId,
            BusinessId = flatExtDto.BusinessId,
            BranchId = flatExtDto.BranchId,
            HouseOwnerId = flatExtDto.HouseOwnerId,
            TenantId = flatExtDto.TenantId,
            FlatCode = flatCode,
            DoorNumber = flatExtDto.DoorNumber,
        };
        var addFlatResult = Add(flatDto);
        if (!addFlatResult.Success)
            return addFlatResult;

        return new SuccessResult(Messages.FlatExtAdded);
    }

    public IResult Delete(long id)
    {
        var getFlatResult = GetById(id);
        if (getFlatResult is null)
            return new ErrorResult(Messages.FlatNotFound);

        _flatDal.Delete(id);

        return new SuccessResult(Messages.FlatDeleted);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(long id)
    {
        var deleteSectionResult = Delete(id);
        if (!deleteSectionResult.Success)
            return deleteSectionResult;

        return new SuccessResult(Messages.FlatExtDeleted);
    }

    public IDataResult<FlatDto> GetById(long id)
    {
        Flat searchedFlat = _flatDal.GetById(id);
        if (searchedFlat is null)
            return new ErrorDataResult<FlatDto>(Messages.FlatNotFound);

        FlatDto searchedFlatDto = FillDto(searchedFlat);

        return new SuccessDataResult<FlatDto>(searchedFlatDto, Messages.FlatListedById);
    }

    public IDataResult<FlatExtDto> GetExtById(long id)
    {
        Flat searchedFlat = _flatDal.GetExtById(id);
        if (searchedFlat is null)
            return new ErrorDataResult<FlatExtDto>(Messages.FlatNotFound);

        FlatExtDto searchedFlatExtDto = FillExtDto(searchedFlat);

        return new SuccessDataResult<FlatExtDto>(searchedFlatExtDto, Messages.FlatExtListedById);
    }

    public IDataResult<List<FlatExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<Flat> searchedFlats = _flatDal.GetExtsByBusinessId(businessId);
        if (searchedFlats.Count == 0)
            return new ErrorDataResult<List<FlatExtDto>>(Messages.FlatsNotFound);

        List<FlatExtDto> searchedFlatExtDtos = FillExtDtos(searchedFlats);

        return new SuccessDataResult<List<FlatExtDto>>(searchedFlatExtDtos, Messages.FlatExtsListedByBusinessId);
    }

    public IResult Update(FlatDto flatDto)
    {
        Flat searchedFlat = _flatDal.GetById(flatDto.FlatId);
        if (searchedFlat is null)
            return new ErrorDataResult<FlatDto>(Messages.FlatNotFound);

        searchedFlat.BranchId = flatDto.BranchId;
        searchedFlat.HouseOwnerId = flatDto.HouseOwnerId;
        searchedFlat.TenantId = flatDto.TenantId;
        searchedFlat.DoorNumber = flatDto.DoorNumber;
        searchedFlat.UpdatedAt = DateTimeOffset.Now;
        _flatDal.Update(searchedFlat);

        return new SuccessResult(Messages.FlatUpdated);
    }

    public IResult UpdateExt(FlatExtDto flatExtDto)
    {
        FlatDto flatDto = new()
        {
            FlatId = flatExtDto.FlatId,
            BranchId = flatExtDto.BranchId,
            HouseOwnerId = flatExtDto.HouseOwnerId,
            TenantId = flatExtDto.TenantId,
            DoorNumber = flatExtDto.DoorNumber,
        };
        var updateFlatResult = Update(flatDto);
        if (!updateFlatResult.Success)
            return updateFlatResult;

        return new SuccessResult(Messages.FlatExtUpdated);
    }

    private FlatDto FillDto(Flat flat)
    {
        FlatDto flatDto = new()
        {
            FlatId = flat.FlatId,
            SectionId = flat.SectionId,
            ApartmentId = flat.ApartmentId,
            BusinessId = flat.BusinessId,
            BranchId = flat.BranchId,
            HouseOwnerId = flat.HouseOwnerId,
            TenantId = flat.TenantId,
            FlatCode = flat.FlatCode,
            DoorNumber = flat.DoorNumber,
            CreatedAt = flat.CreatedAt,
            UpdatedAt = flat.UpdatedAt,
        };

        return flatDto;
    }

    private List<FlatDto> FillDtos(List<Flat> flats)
    {
        List<FlatDto> flatDtos = flats.Select(flat => FillDto(flat)).ToList();

        return flatDtos;
    }

    private FlatExtDto FillExtDto(Flat flat)
    {
        FlatExtDto flatExtDto = new()
        {
            FlatId = flat.FlatId,
            SectionId = flat.SectionId,
            ApartmentId = flat.ApartmentId,
            BusinessId = flat.BusinessId,
            BranchId = flat.BranchId,
            HouseOwnerId = flat.HouseOwnerId,
            TenantId = flat.TenantId,
            FlatCode = flat.FlatCode,
            DoorNumber = flat.DoorNumber,
            CreatedAt = flat.CreatedAt,
            UpdatedAt = flat.UpdatedAt,

            SectionName = flat.Section.SectionName,
            ApartmentName = flat.Apartment.ApartmentName,
            HouseOwnerNameSurname = flat.HouseOwner?.NameSurname,
            TenantNameSurname = flat.Tenant?.NameSurname,
        };

        return flatExtDto;
    }

    private List<FlatExtDto> FillExtDtos(List<Flat> flats)
    {
        List<FlatExtDto> flatExtDtos = flats.Select(flat => FillExtDto(flat)).ToList();

        return flatExtDtos;
    }
}
