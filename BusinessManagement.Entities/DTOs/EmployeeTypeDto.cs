namespace BusinessManagement.Entities.DTOs;

public record EmployeeTypeDto
{
    public short EmployeeTypeId { get; init; }
    public string EmployeeTypeName { get; init; }
}
