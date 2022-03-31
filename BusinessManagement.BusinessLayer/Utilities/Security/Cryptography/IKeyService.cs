using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;

public interface IKeyService
{
    string GenerateApartmentCode(List<Apartment> apartments, string sectionCode);
    string GenerateFlatCode(List<Flat> flats, string apartmentCode);
    string GenerateSectionCode(List<Section> sections);
    string GenerateUniqueKey(int size, char[] charArray);
}
