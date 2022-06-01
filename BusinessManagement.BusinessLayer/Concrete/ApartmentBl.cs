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
        ApartmentDto searchedApartmentDto = _apartmentDal.GetByApartmentCode(apartmentDto.ApartmentCode);
        if (searchedApartmentDto is not null)
            return new ErrorDataResult<ApartmentDto>(Messages.ApartmentAlreadyExists);

        apartmentDto.CreatedAt = DateTimeOffset.Now;
        apartmentDto.UpdatedAt = DateTimeOffset.Now;
        long id = _apartmentDal.Add(apartmentDto);
        apartmentDto.ApartmentId = id;

        return new SuccessDataResult<ApartmentDto>(apartmentDto, Messages.ApartmentAdded);
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
        List<ApartmentDto> apartmentDtos = _apartmentDal.GetByBusinessId(businessId);
        if (apartmentDtos.Count == 0)
            return new ErrorDataResult<List<ApartmentDto>>(Messages.ApartmentsNotFound);

        return new SuccessDataResult<List<ApartmentDto>>(apartmentDtos, Messages.ApartmentsListedByBusinessId);
    }

    public IDataResult<ApartmentDto> GetById(long id)
    {
        ApartmentDto apartmentDto = _apartmentDal.GetById(id);
        if (apartmentDto is null)
            return new ErrorDataResult<ApartmentDto>(Messages.ApartmentNotFound);

        return new SuccessDataResult<ApartmentDto>(apartmentDto, Messages.ApartmentListedById);
    }

    public IDataResult<List<ApartmentDto>> GetBySectionId(int sectionId)
    {
        List<ApartmentDto> apartmentDtos = _apartmentDal.GetBySectionId(sectionId);
        if (apartmentDtos.Count == 0)
            return new ErrorDataResult<List<ApartmentDto>>(Messages.ApartmentsNotFound);

        return new SuccessDataResult<List<ApartmentDto>>(apartmentDtos, Messages.ApartmentsListedBySectionId);
    }

    public IResult Update(ApartmentDto apartmentDto)
    {
        var searchedApartmentResult = GetById(apartmentDto.ApartmentId);
        if (!searchedApartmentResult.Success)
            return searchedApartmentResult;

        searchedApartmentResult.Data.BranchId = apartmentDto.BranchId;
        searchedApartmentResult.Data.ManagerId = apartmentDto.ManagerId;
        searchedApartmentResult.Data.ApartmentName = apartmentDto.ApartmentName;
        searchedApartmentResult.Data.BlockNumber = apartmentDto.BlockNumber;
        searchedApartmentResult.Data.UpdatedAt = DateTimeOffset.Now;
        _apartmentDal.Update(searchedApartmentResult.Data);

        return new SuccessResult(Messages.ApartmentUpdated);
    }
}
