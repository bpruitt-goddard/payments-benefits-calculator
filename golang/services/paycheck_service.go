package services

import (
	"example/server/types"
)

func GetPaychecks(employee types.Employee) []types.Paycheck{
	// create initial deduction
	deductions := 1200000
	// create 
	yearlyTotal := employee.Salary - deductions
	
	return splitEvenly(yearlyTotal)
}

// split payments evenly
func splitEvenly(amount int) []types.Paycheck{
	paychecksPerYear := 26
	split := amount / paychecksPerYear
	remainder := amount - (split * paychecksPerYear)
	paychecks := make([]types.Paycheck, paychecksPerYear - 1)
	for i := 0; i < paychecksPerYear - 1; i++ {
		paychecks[i] = types.Paycheck{Amount: split}
	}
	paychecks = append(paychecks, types.Paycheck{Amount: split + remainder})
	return paychecks
}