﻿using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DTOs;
using Dapper;

namespace BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;

public class DpMsEmployeeDal : IEmployeeDal
{
    private readonly DapperContext _context;

    public DpMsEmployeeDal(DapperContext context)
    {
        _context = context;
    }

    public long Add(EmployeeDto employeeDto)
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
        return connection.Query<long>(sql, employeeDto).Single();
    }

    public void Delete(long id)
    {
        using var connection = _context.CreateConnection();
        var sql = "DELETE FROM Employee"
            + " WHERE EmployeeId = @EmployeeId";
        connection.Execute(sql, new { @EmployeeId = id });
    }

    public EmployeeDto GetByAccountId(long accountId)
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
        return connection.Query<EmployeeDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public EmployeeDto GetByBusinessIdAndAccountId(int businessId, long accountId)
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
        return connection.Query<EmployeeDto>(sql, new
        {
            @BusinessId = businessId,
            @AccountId = accountId,
        }).SingleOrDefault();
    }

    public EmployeeDto GetById(long id)
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
        return connection.Query<EmployeeDto>(sql, new { @EmployeeId = id }).SingleOrDefault();
    }

    public EmployeeExtDto GetExtByAccountId(long accountId)
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
        return connection.Query<EmployeeExtDto>(sql, new { @AccountId = accountId }).SingleOrDefault();
    }

    public EmployeeExtDto GetExtById(long id)
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
        return connection.Query<EmployeeExtDto>(sql, new { @EmployeeId = id }).SingleOrDefault();
    }

    public IEnumerable<EmployeeExtDto> GetExtsByBusinessId(int businessId)
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
        return connection.Query<EmployeeExtDto>(sql, new { @BusinessId = businessId }).ToList();
    }

    public void Update(EmployeeDto employeeDto)
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
        connection.Execute(sql, employeeDto);
    }
}
