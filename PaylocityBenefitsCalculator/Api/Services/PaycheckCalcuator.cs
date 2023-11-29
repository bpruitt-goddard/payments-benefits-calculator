using Api.Models;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Api.Services;

public static class PaycheckCalculator
{
	private const int PaychecksPerYear = 26;
	private const decimal YearlyBenefitsBaseCost = 12_000m;
	// Return a years worth of paychecks
	public static List<Paycheck> GetPaychecks(Employee employee)
	{
		// calculate total deductions
		var deductions = YearlyBenefitsBaseCost;


		var yearlyTotal = employee.Salary - deductions;
		return SplitEvenly(yearlyTotal);
	}

	// We need to divide the total as evenly as possible between paychecks
	// For cents, add to final paycheck for simplicity
	private static List<Paycheck> SplitEvenly(decimal amount)
	{
		// split down to cents, do not extend decimal
		const int precision = 2;
		decimal scale = (decimal)Math.Pow(10, precision);
		decimal split = Math.Truncate((amount / PaychecksPerYear) * scale) / scale;
		List<decimal> output = Enumerable.Repeat(split, PaychecksPerYear).ToList();
		decimal remainder = amount - output.Sum();
		output[^1] += remainder;

		return output.Select(o => new Paycheck { Amount = o })
			.ToList();
	}
}