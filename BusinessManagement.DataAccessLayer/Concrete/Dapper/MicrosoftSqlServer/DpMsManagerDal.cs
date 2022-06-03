using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsManagerDal : IManagerDal
{
    private readonly DapperContext _context;

    public DpMsManagerDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(ManagerDto managerDto)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Manager ("
            + " BusinessId,"
            + " BranchId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @BusinessId,"
            + " @BranchId,"
            + " @NameSurname,"
            + " @Email,"
            + " @Phone,"
            + " @DateOfBirth,"
            + " @Gender,"
            + " @Notes,"
            + " @AvatarUrl,"
            + " @TaxOffice,"
            + " @TaxNumber,"
            + " @IdentityNumber,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, managerDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Manager"
            + " WHERE ManagerId = @ManagerId";
        connection.Execute(sql, new { @ManagerId = id });
    }

    public IEnumerable<ManagerDto> GetByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ManagerId,"
            + " BusinessId,"
            + " BranchId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Manager"
            + " WHERE BusinessId = @BusinessId";
        return connection.Query<ManagerDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public ManagerDto GetByBusinessIdAndPhone(int businessId, string phone)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ManagerId,"
            + " BusinessId,"
            + " BranchId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Manager"
            + " WHERE BusinessId = @BusinessId AND Phone = @Phone";
        return connection.Query<ManagerDto>(sql, new
        {
            @BusinessId = businessId,
            @Phone = phone,
        }).SingleOrDefault();
    }

    public ManagerDto GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " ManagerId,"
            + " BusinessId,"
            + " BranchId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " TaxOffice,"
            + " TaxNumber,"
            + " IdentityNumber,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Manager"
            + " WHERE ManagerId = @ManagerId";
        return connection.Query<ManagerDto>(sql, new { @ManagerId = id }).SingleOrDefault();
    }

    public IEnumerable<ManagerExtDto> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " m.ManagerId,"
            + " m.BusinessId,"
            + " m.BranchId,"
            + " m.NameSurname,"
            + " m.Email,"
            + " m.Phone,"
            + " m.DateOfBirth,"
            + " m.Gender,"
            + " m.Notes,"
            + " m.AvatarUrl,"
            + " m.TaxOffice,"
            + " m.TaxNumber,"
            + " m.IdentityNumber,"
            + " m.CreatedAt,"
            + " m.UpdatedAt,"
            + " bu.BusinessName,"
            + " fa.CityId,"
            + " fa.DistrictId,"
            + " fa.AddressText"
            + " FROM Manager m"
            + " INNER JOIN Business bu ON m.BusinessId = bu.BusinessId"
            + " INNER JOIN Branch br ON m.BranchId = br.BranchId"
            + " INNER JOIN FullAddress fa ON br.FullAddressId = fa.FullAddressId"
            + " WHERE m.BusinessId = @BusinessId";
        return connection.Query<ManagerExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(ManagerDto managerDto)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Manager SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " NameSurname = @NameSurname,"
            + " Email = @Email,"
            + " Phone = @Phone,"
            + " DateOfBirth = @DateOfBirth,"
            + " Gender = @Gender,"
            + " Notes = @Notes,"
            + " AvatarUrl = @AvatarUrl,"
            + " TaxOffice = @TaxOffice,"
            + " TaxNumber = @TaxNumber,"
            + " IdentityNumber = @IdentityNumber,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE ManagerId = @ManagerId";
        connection.Execute(sql, managerDto);
    }
}
