using Api.Models;

namespace Api.Services;

public static class PaycheckCalculator
{
	private const int PaychecksPerYear = 26;
	private const decimal YearlyBenefitsBaseCost = 12_000m;
	private const decimal YearlyDependentCost = 7_200m;
	private const decimal HighSalaryMinimumExclusive = 80_000m;
	private const decimal HighSalaryPercentageCost = .02m;
	// how are you still playing basketball?
	private const int OldAgeMinimumYearsExclusive = 50;
	private const decimal YearlyOldAgeCost = 2_400m;

	// Return a years worth of paychecks
	public static List<Paycheck> GetPaychecks(Employee employee)
	{
		// calculate total deductions
		var deductions = YearlyBenefitsBaseCost;
		deductions += employee.Dependents.Count * YearlyDependentCost;

		if (employee.Salary > HighSalaryMinimumExclusive)
		{
			var deductionPercentage = employee.Salary * HighSalaryPercentageCost;
			// Round calculation to avoid fractional cents
			deductions += Math.Round(deductionPercentage, 2);
		}

		// Re-calculate on each call of method to ensure
		// we don't miss folks that just passed threshold
		var oldAgeDateOfBirth = DateTime.UtcNow.AddYears(-OldAgeMinimumYearsExclusive);
		if (employee.DateOfBirth < oldAgeDateOfBirth)
		{
			deductions += YearlyOldAgeCost;
		}

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