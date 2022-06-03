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
    private readonly IFlatDal _flatDal;

    public FlatBl(
        IFlatDal flatDal
    )
    {
        _flatDal = flatDal;
    }

    public IDataResult<FlatDto> Add(FlatDto flatDto)
    {
        FlatDto searchedFlatDto = _flatDal.GetByFlatCode(flatDto.FlatCode);
        if (searchedFlatDto is not null)
            return new ErrorDataResult<FlatDto>(Messages.FlatAlreadyExists);

        flatDto.CreatedAt = DateTimeOffset.Now;
        flatDto.UpdatedAt = DateTimeOffset.Now;
        long id = _flatDal.Add(flatDto);
        flatDto.FlatId = id;

        return new SuccessDataResult<FlatDto>(flatDto, Messages.FlatAdded);
    }

    public IResult Delete(long id)
    {
        var getFlatResult = GetById(id);
        if (getFlatResult is null)
            return getFlatResult;

        _flatDal.Delete(id);

        return new SuccessResult(Messages.FlatDeleted);
    }

    public IDataResult<IEnumerable<FlatDto>> GetByApartmentId(long apartmentId)
    {
        IEnumerable<FlatDto> flatDtos = _flatDal.GetByApartmentId(apartmentId);
        if (flatDtos is null)
            return new ErrorDataResult<IEnumerable<FlatDto>>(Messages.FlatNotFound);

        return new SuccessDataResult<IEnumerable<FlatDto>>(flatDtos, Messages.FlatsListedByApartmentId);
    }

    public IDataResult<FlatDto> GetById(long id)
    {
        FlatDto flatDto = _flatDal.GetById(id);
        if (flatDto is null)
            return new ErrorDataResult<FlatDto>(Messages.FlatNotFound);

        return new SuccessDataResult<FlatDto>(flatDto, Messages.FlatListedById);
    }

    public IResult Update(FlatDto flatDto)
    {
        var searchedFlatResult = GetById(flatDto.FlatId);
        if (!searchedFlatResult.Success)
            return searchedFlatResult;

        searchedFlatResult.Data.BranchId = flatDto.BranchId;
        searchedFlatResult.Data.HouseOwnerId = flatDto.HouseOwnerId;
        searchedFlatResult.Data.TenantId = flatDto.TenantId;
        searchedFlatResult.Data.DoorNumber = flatDto.DoorNumber;
        searchedFlatResult.Data.UpdatedAt = DateTimeOffset.Now;
        _flatDal.Update(searchedFlatResult.Data);

        return new SuccessResult(Messages.FlatUpdated);
    }
}
