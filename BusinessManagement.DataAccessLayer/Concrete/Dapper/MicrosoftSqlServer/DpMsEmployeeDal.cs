using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsEmployeeDal : IEmployeeDal
{
    private readonly DapperContext _context;

    public DpMsEmployeeDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(Employee employee)
    {
        using var connection = _context.CreateConnection();
        var sql = "INSERT INTO Employee ("
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " EmployeeTypeId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " IdentityNumber,"
            + " StillWorking,"
            + " StartDate, "
            + " QuitDate,"
            + " CreatedAt,"
            + " UpdatedAt)"
            + " VALUES("
            + " @BusinessId,"
            + " @BranchId,"
            + " @AccountId,"
            + " @EmployeeTypeId,"
            + " @NameSurname,"
            + " @Email,"
            + " @Phone,"
            + " @DateOfBirth,"
            + " @Gender,"
            + " @Notes,"
            + " @AvatarUrl,"
            + " @IdentityNumber,"
            + " @StillWorking,"
            + " @StartDate,"
            + " @QuitDate,"
            + " @CreatedAt,"
            + " @UpdatedAt)"
            + " SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        return connection.Query<long>(sql, employee).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Employee"
            + " WHERE EmployeeId = @EmployeeId";
        connection.Execute(sql, new { @EmployeeId = id });
    }

    public Employee GetByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " EmployeeId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " EmployeeTypeId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " IdentityNumber,"
            + " StillWorking,"
            + " StartDate, "
            + " QuitDate,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Employee"
            + " WHERE AccountId = @AccountId";
        return connection.Query<Employee>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public Employee GetByBusinessIdAndAccountId(int businessId, long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " EmployeeId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " EmployeeTypeId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " IdentityNumber,"
            + " StillWorking,"
            + " StartDate, "
            + " QuitDate,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Employee"
            + " WHERE BusinessId = @BusinessId AND AccountId = @AccountId";
        return connection.Query<Employee>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public Employee GetById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " EmployeeId,"
            + " BusinessId,"
            + " BranchId,"
            + " AccountId,"
            + " EmployeeTypeId,"
            + " NameSurname,"
            + " Email,"
            + " Phone,"
            + " DateOfBirth,"
            + " Gender,"
            + " Notes,"
            + " AvatarUrl,"
            + " IdentityNumber,"
            + " StillWorking,"
            + " StartDate, "
            + " QuitDate,"
            + " CreatedAt,"
            + " UpdatedAt"
            + " FROM Employee"
            + " WHERE EmployeeId = @EmployeeId";
        return connection.Query<Employee>(sql, new { @EmployeeId = id }).SingleOrDefault();
    }

    public EmployeeExt GetExtByAccountId(long accountId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " e.EmployeeId,"
            + " e.BusinessId,"
            + " e.BranchId,"
            + " e.AccountId,"
            + " e.EmployeeTypeId,"
            + " e.NameSurname,"
            + " e.Email,"
            + " e.Phone,"
            + " e.DateOfBirth,"
            + " e.Gender,"
            + " e.Notes,"
            + " e.AvatarUrl,"
            + " e.IdentityNumber,"
            + " e.StillWorking,"
            + " e.StartDate, "
            + " e.QuitDate,"
            + " e.CreatedAt,"
            + " e.UpdatedAt,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName,"
            + " et.EmployeeTypeName"
            + " FROM Employee e"
            + " INNER JOIN Account a ON e.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN EmployeeType et ON e.EmployeeTypeId = et.EmployeeTypeId"
            + " WHERE e.AccountId = @AccountId";
        return connection.Query<EmployeeExt>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public EmployeeExt GetExtById(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " e.EmployeeId,"
            + " e.BusinessId,"
            + " e.BranchId,"
            + " e.AccountId,"
            + " e.EmployeeTypeId,"
            + " e.NameSurname,"
            + " e.Email,"
            + " e.Phone,"
            + " e.DateOfBirth,"
            + " e.Gender,"
            + " e.Notes,"
            + " e.AvatarUrl,"
            + " e.IdentityNumber,"
            + " e.StillWorking,"
            + " e.StartDate, "
            + " e.QuitDate,"
            + " e.CreatedAt,"
            + " e.UpdatedAt,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName,"
            + " et.EmployeeTypeName"
            + " FROM Employee e"
            + " INNER JOIN Account a ON e.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN EmployeeType et ON e.EmployeeTypeId = et.EmployeeTypeId"
            + " WHERE e.EmployeeId = @EmployeeId";
        return connection.Query<EmployeeExt>(sql, new { @EmployeeId = id }).SingleOrDefault();
    }

    public IEnumerable<EmployeeExt> GetExtsByBusinessId(int businessId)
    {
        using var connection = _context.CreateConnection();
        var sql = "SELECT"
            + " e.EmployeeId,"
            + " e.BusinessId,"
            + " e.BranchId,"
            + " e.AccountId,"
            + " e.EmployeeTypeId,"
            + " e.NameSurname,"
            + " e.Email,"
            + " e.Phone,"
            + " e.DateOfBirth,"
            + " e.Gender,"
            + " e.Notes,"
            + " e.AvatarUrl,"
            + " e.IdentityNumber,"
            + " e.StillWorking,"
            + " e.StartDate, "
            + " e.QuitDate,"
            + " e.CreatedAt,"
            + " e.UpdatedAt,"
            + " a.AccountGroupId,"
            + " a.AccountOrder,"
            + " a.AccountName,"
            + " a.AccountCode,"
            + " a.Limit,"
            + " ag.AccountGroupName,"
            + " et.EmployeeTypeName"
            + " FROM Employee e"
            + " INNER JOIN Account a ON e.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN EmployeeType et ON e.EmployeeTypeId = et.EmployeeTypeId"
            + " WHERE e.BusinessId = @BusinessId";
        return connection.Query<EmployeeExt>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(Employee employee)
    {
        using var connection = _context.CreateConnection();
        var sql = "UPDATE Employee SET"
            + " BusinessId = @BusinessId,"
            + " BranchId = @BranchId,"
            + " AccountId = @AccountId,"
            + " EmployeeTypeId = @EmployeeTypeId,"
            + " NameSurname = @NameSurname,"
            + " Email = @Email,"
            + " Phone = @Phone,"
            + " DateOfBirth = @DateOfBirth,"
            + " Gender = @Gender,"
            + " Notes = @Notes,"
            + " AvatarUrl = @AvatarUrl,"
            + " IdentityNumber = @IdentityNumber,"
            + " StillWorking = @StillWorking,"
            + " StartDate = @StartDate,"
            + " QuitDate = @QuitDate,"
            + " CreatedAt = @CreatedAt,"
            + " UpdatedAt = @UpdatedAt"
            + " WHERE EmployeeId = @EmployeeId";
        connection.Execute(sql, employee);
    }
}
