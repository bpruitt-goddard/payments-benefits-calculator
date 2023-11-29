using PaycheckModel = Api.Models.Paycheck;

namespace Api.Dtos.Paycheck;

public class GetPaycheckDto
{
	public decimal Amount { get; set; }

	public GetPaycheckDto() { }

	public GetPaycheckDto(PaycheckModel paycheck)
	{
		Amount = paycheck.Amount;
	}
}
