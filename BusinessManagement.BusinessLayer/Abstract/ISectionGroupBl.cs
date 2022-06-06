using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ISectionGroupBl
{
    IDataResult<SectionGroupDto> Add(SectionGroupDto sectionGroupDto);
    IResult Delete(long id);
    IDataResult<SectionGroupDto> GetById(long id);
    IDataResult<List<SectionGroupDto>> GetByBusinessId(int businessId);
    IResult Update(SectionGroupDto sectionGroupDto);
}
