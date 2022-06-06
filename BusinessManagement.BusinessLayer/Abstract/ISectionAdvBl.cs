using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ISectionAdvBl
{
    IResult Add(SectionExtDto sectionExtDto);
    IResult Delete(int id);
    IResult Update(SectionExtDto sectionExtDto);
}
