using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class SectionBl : ISectionBl
    {
        private readonly IFullAddressBl _fullAddressBl;
        private readonly IKeyService _keyService;
        private readonly ISectionDal _sectionDal;

        public SectionBl(
            IFullAddressBl fullAddressBl,
            IKeyService keyService,
            ISectionDal sectionDal
        )
        {
            _fullAddressBl = fullAddressBl;
            _keyService = keyService;
            _sectionDal = sectionDal;
        }

        public IDataResult<SectionDto> Add(SectionDto sectionDto)
        {
            Section getSection = _sectionDal.GetBySectionCode(sectionDto.SectionCode);
            if (getSection != null)
                return new ErrorDataResult<SectionDto>(Messages.SectionAlreadyExists);

            Section addSection = new()
            {
                SectionGroupId = sectionDto.SectionGroupId,
                BusinessId = sectionDto.BusinessId,
                BranchId = sectionDto.BranchId,
                ManagerId = sectionDto.ManagerId,
                FullAddressId = sectionDto.FullAddressId,
                SectionName = sectionDto.SectionName,
                SectionCode = sectionDto.SectionCode,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            _sectionDal.Add(addSection);

            SectionDto addSectionDto = FillDto(addSection);

            return new SuccessDataResult<SectionDto>(addSectionDto, Messages.SectionAdded);
        }

        [TransactionScopeAspect]
        public IResult AddExt(SectionExtDto sectionExtDto)
        {
            // Sitenin adresi eklenir.
            FullAddressDto fullAddressDto = new()
            {
                CityId = sectionExtDto.CityId,
                DistrictId = sectionExtDto.DistrictId,
                AddressTitle = "Site Adresi",
                PostalCode = sectionExtDto.PostalCode,
                AddressText = sectionExtDto.AddressText,
            };
            var addFullAddressResult = _fullAddressBl.Add(fullAddressDto);
            if (!addFullAddressResult.Success)
                return addFullAddressResult;

            // Eşsiz bir site kodu oluşturmak için tüm siteler getirilir.
            List<Section> getSections = _sectionDal.GetAll();

            // Site kodu üretilir.
            string sectionCode = _keyService.GenerateSectionCode(getSections);

            // Site eklenir.
            SectionDto sectionDto = new()
            {
                SectionGroupId = sectionExtDto.SectionGroupId,
                BusinessId = sectionExtDto.BusinessId,
                BranchId = sectionExtDto.BranchId,
                ManagerId = sectionExtDto.ManagerId,
                FullAddressId = addFullAddressResult.Data.FullAddressId,
                SectionName = sectionExtDto.SectionName,
                SectionCode = sectionCode,
            };
            var addSectionResult = Add(sectionDto);
            if (!addSectionResult.Success)
                return addSectionResult;

            return new SuccessResult(Messages.SectionExtAdded);
        }

        public IResult Delete(int id)
        {
            _sectionDal.Delete(id);

            return new SuccessResult(Messages.SectionDeleted);
        }

        [TransactionScopeAspect]
        public IResult DeleteExt(int id)
        {
            // Site getirilir. FullAddressId'ye ulaşmak için gereklidir.
            var getSectionResult = GetById(id);
            if (!getSectionResult.Success)
                return getSectionResult;

            // Site silinir.
            var deleteSectionResult = Delete(getSectionResult.Data.SectionId);
            if (!deleteSectionResult.Success)
                return deleteSectionResult;

            // Şubenin adresi silinir.
            var deleteFullAddressResult = _fullAddressBl.Delete(getSectionResult.Data.FullAddressId);
            if (!deleteFullAddressResult.Success)
                return deleteFullAddressResult;

            return new SuccessResult(Messages.SectionExtDeleted);
        }

        public IDataResult<SectionDto> GetById(int id)
        {
            Section getSection = _sectionDal.GetById(id);
            if (getSection == null)
                return new ErrorDataResult<SectionDto>(Messages.SectionNotFound);

            SectionDto getSectionDto = FillDto(getSection);

            return new SuccessDataResult<SectionDto>(getSectionDto, Messages.SectionListedById);
        }

        public IDataResult<SectionExtDto> GetExtById(int id)
        {
            Section getSection = _sectionDal.GetExtById(id);
            if (getSection == null)
                return new ErrorDataResult<SectionExtDto>(Messages.SectionNotFound);

            SectionExtDto getSectionExtDto = FillExtDto(getSection);

            return new SuccessDataResult<SectionExtDto>(getSectionExtDto, Messages.SectionExtListedById);
        }

        public IDataResult<List<SectionExtDto>> GetExtsByBusinessId(int businessId)
        {
            List<Section> sections = _sectionDal.GetExtsByBusinessId(businessId);
            if (sections.Count == 0)
                return new ErrorDataResult<List<SectionExtDto>>(Messages.SectionsNotFound);

            List<SectionExtDto> sectionExtDtos = FillExtDtos(sections);

            return new SuccessDataResult<List<SectionExtDto>>(sectionExtDtos, Messages.SectionExtsListedByBusinessId);
        }

        public IResult Update(SectionDto sectionDto)
        {
            Section getSection = _sectionDal.GetById(sectionDto.SectionId);
            if (getSection == null)
                return new ErrorDataResult<SectionDto>(Messages.SectionNotFound);

            getSection.SectionId = sectionDto.SectionId;
            getSection.SectionGroupId = sectionDto.SectionGroupId;
            getSection.BranchId = sectionDto.BranchId;
            getSection.ManagerId = sectionDto.ManagerId;
            getSection.SectionName = sectionDto.SectionName;
            getSection.UpdatedAt = DateTimeOffset.Now;
            _sectionDal.Update(getSection);

            return new SuccessResult(Messages.SectionUpdated);
        }

        [TransactionScopeAspect]
        public IResult UpdateExt(SectionExtDto sectionExtDto)
        {
            // Site adresi güncellenir.
            FullAddressDto fullAddressDto = new()
            {
                FullAddressId = sectionExtDto.FullAddressId,
                CityId = sectionExtDto.CityId,
                AddressTitle = "Site Adresi",
                DistrictId = sectionExtDto.DistrictId,
                PostalCode = sectionExtDto.PostalCode,
                AddressText = sectionExtDto.AddressText,
            };
            var updateFullAddressResult = _fullAddressBl.Update(fullAddressDto);
            if (!updateFullAddressResult.Success)
                return updateFullAddressResult;

            // Site güncellenir.
            SectionDto sectionDto = new()
            {
                SectionId = sectionExtDto.SectionId,
                SectionGroupId = sectionExtDto.SectionGroupId,
                BranchId = sectionExtDto.BranchId,
                ManagerId = sectionExtDto.ManagerId,
                SectionName = sectionExtDto.SectionName,
            };
            var updateSectionResult = Update(sectionDto);
            if (!updateSectionResult.Success)
                return updateSectionResult;

            return new SuccessResult(Messages.SectionExtUpdated);
        }

        private SectionDto FillDto(Section section)
        {
            SectionDto sectionDto = new()
            {
                SectionId = section.SectionId,
                SectionGroupId = section.SectionGroupId,
                BusinessId = section.BusinessId,
                BranchId = section.BranchId,
                ManagerId = section.ManagerId,
                FullAddressId = section.FullAddressId,
                SectionName = section.SectionName,
                SectionCode = section.SectionCode,
                CreatedAt = section.CreatedAt,
                UpdatedAt = section.UpdatedAt,
            };

            return sectionDto;
        }

        private List<SectionDto> FillDtos(List<Section> sections)
        {
            List<SectionDto> sectionDtos = sections.Select(section => FillDto(section)).ToList();

            return sectionDtos;
        }

        private SectionExtDto FillExtDto(Section section)
        {
            SectionExtDto sectionExtDto = new()
            {
                SectionId = section.SectionId,
                SectionGroupId = section.SectionGroupId,
                BusinessId = section.BusinessId,
                BranchId = section.BranchId,
                ManagerId = section.ManagerId,
                FullAddressId = section.FullAddressId,
                SectionName = section.SectionName,
                SectionCode = section.SectionCode,
                CreatedAt = section.CreatedAt,
                UpdatedAt = section.UpdatedAt,

                SectionGroupName = section.SectionGroup.SectionGroupName,
                ManagerNameSurname = section.Manager.NameSurname,
                CityId = section.FullAddress.CityId,
                DistrictId = section.FullAddress.DistrictId,
                AddressTitle = section.FullAddress.AddressTitle,
                PostalCode = section.FullAddress.PostalCode,
                AddressText = section.FullAddress.AddressText,
                CityName = section.FullAddress.City.CityName,
                DistrictName = section.FullAddress.District.DistrictName,
            };

            return sectionExtDto;
        }

        private List<SectionExtDto> FillExtDtos(List<Section> sections)
        {
            List<SectionExtDto> sectionExtDtos = sections.Select(section => FillExtDto(section)).ToList();

            return sectionExtDtos;
        }
    }
}
