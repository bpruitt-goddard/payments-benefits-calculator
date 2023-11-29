using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Api.Services;
using Xunit;

namespace ApiTests.UnitTests;

public class PaycheckCalculatorTests
{
	[Fact]
	public void EmployeeWithNoExtraDeductions_ReturnsCorrectPaychecks()
	{
		var employee = new Employee
		{
			Salary = 50_000.00m,
			DateOfBirth = DateTime.UtcNow.AddYears(-30),
		};

		var paychecks = PaycheckCalculator.GetPaychecks(employee);

		Assert.Equal(26, paychecks.Count);
		Assert.All(paychecks.Take(25), paycheck => Assert.Equal(1461.53m, paycheck.Amount));
		Assert.Equal(1461.75m, paychecks.Last().Amount);
		Assert.Equal(38_000m, paychecks.Sum(p => p.Amount));
	}

	[Fact]
	public void EmployeeWithOneDependent_ReturnsPaychecksWithDependentDeduction()
	{
		var employee = new Employee
		{
			Salary = 50_000.00m,
			DateOfBirth = DateTime.UtcNow.AddYears(-30),
			Dependents = new List<Dependent> { new() },
		};

		var paychecks = PaycheckCalculator.GetPaychecks(employee);

		Assert.Equal(26, paychecks.Count);
		Assert.All(paychecks.Take(25), paycheck => Assert.Equal(1184.61m, paycheck.Amount));
		Assert.Equal(1184.75m, paychecks.Last().Amount);
		Assert.Equal(30_800m, paychecks.Sum(p => p.Amount));
	}

	[Fact]
	public void EmployeeWithTwoDependents_ReturnsPaychecksWithDependentDeduction()
	{
		var employee = new Employee
		{
			Salary = 50_000.00m,
			DateOfBirth = DateTime.UtcNow.AddYears(-30),
			Dependents = new List<Dependent> { new(), new() },
		};

		var paychecks = PaycheckCalculator.GetPaychecks(employee);

		Assert.Equal(26, paychecks.Count);
		Assert.All(paychecks.Take(25), paycheck => Assert.Equal(907.69m, paycheck.Amount));
		Assert.Equal(907.75m, paychecks.Last().Amount);
		Assert.Equal(23_600m, paychecks.Sum(p => p.Amount));
	}

	[Fact]
	public void EmployeeWithHighSalary_ReturnsPaychecksWithSalaryDeduction()
	{
		var employee = new Employee
		{
			Salary = 80_000.01m,
			DateOfBirth = DateTime.UtcNow.AddYears(-30),
		};

		var paychecks = PaycheckCalculator.GetPaychecks(employee);

		Assert.Equal(26, paychecks.Count);
		Assert.All(paychecks.Take(25), paycheck => Assert.Equal(2553.84m, paycheck.Amount));
		Assert.Equal(2554.01m, paychecks.Last().Amount);
		Assert.Equal(66_400.01m, paychecks.Sum(p => p.Amount));
	}

	[Fact]
	public void EmployeeWithOldAge_ReturnsPaychecksWithExtraDeduction()
	{
		var employee = new Employee
		{
			Salary = 50_000.00m,
			DateOfBirth = DateTime.UtcNow.AddYears(-51),
		};

		var paychecks = PaycheckCalculator.GetPaychecks(employee);

		Assert.Equal(26, paychecks.Count);
		Assert.All(paychecks.Take(25), paycheck => Assert.Equal(1369.23m, paycheck.Amount));
		Assert.Equal(1369.25m, paychecks.Last().Amount);
		Assert.Equal(35_600m, paychecks.Sum(p => p.Amount));
	}

	[Fact]
	public void EmployeeWithAllDeductions_ReturnsPaychecksWithCumulativeDeductions()
	{
		var employee = new Employee
		{
			Salary = 100_000.00m,
			DateOfBirth = DateTime.UtcNow.AddYears(-51),
			Dependents = new List<Dependent> { new() }
		};

		var paychecks = PaycheckCalculator.GetPaychecks(employee);

		Assert.Equal(26, paychecks.Count);
		Assert.All(paychecks.Take(25), paycheck => Assert.Equal(2938.46m, paycheck.Amount));
		Assert.Equal(2938.50m, paychecks.Last().Amount);
		Assert.Equal(76_400m, paychecks.Sum(p => p.Amount));
	}
}