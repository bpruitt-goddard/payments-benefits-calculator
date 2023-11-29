using Api.Dtos.Dependent;
using EmployeeModel = Api.Models.Employee;

namespace Api.Dtos.Employee;

public class GetEmployeeDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateOfBirth { get; set; }
    public ICollection<GetDependentDto> Dependents { get; set; } = new List<GetDependentDto>();

    public GetEmployeeDto() {}

    public GetEmployeeDto(EmployeeModel employee)
    {
        Id = employee.Id;
        FirstName = employee.FirstName;
        LastName = employee.LastName;
        Salary = employee.Salary;
        DateOfBirth = employee.DateOfBirth;
        Dependents = employee.Dependents.Select(d => new GetDependentDto(d)).ToList();
    }
}
