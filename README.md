# SampleApi
SampleApi is rest api written in .NET Core.  

Project is divided into several layers:

## Sample.Api
Presentation layer. Includes Controllers, DTOs
#### DTO - *data transfer object*
Classes using for input or output in controllers. For its filling from Domain entities (loaded by application or infrastructure layer) is used NuGet **AutoMapper**.
## Sample.Application
Application layer. Contains business logic.
## Sample.Domain
Core of application. It contains Entities (Base application data classes).
It contains plain objects and logic that is applicable in general to the whole entity.. 
## Sample.Infrastructure
- Retrieve and store data from and to a number of sources (database, network devices, file system, 3rd parties, and so on.  
- Define interfaces for the data that they need in order to apply some logic. One or more data providers will implement the interface, but the use case doesn’t know where the data is coming from.
- Implements this interfaces (Repositories)
### Database
The application uses ORM (Object Relational Mapping) **EntityFramework** for accessing and managing database. Database model and data init seed to tables is provided by EntityFramework migrations. 
## Testing
For testing is used xUnit. 
#### Arrange
For data mocking is used NuGet **Moq**.
#### Assert
For asserting is used NuGet **FluentAssertions**.

## Setup and start the application

### Creating database model and seed initial data
From a Package Manager Console in Visual Studio call command:  
<Code>Update-Database</Code>  
Default project must be set to **Sample.Infrastructure**.
