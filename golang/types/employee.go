package types

import "time"

type Employee struct {
	Id int
	FirstName string
	LastName string
	// Stored as cents eg 7900=79.00
	Salary int
	DateOfBirth  time.Time
	Dependents []Dependent
}