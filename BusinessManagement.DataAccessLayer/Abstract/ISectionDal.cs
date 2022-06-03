using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISectionDal
{
    int Add(SectionDto sectionDto);
    void Delete(int id);
    IEnumerable<SectionDto> GetAll();
    IEnumerable<SectionDto> GetByBusinessId(int businessId);
    SectionDto GetById(int id);
    SectionDto GetBySectionCode(string sectionCode);
    SectionExtDto GetExtById(int id);
    IEnumerable<SectionExtDto> GetExtsByBusinessId(int businessId);
    void Update(SectionDto sectionDto);
}
