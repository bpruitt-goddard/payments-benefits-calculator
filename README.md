# What is this?

A project seed for a C# dotnet API ("PaylocityBenefitsCalculator").  It is meant to get you started on the Paylocity BackEnd Coding Challenge by taking some initial setup decisions away.

The goal is to respect your time, avoid live coding, and get a sense for how you work.

# Coding Challenge

**Show us how you work.**

Each of our Paylocity product teams operates like a small startup, empowered to deliver business value in
whatever way they see fit. Because our teams are close knit and fast moving it is imperative that you are able
to work collaboratively with your fellow developers. 

This coding challenge is designed to allow you to demonstrate your abilities and discuss your approach to
design and implementation with your potential colleagues. You are free to use whatever technologies you
prefer but please be prepared to discuss the choices you’ve made. We encourage you to focus on creating a
logical and functional solution rather than one that is completely polished and ready for production.

The challenge can be used as a canvas to capture your strengths in addition to reflecting your overall coding
standards and approach. There’s no right or wrong answer.  It’s more about how you think through the
problem. We’re looking to see your skills in all three tiers so the solution can be used as a conversation piece
to show our teams your abilities across the board.

Requirements will be given separately.

# Running
To run locally, simply run `dotnet run`. As the database is in-memory, there are no external requirements.

# Decisions

## Employee Data

### Database
The data was moved into a database to centralize it. It is using an in-memory database for now for speed and simplicity. This avoids database migrations and extra external dependencies. Because it is using EntityFramework, it is easy to change the database away from an in-memory to something else (remote, containerized, etc).

### Seeding Data
The seed project started with the data hard-coded in the employee controller. This was pulled out but placed within a seed script that runs during project startup. This made it easier to test. If this data is desired to seed all databases, it can be moved to a database migration file once the database is moved from in-memory. This would also fix the (potential) performance problem of the seeding process slowing down the app's startup if the seed script we to grow larger.

## Employee Validation
Currently there is no validation on employees. Part of the requirements state limitations on spouse/dependents. This has been deferred to future work as we do not currently have a way to create new employees, which could help drive where this validation occurs and if additional validation is needed (such as around date of birth).

## API Response Structure
The structure of the API responses uses a generic `ApiResponse` model. This was not leveraged in the error responses currently. This is left to a future change. Additionally, the `Success` boolean property on it should be removed as it goes against Http standards. Instead, the response status code should indicate this, with 2xx being a success and all others being failures.

## Paycheck Calculation
The requirements do not state how a user would query for a paycheck, or whether the 26 yearly paychecks occur on certain days. Thus, the calculation was simplified to return the 26 paychecks for a year. This can be extended to return a subset of paychecks or single paycheck.