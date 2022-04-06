using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ISectionBl
{
    IDataResult<SectionDto> Add(SectionDto sectionDto);
    IResult AddExt(SectionExtDto sectionExtDto);
    IResult Delete(int id);
    IResult DeleteExt(int id);
    IDataResult<List<SectionDto>> GetByBusinessId(int businessId);
    IDataResult<SectionDto> GetById(int id);
    IDataResult<SectionExtDto> GetExtById(int id);
    IDataResult<List<SectionExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(SectionDto sectionDto);
    IResult UpdateExt(SectionExtDto sectionExtDto);
}
