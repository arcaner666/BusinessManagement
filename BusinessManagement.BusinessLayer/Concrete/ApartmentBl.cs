using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class ApartmentBl : IApartmentBl
{
    private readonly IApartmentDal _apartmentDal;

    public ApartmentBl(
        IApartmentDal apartmentDal
    )
    {
        _apartmentDal = apartmentDal;
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

    public IResult Delete(long id)
    {
        var getApartmentResult = GetById(id);
        if (getApartmentResult is null)
            return getApartmentResult;

        _apartmentDal.Delete(id);

        return new SuccessResult(Messages.ApartmentDeleted);
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
}
