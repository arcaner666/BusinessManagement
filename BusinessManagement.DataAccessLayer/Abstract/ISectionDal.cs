using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ISectionDal
{
    Section Add(Section section);
    void Delete(int id);
    List<Section> GetAll();
    List<Section> GetByBusinessId(int businessId);
    Section GetById(int id);
    Section GetBySectionCode(string sectionCode);
    Section GetExtById(int id);
    List<Section> GetExtsByBusinessId(int businessId);
    void Update(Section section);
}
