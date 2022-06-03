using AutoMapper;
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
    private readonly IMapper _mapper;

    public FlatBl(
        IFlatDal flatDal,
        IMapper mapper
    )
    {
        _flatDal = flatDal;
        _mapper = mapper;
    }

    public IDataResult<FlatDto> Add(FlatDto flatDto)
    {
        Flat searchedFlat = _flatDal.GetByFlatCode(flatDto.FlatCode);
        if (searchedFlat is not null)
            return new ErrorDataResult<FlatDto>(Messages.FlatAlreadyExists);

        var addedFlat = _mapper.Map<Flat>(flatDto);

        addedFlat.CreatedAt = DateTimeOffset.Now;
        addedFlat.UpdatedAt = DateTimeOffset.Now;
        long id = _flatDal.Add(addedFlat);
        addedFlat.FlatId = id;

        var addedFlatDto = _mapper.Map<FlatDto>(addedFlat);

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

    public IDataResult<IEnumerable<FlatDto>> GetByApartmentId(long apartmentId)
    {
        IEnumerable<Flat> flats = _flatDal.GetByApartmentId(apartmentId);
        if (flats is null)
            return new ErrorDataResult<IEnumerable<FlatDto>>(Messages.FlatNotFound);

        var flatDtos = _mapper.Map<IEnumerable<FlatDto>>(flats);

        return new SuccessDataResult<IEnumerable<FlatDto>>(flatDtos, Messages.FlatsListedByApartmentId);
    }

    public IDataResult<FlatDto> GetById(long id)
    {
        Flat flat = _flatDal.GetById(id);
        if (flat is null)
            return new ErrorDataResult<FlatDto>(Messages.FlatNotFound);

        var flatDto = _mapper.Map<FlatDto>(flat);

        return new SuccessDataResult<FlatDto>(flatDto, Messages.FlatListedById);
    }

    public IResult Update(FlatDto flatDto)
    {
        Flat flat = _flatDal.GetById(flatDto.FlatId);
        if (flat is null)
            return new ErrorDataResult<FlatDto>(Messages.FlatNotFound);

        flat.BranchId = flatDto.BranchId;
        flat.HouseOwnerId = flatDto.HouseOwnerId;
        flat.TenantId = flatDto.TenantId;
        flat.DoorNumber = flatDto.DoorNumber;
        flat.UpdatedAt = DateTimeOffset.Now;
        _flatDal.Update(flat);

        return new SuccessResult(Messages.FlatUpdated);
    }
}
