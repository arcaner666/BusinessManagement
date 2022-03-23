using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete
{
    public class SectionGroupBl
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
            {
                return new ErrorDataResult<SectionGroupDto>(Messages.SectionGroupAlreadyExists);
            }

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
