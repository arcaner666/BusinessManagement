using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISectionDal
{
    int Add(SectionDto sectionDto);
    void Delete(int id);
    List<SectionDto> GetAll();
    List<SectionDto> GetByBusinessId(int businessId);
    SectionDto GetById(int id);
    SectionDto GetBySectionCode(string sectionCode);
    SectionExtDto GetExtById(int id);
    List<SectionExtDto> GetExtsByBusinessId(int businessId);
    void Update(SectionDto sectionDto);
}
