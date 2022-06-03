using AutoMapper;
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
    private readonly IMapper _mapper;

    public ApartmentBl(
        IApartmentDal apartmentDal,
        IMapper mapper
    )
    {
        _apartmentDal = apartmentDal;
        _mapper = mapper;
    }

    public IDataResult<ApartmentDto> Add(ApartmentDto apartmentDto)
    {
        Apartment searchedApartment = _apartmentDal.GetByApartmentCode(apartmentDto.ApartmentCode);
        if (searchedApartment is not null)
            return new ErrorDataResult<ApartmentDto>(Messages.ApartmentAlreadyExists);

        var addedApartment = _mapper.Map<Apartment>(apartmentDto);

        addedApartment.CreatedAt = DateTimeOffset.Now;
        addedApartment.UpdatedAt = DateTimeOffset.Now;
        long id = _apartmentDal.Add(addedApartment);
        addedApartment.ApartmentId = id;

        var addedApartmentDto = _mapper.Map<ApartmentDto>(addedApartment);

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

    public IDataResult<IEnumerable<ApartmentDto>> GetByBusinessId(int businessId)
    {
        IEnumerable<Apartment> apartments = _apartmentDal.GetByBusinessId(businessId);
        if (!apartments.Any())
            return new ErrorDataResult<IEnumerable<ApartmentDto>>(Messages.ApartmentsNotFound);

        var apartmentDtos = _mapper.Map<IEnumerable<ApartmentDto>>(apartments);

        return new SuccessDataResult<IEnumerable<ApartmentDto>>(apartmentDtos, Messages.ApartmentsListedByBusinessId);
    }

    public IDataResult<ApartmentDto> GetById(long id)
    {
        Apartment apartment = _apartmentDal.GetById(id);
        if (apartment is null)
            return new ErrorDataResult<ApartmentDto>(Messages.ApartmentNotFound);

        var apartmentDto = _mapper.Map<ApartmentDto>(apartment);

        return new SuccessDataResult<ApartmentDto>(apartmentDto, Messages.ApartmentListedById);
    }

    public IDataResult<IEnumerable<ApartmentDto>> GetBySectionId(int sectionId)
    {
        IEnumerable<Apartment> apartments = _apartmentDal.GetBySectionId(sectionId);
        if (!apartments.Any())
            return new ErrorDataResult<IEnumerable<ApartmentDto>>(Messages.ApartmentsNotFound);

        var apartmentDtos = _mapper.Map<IEnumerable<ApartmentDto>>(apartments);

        return new SuccessDataResult<IEnumerable<ApartmentDto>>(apartmentDtos, Messages.ApartmentsListedBySectionId);
    }

    public IResult Update(ApartmentDto apartmentDto)
    {
        Apartment apartment = _apartmentDal.GetById(apartmentDto.ApartmentId);
        if (apartment is null)
            return new ErrorDataResult<ApartmentDto>(Messages.ApartmentNotFound);

        apartment.BranchId = apartmentDto.BranchId;
        apartment.ManagerId = apartmentDto.ManagerId;
        apartment.ApartmentName = apartmentDto.ApartmentName;
        apartment.BlockNumber = apartmentDto.BlockNumber;
        apartment.UpdatedAt = DateTimeOffset.Now;
        _apartmentDal.Update(apartment);

        return new SuccessResult(Messages.ApartmentUpdated);
    }
}
