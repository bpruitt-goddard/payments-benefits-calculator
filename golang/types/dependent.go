package types

import "time"

type Dependent struct {
	Id int
	FirstName string
	LastName string
	DateOfBirth time.Time
	Relationship Relationship
}

type Relationship int

const (
	None Relationship = iota
	Spouse
	DomesticPartner
	Child
)