using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ISectionExtBl
{
    IResult AddExt(SectionExtDto sectionExtDto);
    IResult DeleteExt(int id);
    IDataResult<SectionExtDto> GetExtById(int id);
    IDataResult<IEnumerable<SectionExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(SectionExtDto sectionExtDto);
}
