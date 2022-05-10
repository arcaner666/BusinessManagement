using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsEmployeeDal : IEmployeeDal
{
    private readonly IDbConnection _db;

    public DpMsEmployeeDal(IConfiguration configuration)
    {
        _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }

    public Employee Add(Employee employee)
    {
        var sql = "INSERT INTO Employee (BusinessId, BranchId, AccountId, EmployeeTypeId, NameSurname, Email, Phone, DateOfBirth, Gender, Notes, AvatarUrl, IdentityNumber, StillWorking, StartDate, QuitDate, CreatedAt, UpdatedAt)"
            + " VALUES(@BusinessId, @BranchId, @AccountId, @EmployeeTypeId, @NameSurname, @Email, @Phone, @DateOfBirth, @Gender, @Notes, @AvatarUrl, @IdentityNumber, @StillWorking, @StartDate, @QuitDate, @CreatedAt, @UpdatedAt) SELECT CAST(SCOPE_IDENTITY() AS BIGINT)";
        var id = _db.Query<long>(sql, employee).Single();
        employee.EmployeeId = id;
        return employee;
    }

    public void Delete(long id)
    {
        var sql = "DELETE FROM Employee"
            + " WHERE EmployeeId = @EmployeeId";
        _db.Execute(sql, new { @EmployeeId = id });
    }

    public Employee GetByAccountId(long accountId)
    {
        var sql = "SELECT * FROM Employee"
            + " WHERE AccountId = @AccountId";
        return _db.Query<Employee>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public Employee GetByBusinessIdAndAccountId(int businessId, long accountId)
    {
        var sql = "SELECT * FROM Employee"
            + " WHERE BusinessId = @BusinessId AND AccountId = @AccountId";
        return _db.Query<Employee>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public Employee GetById(long id)
    {
        var sql = "SELECT * FROM Employee"
            + " WHERE EmployeeId = @EmployeeId";
        return _db.Query<Employee>(sql, new { @EmployeeId = id }).SingleOrDefault();
    }

    public Employee GetExtByAccountId(long accountId)
    {
        var sql = "SELECT * FROM Employee e"
            + " INNER JOIN Account a ON e.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN EmployeeType et ON e.EmployeeTypeId = et.EmployeeTypeId"
            + " WHERE e.AccountId = @AccountId";
        return _db.Query<Employee, Account, AccountGroup, EmployeeType, Employee>(sql,
            (employee, account, accountGroup, employeeType) =>
            {
                employee.Account = account;
                employee.Account.AccountGroup = accountGroup;
                employee.EmployeeType = employeeType;
                return employee;
            }, new { @AccountId = accountId },
            splitOn: "AccountId,AccountGroupId,EmployeeTypeId").SingleOrDefault();
    }

    public Employee GetExtById(long id)
    {
        var sql = "SELECT * FROM Employee e"
            + " INNER JOIN Account a ON e.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN EmployeeType et ON e.EmployeeTypeId = et.EmployeeTypeId"
            + " WHERE e.EmployeeId = @EmployeeId";
        return _db.Query<Employee, Account, AccountGroup, EmployeeType, Employee>(sql,
            (employee, account, accountGroup, employeeType) =>
            {
                employee.Account = account;
                employee.Account.AccountGroup = accountGroup;
                employee.EmployeeType = employeeType;
                return employee;
            }, new { @EmployeeId = id },
            splitOn: "AccountId,AccountGroupId,EmployeeTypeId").SingleOrDefault();
    }

    public List<Employee> GetExtsByBusinessId(int businessId)
    {
        var sql = "SELECT * FROM Employee e"
            + " INNER JOIN Account a ON e.AccountId = a.AccountId"
            + " INNER JOIN AccountGroup ag ON a.AccountGroupId = ag.AccountGroupId"
            + " INNER JOIN EmployeeType et ON e.EmployeeTypeId = et.EmployeeTypeId"
            + " WHERE e.BusinessId = @BusinessId";
        return _db.Query<Employee, Account, AccountGroup, EmployeeType, Employee>(sql,
            (employee, account, accountGroup, employeeType) =>
            {
                employee.Account = account;
                employee.Account.AccountGroup = accountGroup;
                employee.EmployeeType = employeeType;
                return employee;
            }, new { @BusinessId = businessId },
            splitOn: "AccountId,AccountGroupId,EmployeeTypeId").ToList();
    }

    public void Update(Employee employee)
    {
        var sql = "UPDATE Employee SET BusinessId = @BusinessId, BranchId = @BranchId, AccountId = @AccountId, EmployeeTypeId = @EmployeeTypeId, NameSurname = @NameSurname, Email = @Email, Phone = @Phone, DateOfBirth = @DateOfBirth, Gender = @Gender, Notes = @Notes, AvatarUrl = @AvatarUrl, IdentityNumber = @IdentityNumber, StillWorking = @StillWorking, StartDate = @StartDate, QuitDate = @QuitDate, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt"
            + " WHERE EmployeeId = @EmployeeId";
        _db.Execute(sql, employee);
    }
}
