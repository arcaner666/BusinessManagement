using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;

public interface IKeyService
{
    string GenerateApartmentCode(List<ApartmentDto> apartmentDtos, string sectionCode);
    string GenerateFlatCode(List<FlatDto> flatDtos, string apartmentCode);
    string GenerateSectionCode(List<SectionDto> sectionDtos);
    string GenerateUniqueKey(int size, char[] charArray);
}
