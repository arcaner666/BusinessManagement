using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class SectionGroupBl : ISectionGroupBl
    {
        private readonly ISectionGroupDal _sectionGroupDal;

        public SectionGroupBl(
            ISectionGroupDal sectionGroupDal
        )
        {
            _sectionGroupDal = sectionGroupDal;
        }

        public IDataResult<SectionGroupDto> Add(SectionGroupDto sectionGroupDto)
        {
            SectionGroup getSectionGroup = _sectionGroupDal.GetByBusinessIdAndSectionGroupName(sectionGroupDto.BusinessId, sectionGroupDto.SectionGroupName);
            if (getSectionGroup != null)
                return new ErrorDataResult<SectionGroupDto>(Messages.SectionGroupAlreadyExists);

            SectionGroup addSectionGroup = new()
            {
                BusinessId = sectionGroupDto.BusinessId,
                BranchId = sectionGroupDto.BranchId,
                SectionGroupName = sectionGroupDto.SectionGroupName,
                CreatedAt = DateTimeOffset.Now,
                UpdatedAt = DateTimeOffset.Now,
            };
            _sectionGroupDal.Add(addSectionGroup);

            SectionGroupDto addSectionGroupDto = FillDto(addSectionGroup);

            return new SuccessDataResult<SectionGroupDto>(addSectionGroupDto, Messages.SectionGroupAdded);
        }

        public IResult Delete(long id)
        {
            var getSectionGroupResult = GetById(id);
            if (!getSectionGroupResult.Success)
                return getSectionGroupResult;

            _sectionGroupDal.Delete(id);

            return new SuccessResult(Messages.SectionGroupDeleted);
        }

        public IDataResult<List<SectionGroupDto>> GetByBusinessId(int businessId)
        {
            List<SectionGroup> sectionGroups = _sectionGroupDal.GetByBusinessId(businessId);
            if (sectionGroups.Count == 0)
                return new ErrorDataResult<List<SectionGroupDto>>(Messages.SectionGroupsNotFound);

            List<SectionGroupDto> sectionGroupDtos = FillDtos(sectionGroups);

            return new SuccessDataResult<List<SectionGroupDto>>(sectionGroupDtos, Messages.SectionGroupsListedByBusinessId);
        }

        public IDataResult<SectionGroupDto> GetById(long id)
        {
            SectionGroup getSectionGroup = _sectionGroupDal.GetById(id);
            if (getSectionGroup == null)
                return new ErrorDataResult<SectionGroupDto>(Messages.SectionGroupNotFound);

            SectionGroupDto getSectionGroupDto = FillDto(getSectionGroup);

            return new SuccessDataResult<SectionGroupDto>(getSectionGroupDto, Messages.SectionGroupListedById);
        }

        public IResult Update(SectionGroupDto sectionGroupDto)
        {
            SectionGroup getSectionGroup = _sectionGroupDal.GetById(sectionGroupDto.SectionGroupId);
            if (getSectionGroup == null)
                return new ErrorDataResult<SectionDto>(Messages.SectionGroupNotFound);

            getSectionGroup.BranchId = sectionGroupDto.BranchId;
            getSectionGroup.SectionGroupName = sectionGroupDto.SectionGroupName;
            getSectionGroup.UpdatedAt = DateTimeOffset.Now;
            _sectionGroupDal.Update(getSectionGroup);

            return new SuccessResult(Messages.SectionGroupUpdated);

        }

        private SectionGroupDto FillDto(SectionGroup sectionGroup)
        {
            SectionGroupDto sectionGroupDto = new()
            {
                SectionGroupId = sectionGroup.SectionGroupId,
                BusinessId = sectionGroup.BusinessId,
                BranchId = sectionGroup.BranchId,
                SectionGroupName = sectionGroup.SectionGroupName,
                CreatedAt = sectionGroup.CreatedAt,
                UpdatedAt = sectionGroup.UpdatedAt,
            };

            return sectionGroupDto;
        }

        private List<SectionGroupDto> FillDtos(List<SectionGroup> sectionGroups)
        {
            List<SectionGroupDto> sectionGroupDtos = sectionGroups.Select(sectionGroup => FillDto(sectionGroup)).ToList();

            return sectionGroupDtos;
        }
    }
}
