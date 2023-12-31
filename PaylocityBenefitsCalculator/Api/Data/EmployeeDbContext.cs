using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class EmployeeDbContext : DbContext
{
	public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
	{
	}

    public DbSet<Employee> Employees { get; set; }
	public DbSet<Dependent> Dependents { get; set; }
}