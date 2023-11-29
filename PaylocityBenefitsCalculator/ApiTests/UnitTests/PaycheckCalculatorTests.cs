using System;
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
			DateOfBirth = DateTime.UtcNow.AddYears(-30)
		};

		var paychecks = PaycheckCalculator.GetPaychecks(employee);

		Assert.Equal(26, paychecks.Count);
		Assert.All(paychecks.Take(25), paycheck => Assert.Equal(1923.07m, paycheck.Amount));
		Assert.Equal(1923.25m, paychecks.Last().Amount);
	}
}