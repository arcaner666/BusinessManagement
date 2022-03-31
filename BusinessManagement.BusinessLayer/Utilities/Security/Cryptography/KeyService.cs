using BusinessManagement.Entities.DatabaseModels;
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

    public string GenerateApartmentCode(List<Apartment> apartments, string sectionCode)
    {
        string apartmentCode = GenerateUniqueKey(2, secureUppercaseLettersAndNumbers);
        if (apartments != null)
        {
            while (SearchApartmentWithSameCode(apartments, apartmentCode))
            {
                apartmentCode = GenerateUniqueKey(2, secureUppercaseLettersAndNumbers);
            }
        }
        return $"{sectionCode}{apartmentCode}";
    }

    public string GenerateFlatCode(List<Flat> flats, string apartmentCode)
    {
        string flatCode = GenerateUniqueKey(2, secureUppercaseLettersAndNumbers);
        if (flats != null)
        {
            while (SearchFlatWithSameCode(flats, flatCode))
            {
                flatCode = GenerateUniqueKey(2, secureUppercaseLettersAndNumbers);
            }
        }
        return $"{apartmentCode}{flatCode}";
    }

    public string GenerateSectionCode(List<Section> sections)
    {
        string sectionCode = GenerateUniqueKey(4, secureUppercaseLettersAndNumbers);
        if (sections != null)
        {
            while (SearchSectionWithSameCode(sections, sectionCode))
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

    private bool SearchApartmentWithSameCode(List<Apartment> apartments, string apartmentCode)
    {
        bool isExists = false;
        foreach (Apartment apartment in apartments)
        {
            if (apartment.ApartmentCode == apartmentCode)
            {
                isExists = true;
                break;
            }
            isExists = false;
        }
        return isExists;
    }

    private bool SearchFlatWithSameCode(List<Flat> flats, string flatCode)
    {
        bool isExists = false;
        foreach (Flat flat in flats)
        {
            if (flat.FlatCode == flatCode)
            {
                isExists = true;
                break;
            }
            isExists = false;
        }
        return isExists;
    }

    private bool SearchSectionWithSameCode(List<Section> sections, string sectionCode)
    {
        bool isExists = false;
        foreach (Section section in sections)
        {
            if (section.SectionCode == sectionCode)
            {
                isExists = true;
                break;
            }
            isExists = false;
        }
        return isExists;
    }
}

