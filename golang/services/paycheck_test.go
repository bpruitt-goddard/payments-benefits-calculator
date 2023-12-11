package services

import (
	"example/server/types"

	"testing"
	"time"
)

func TestGetPayChecks(t *testing.T) {
	tests := map[string]struct {
		input           types.Employee
		expectedFirst25 int
		expectedLast    int
	}{
		"employee with no deductions": {
			input: types.Employee{
				Id:          1,
				FirstName:   "John",
				LastName:    "Doe",
				Salary:      50000.0,
				DateOfBirth: time.Now(),
				Dependents: []types.Dependent{
					{
						Id:           1,
						FirstName:    "Jane",
						LastName:     "Doe",
						DateOfBirth:  time.Now(),
						Relationship: types.Spouse,
					},
				},
			},
			expectedFirst25: 146153,
			expectedLast:    146175,
		},
	}

	for name, test := range tests {
		t.Parallel()
		t.Fatalf("get paychecks failed %q for employee %q", name, test.input.FirstName)
	}
}
