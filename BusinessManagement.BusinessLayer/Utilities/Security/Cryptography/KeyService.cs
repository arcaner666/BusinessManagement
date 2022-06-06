using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;

public class KeyService : IKeyService
{
    private readonly char[] alphanumericalCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    private readonly char[] uppercaseLettersAndNumbers =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

    private readonly char[] secureUppercaseLettersAndNumbers =
        "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

    private readonly char[] testCharacters =
        "123456789".ToCharArray();

    public string GenerateApartmentCode(List<ApartmentDto> apartmentDtos, string sectionCode)
    {
        string apartmentCode = GenerateUniqueKey(2, secureUppercaseLettersAndNumbers);
        if (apartmentDtos is not null)
        {
            while (SearchApartmentWithSameCode(apartmentDtos, apartmentCode))
            {
                apartmentCode = GenerateUniqueKey(2, secureUppercaseLettersAndNumbers);
            }
        }
        return $"{sectionCode}{apartmentCode}";
    }

    public string GenerateFlatCode(List<FlatDto> flatDtos, string apartmentCode)
    {
        string flatCode = GenerateUniqueKey(2, secureUppercaseLettersAndNumbers);
        if (flatDtos is not null)
        {
            while (SearchFlatWithSameCode(flatDtos, flatCode))
            {
                flatCode = GenerateUniqueKey(2, secureUppercaseLettersAndNumbers);
            }
        }
        return $"{apartmentCode}{flatCode}";
    }

    public string GenerateSectionCode(List<SectionDto> sectionDtos)
    {
        string sectionCode = GenerateUniqueKey(4, secureUppercaseLettersAndNumbers);
        if (sectionDtos is not null)
        {
            while (SearchSectionWithSameCode(sectionDtos, sectionCode))
            {
                sectionCode = GenerateUniqueKey(4, secureUppercaseLettersAndNumbers);
            }
        }
        return sectionCode;
    }

    public string GenerateUniqueKey(int size, char[] charArray)
    {
        byte[] data = new byte[4 * size];
        using RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetBytes(data);
        StringBuilder result = new StringBuilder(size);
        for (int i = 0; i < size; i++)
        {
            var rnd = BitConverter.ToUInt32(data, i * 4);
            var idx = rnd % charArray.Length;

            result.Append(charArray[idx]);
        }

        return result.ToString();
    }

    private bool SearchApartmentWithSameCode(List<ApartmentDto> apartmentDtos, string apartmentCode)
    {
        bool isExists = false;
        foreach (ApartmentDto apartmentDto in apartmentDtos)
        {
            if (apartmentDto.ApartmentCode == apartmentCode)
            {
                isExists = true;
                break;
            }
            isExists = false;
        }
        return isExists;
    }

    private bool SearchFlatWithSameCode(List<FlatDto> flatDtos, string flatCode)
    {
        bool isExists = false;
        foreach (FlatDto flatDto in flatDtos)
        {
            if (flatDto.FlatCode == flatCode)
            {
                isExists = true;
                break;
            }
            isExists = false;
        }
        return isExists;
    }

    private bool SearchSectionWithSameCode(List<SectionDto> sectionDtos, string sectionCode)
    {
        bool isExists = false;
        foreach (SectionDto sectionDto in sectionDtos)
        {
            if (sectionDto.SectionCode == sectionCode)
            {
                isExists = true;
                break;
            }
            isExists = false;
        }
        return isExists;
    }
}

