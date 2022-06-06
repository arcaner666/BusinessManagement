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

public class FlatAdvBl : IFlatAdvBl
{
    private readonly IApartmentBl _apartmentBl;
    private readonly IFlatBl _flatBl;
    private readonly IFlatDal _flatDal;
    private readonly IKeyService _keyService;
    private readonly IMapper _mapper;

    public FlatAdvBl(
        IApartmentBl apartmentBl,
        IFlatBl flatBl,
        IFlatDal flatDal,
        IKeyService keyService,
        IMapper mapper
    )
    {
        _apartmentBl = apartmentBl;
        _flatBl = flatBl;
        _flatDal = flatDal;
        _keyService = keyService;
        _mapper = mapper;
    }

    [TransactionScopeAspect]
    public IResult Add(FlatExtDto flatExtDto)
    {
        // Arayüzden gelen id'ye sahip bir apartman var mı kontrol edilir.
        var getApartmentResult = _apartmentBl.GetById(flatExtDto.ApartmentId);
        if (!getApartmentResult.Success)
            return getApartmentResult;

        // Eşsiz bir daire kodu üretebilmek için ilgili apartmandaki tüm daireler getirilir.
        List<Flat> flats = _flatDal.GetByApartmentId(flatExtDto.ApartmentId);

        var flatDtos = _mapper.Map<List<FlatDto>>(flats);

        // Daire kodu üretilir.
        string flatCode = _keyService.GenerateFlatCode(flatDtos, getApartmentResult.Data.ApartmentCode);

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
        var addFlatResult = _flatBl.Add(flatDto);
        if (!addFlatResult.Success)
            return addFlatResult;

        return new SuccessResult(Messages.FlatExtAdded);
    }

    [TransactionScopeAspect]
    public IResult Delete(long id)
    {
        var deleteFlatResult = _flatBl.Delete(id);
        if (!deleteFlatResult.Success)
            return deleteFlatResult;

        return new SuccessResult(Messages.FlatExtDeleted);
    }

    public IResult Update(FlatExtDto flatExtDto)
    {
        FlatDto flatDto = new()
        {
            FlatId = flatExtDto.FlatId,
            BranchId = flatExtDto.BranchId,
            HouseOwnerId = flatExtDto.HouseOwnerId,
            TenantId = flatExtDto.TenantId,
            DoorNumber = flatExtDto.DoorNumber,
        };
        var updateFlatResult = _flatBl.Update(flatDto);
        if (!updateFlatResult.Success)
            return updateFlatResult;

        return new SuccessResult(Messages.FlatExtUpdated);
    }
}
