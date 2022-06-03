using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISectionDal
{
    int Add(Section section);
    void Delete(int id);
    IEnumerable<Section> GetAll();
    IEnumerable<Section> GetByBusinessId(int businessId);
    Section GetById(int id);
    Section GetBySectionCode(string sectionCode);
    SectionExt GetExtById(int id);
    IEnumerable<SectionExt> GetExtsByBusinessId(int businessId);
    void Update(Section section);
}
