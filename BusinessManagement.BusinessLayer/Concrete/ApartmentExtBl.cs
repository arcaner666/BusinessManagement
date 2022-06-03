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

public class ApartmentExtBl : IApartmentExtBl
{
    private readonly IApartmentBl _apartmentBl;
    private readonly IApartmentDal _apartmentDal;
    private readonly IFlatBl _flatBl;
    private readonly IKeyService _keyService;
    private readonly IMapper _mapper;
    private readonly ISectionBl _sectionBl;

    public ApartmentExtBl(
        IApartmentBl apartmentBl,
        IApartmentDal apartmentDal,
        IFlatBl flatBl,
        IKeyService keyService,
        IMapper mapper,
        ISectionBl sectionBl
    )
    {
        _apartmentBl = apartmentBl;
        _apartmentDal = apartmentDal;
        _flatBl = flatBl;
        _keyService = keyService;
        _mapper = mapper;
        _sectionBl = sectionBl;
    }

    [TransactionScopeAspect]
    public IResult AddExt(ApartmentExtDto apartmentExtDto)
    {
        // Arayüzden gelen id'ye sahip bir site var mı kontrol edilir.
        var getSectionResult = _sectionBl.GetById(apartmentExtDto.SectionId);
        if (!getSectionResult.Success)
            return getSectionResult;

        // Eşsiz bir apartman kodu üretebilmek için ilgili sitedeki tüm apartmanlar getirilir.
        IEnumerable<ApartmentDto> apartmentDtos = _apartmentDal.GetBySectionId(apartmentExtDto.SectionId);

        // Apartman kodu üretilir.
        string apartmentCode = _keyService.GenerateApartmentCode(apartmentDtos, getSectionResult.Data.SectionCode);

        // Apartman eklenir.
        ApartmentDto apartmentDto = new()
        {
            SectionId = apartmentExtDto.SectionId,
            BusinessId = apartmentExtDto.BusinessId,
            BranchId = apartmentExtDto.BranchId,
            ManagerId = apartmentExtDto.ManagerId,
            ApartmentName = apartmentExtDto.ApartmentName,
            ApartmentCode = apartmentCode,
            BlockNumber = apartmentExtDto.BlockNumber,
        };
        var addApartmentResult = _apartmentBl.Add(apartmentDto);
        if (!addApartmentResult.Success)
            return addApartmentResult;

        return new SuccessResult(Messages.ApartmentExtAdded);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(long id)
    {
        // Apartmandaki daireler getirilir.
        var getFlatsResult = _flatBl.GetByApartmentId(id);
        if (getFlatsResult is null)
            return getFlatsResult;

        // Apartmandaki daireler silinir.
        foreach (FlatDto flatDto in getFlatsResult.Data)
        {
            _flatBl.Delete(flatDto.FlatId);
        }

        // Apartman silinir.
        var deleteApartmentResult = _apartmentBl.Delete(id);
        if (!deleteApartmentResult.Success)
            return deleteApartmentResult;

        return new SuccessResult(Messages.ApartmentExtDeleted);
    }

    public IDataResult<ApartmentExtDto> GetExtById(long id)
    {
        ApartmentExtDto apartmentExtDto = _apartmentDal.GetExtById(id);
        if (apartmentExtDto is null)
            return new ErrorDataResult<ApartmentExtDto>(Messages.ApartmentNotFound);

        return new SuccessDataResult<ApartmentExtDto>(apartmentExtDto, Messages.ApartmentExtListedById);
    }

    public IDataResult<IEnumerable<ApartmentExtDto>> GetExtsByBusinessId(int businessId)
    {
        IEnumerable<ApartmentExtDto> apartmentExtDtos = _apartmentDal.GetExtsByBusinessId(businessId);
        if (!apartmentExtDtos.Any())
            return new ErrorDataResult<IEnumerable<ApartmentExtDto>>(Messages.ApartmentsNotFound);

        return new SuccessDataResult<IEnumerable<ApartmentExtDto>>(apartmentExtDtos, Messages.ApartmentExtsListedByBusinessId);
    }

    public IResult UpdateExt(ApartmentExtDto apartmentExtDto)
    {
        ApartmentDto apartmentDto = new()
        {
            ApartmentId = apartmentExtDto.ApartmentId,
            BranchId = apartmentExtDto.BranchId,
            ManagerId = apartmentExtDto.ManagerId,
            ApartmentName = apartmentExtDto.ApartmentName,
            BlockNumber = apartmentExtDto.BlockNumber,
        };
        var updateApartmentResult = _apartmentBl.Update(apartmentDto);
        if (!updateApartmentResult.Success)
            return updateApartmentResult;

        return new SuccessResult(Messages.ApartmentExtUpdated);
    }
}
