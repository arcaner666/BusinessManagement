using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;

public interface IKeyService
{
    string GenerateApartmentCode(IEnumerable<ApartmentDto> apartmentDtos, string sectionCode);
    string GenerateFlatCode(IEnumerable<FlatDto> flatDtos, string apartmentCode);
    string GenerateSectionCode(IEnumerable<SectionDto> sectionDtos);
    string GenerateUniqueKey(int size, char[] charArray);
}
