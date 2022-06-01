using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class FlatExtBl : IFlatExtBl
{
    private readonly IApartmentBl _apartmentBl;
    private readonly IFlatBl _flatBl;
    private readonly IFlatDal _flatDal;
    private readonly IKeyService _keyService;

    public FlatExtBl(
        IApartmentBl apartmentBl,
        IFlatBl flatBl,
        IFlatDal flatDal,
        IKeyService keyService
    )
    {
        _apartmentBl = apartmentBl;
        _flatBl = flatBl;
        _flatDal = flatDal;
        _keyService = keyService;
    }

    [TransactionScopeAspect]
    public IResult AddExt(FlatExtDto flatExtDto)
    {
        // Arayüzden gelen id'ye sahip bir apartman var mı kontrol edilir.
        var getApartmentResult = _apartmentBl.GetById(flatExtDto.ApartmentId);
        if (!getApartmentResult.Success)
            return getApartmentResult;

        // Eşsiz bir daire kodu üretebilmek için ilgili apartmandaki tüm daireler getirilir.
        List<FlatDto> flatDtos = _flatDal.GetByApartmentId(flatExtDto.ApartmentId);

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
    public IResult DeleteExt(long id)
    {
        var deleteFlatResult = _flatBl.Delete(id);
        if (!deleteFlatResult.Success)
            return deleteFlatResult;

        return new SuccessResult(Messages.FlatExtDeleted);
    }

    public IDataResult<FlatExtDto> GetExtById(long id)
    {
        FlatExtDto flatExtDto = _flatDal.GetExtById(id);
        if (flatExtDto is null)
            return new ErrorDataResult<FlatExtDto>(Messages.FlatNotFound);

        return new SuccessDataResult<FlatExtDto>(flatExtDto, Messages.FlatExtListedById);
    }

    public IDataResult<List<FlatExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<FlatExtDto> flatExtDtos = _flatDal.GetExtsByBusinessId(businessId);
        if (flatExtDtos.Count == 0)
            return new ErrorDataResult<List<FlatExtDto>>(Messages.FlatsNotFound);

        return new SuccessDataResult<List<FlatExtDto>>(flatExtDtos, Messages.FlatExtsListedByBusinessId);
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
        var updateFlatResult = _flatBl.Update(flatDto);
        if (!updateFlatResult.Success)
            return updateFlatResult;

        return new SuccessResult(Messages.FlatExtUpdated);
    }
}
