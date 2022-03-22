using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract
{
    public interface ISectionGroupDal
    {
        SectionGroup Add(SectionGroup sectionGroup);
        void Delete(long id);
        List<SectionGroup> GetByBusinessId(int businessId);
        SectionGroup GetById(long id);
        SectionGroup GetIfAlreadyExist(int businessId, string sectionGroupName);
        void Update(SectionGroup sectionGroup);
    }
}
