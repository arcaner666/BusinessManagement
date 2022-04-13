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

    public IResult Delete(long id)
    {
        var getFlatResult = GetById(id);
        if (getFlatResult is null)
            return getFlatResult;

        _flatDal.Delete(id);

        return new SuccessResult(Messages.FlatDeleted);
    }

    public IDataResult<FlatDto> GetById(long id)
    {
        Flat searchedFlat = _flatDal.GetById(id);
        if (searchedFlat is null)
            return new ErrorDataResult<FlatDto>(Messages.FlatNotFound);

        FlatDto searchedFlatDto = FillDto(searchedFlat);

        return new SuccessDataResult<FlatDto>(searchedFlatDto, Messages.FlatListedById);
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
}
