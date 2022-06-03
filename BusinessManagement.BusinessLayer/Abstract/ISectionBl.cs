using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ISectionBl
{
    IDataResult<SectionDto> Add(SectionDto sectionDto);
    IResult Delete(int id);
    IDataResult<IEnumerable<SectionDto>> GetByBusinessId(int businessId);
    IDataResult<SectionDto> GetById(int id);
    IResult Update(SectionDto sectionDto);
}
