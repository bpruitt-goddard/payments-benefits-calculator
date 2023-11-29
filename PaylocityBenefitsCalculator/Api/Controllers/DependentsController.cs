using Api.Data;
using Api.Dtos.Dependent;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly EmployeeDbContext _context;

    public DependentsController(EmployeeDbContext context)
    {
        _context = context;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = _context.Dependents
            .FirstOrDefault(d => d.Id == id);
            
        if (dependent is null)
        {
            return NotFound();
        }

        return new ApiResponse<GetDependentDto>
        {
            Data = new GetDependentDto(dependent),
            Success = true
        };
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = _context.Dependents
            .Select(d => new GetDependentDto(d))
            .ToList();

        return new ApiResponse<List<GetDependentDto>>
        {
            Data = dependents,
            Success = true
        };
    }
}
