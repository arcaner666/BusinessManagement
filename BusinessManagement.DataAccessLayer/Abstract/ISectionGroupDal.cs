using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISectionGroupDal
{
    SectionGroup Add(SectionGroup sectionGroup);
    void Delete(long id);
    List<SectionGroup> GetByBusinessId(int businessId);
    SectionGroup GetByBusinessIdAndSectionGroupName(int businessId, string sectionGroupName);
    SectionGroup GetById(long id);
    void Update(SectionGroup sectionGroup);
}
