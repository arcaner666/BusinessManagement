using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISectionGroupDal
{
    long Add(SectionGroupDto sectionGroupDto);
    void Delete(long id);
    IEnumerable<SectionGroupDto> GetByBusinessId(int businessId);
    SectionGroupDto GetByBusinessIdAndSectionGroupName(int businessId, string sectionGroupName);
    SectionGroupDto GetById(long id);
    void Update(SectionGroupDto sectionGroupDto);
}
