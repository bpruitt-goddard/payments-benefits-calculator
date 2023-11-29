using Api.Data;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeDbContext _context;

    public EmployeesController(EmployeeDbContext context)
    {
        _context = context;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = _context.Employees
            .Include(e => e.Dependents)
            .FirstOrDefault(employee => employee.Id == id);

        if (employee is null)
        {
            return NotFound();
        }

        return new ApiResponse<GetEmployeeDto>
        {
            Data = new GetEmployeeDto(employee),
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        // solution: replaced with database
        var employees = _context.Employees
            .Include(e => e.Dependents)
            .Select(e => new GetEmployeeDto(e))
            .ToList();

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees,
            Success = true
        };

        return result;
    }
}
