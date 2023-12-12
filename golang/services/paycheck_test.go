package services

import (
	"example/server/types"
	"slices"

	"testing"
	"time"
)

func TestGetPayChecks(t *testing.T) {
	tests := map[string]struct {
		input           types.Employee
		expectedFirst25 int
		expectedLast    int
	}{
		"employee with no extra deductions": {
			input: types.Employee{
				Salary:      50_000_00,
				DateOfBirth: time.Now().AddDate(-30, 0, 0),
			},
			expectedFirst25: 1_461_53,
			expectedLast:    1_461_75,
		},
		"employee with one dependent": {
			input: types.Employee{
				Salary:      50_000_00,
				DateOfBirth: time.Now().AddDate(-30, 0, 0),
				Dependents:  []types.Dependent{{}},
			},
			expectedFirst25: 1_184_61,
			expectedLast:    1_184_75,
		},
		"employee with two dependents": {
			input: types.Employee{
				Salary:      50_000_00,
				DateOfBirth: time.Now().AddDate(-30, 0, 0),
				Dependents:  []types.Dependent{{},{}},
			},
			expectedFirst25: 907_69,
			expectedLast:    907_75,
		},
	}

	for name, test := range tests {
		expected := repeatedSlice(test.expectedFirst25, 25)
		expected = append(expected, types.Paycheck{Amount: test.expectedLast})
		if got := GetPaychecks(test.input); !slices.Equal(expected, got) {
			t.Fatalf("get paychecks (%q) returned %v\n expected %v", name, got, expected)
		}
	}
}

func repeatedSlice(value, n int) []types.Paycheck {
	arr := make([]types.Paycheck, n)
	for i := 0; i < n; i++ {
		arr[i] = types.Paycheck{Amount: value}
	}
	return arr
}
