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

    public IDataResult<List<FlatDto>> GetByApartmentId(long apartmentId)
    {
        List<Flat> flats = _flatDal.GetByApartmentId(apartmentId);
        if (flats is null)
            return new ErrorDataResult<List<FlatDto>>(Messages.FlatNotFound);

        var flatDtos = _mapper.Map<List<FlatDto>>(flats);

        return new SuccessDataResult<List<FlatDto>>(flatDtos, Messages.FlatsListedByApartmentId);
    }

    public IDataResult<FlatDto> GetById(long id)
    {
        Flat flat = _flatDal.GetById(id);
        if (flat is null)
            return new ErrorDataResult<FlatDto>(Messages.FlatNotFound);

        var flatDto = _mapper.Map<FlatDto>(flat);

        return new SuccessDataResult<FlatDto>(flatDto, Messages.FlatListedById);
    }

    public IDataResult<FlatExtDto> GetExtById(long id)
    {
        FlatExt flatExt = _flatDal.GetExtById(id);
        if (flatExt is null)
            return new ErrorDataResult<FlatExtDto>(Messages.FlatNotFound);

        var flatExtDto = _mapper.Map<FlatExtDto>(flatExt);

        return new SuccessDataResult<FlatExtDto>(flatExtDto, Messages.FlatExtListedById);
    }

    public IDataResult<List<FlatExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<FlatExt> flatExts = _flatDal.GetExtsByBusinessId(businessId);
        if (!flatExts.Any())
            return new ErrorDataResult<List<FlatExtDto>>(Messages.FlatsNotFound);

        var flatExtDtos = _mapper.Map<List<FlatExtDto>>(flatExts);

        return new SuccessDataResult<List<FlatExtDto>>(flatExtDtos, Messages.FlatExtsListedByBusinessId);
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
