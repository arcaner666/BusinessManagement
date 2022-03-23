using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract
{
    public interface ISectionGroupBl
    {
        IDataResult<SectionGroupDto> Add(SectionGroupDto sectionGroupDto);
    }
}
