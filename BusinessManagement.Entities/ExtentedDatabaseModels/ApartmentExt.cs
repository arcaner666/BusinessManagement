namespace BusinessManagement.Entities.ExtendedDatabaseModels;

public class ApartmentExt
{
    public long ApartmentId { get; set; }
    public int SectionId { get; set; }
    public int BusinessId { get; set; }
    public long BranchId { get; set; }
    public long ManagerId { get; set; }
    public string ApartmentName { get; set; }
    public string ApartmentCode { get; set; }
    public int BlockNumber { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Extended With Section
    public string SectionName { get; set; }

    // Extended With Manager
    public string ManagerNameSurname { get; set; }
}
