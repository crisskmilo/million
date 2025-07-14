# million

This application allows to manage the information of some properties in the United States.

The architecture of this project is based on Clean Architecture and Onion in order to clearly separate the responsibilities and dependencies between the different layers of an application and save the core and the business logic allowing the system to be more flexible, less coupled as well as maintainable over time, understandable and scalable. It uses some design patterns such as singleton, inversion of control, repository, factory, wrapper, among others.

Project Structure
1.Domain (Entities/core): Define the main entities and their relationships, as well as use cases and general and little changing logic.
2.Application (Use Cases/core): Contains the application logic and interfaces, as well as more specific and changing use cases and logic.
3.Infrastructure: Implements persistence and other services as a dependency container.
4.WebApi (Presentation): Contains the controllers and exposes the functionalities.
5.Test: Contains tests on the services.

![million01](https://github.com/user-attachments/assets/5232018d-3d31-4c8b-b4bb-f54ee12e2b34)


This project uses Entity Framework with a code-first approach; to generate the database for the first time or run a migration
it must be positioned in the project 3.Infraestructure\Million.Infra.Data, also you should add data.

To create the migration run
dotnet ef migrations add InitialCreate --verbose --project 2.Infraestructure\Million.Infra.Data --startup-project 4.WebApi\Million.WebApi

To run the migration run
Updating the database dotnet ef --verbose --project 3.Infraestructure\Million.Infra.Data --startup-project 4.WebApi\Million.WebApi

A user must be created to allow authentication in the system and obtain a token to invoke the other functionalities.

![MillionDb](https://github.com/user-attachments/assets/47e0bd17-e951-42c5-97dc-6db007019f35)


Objects are: Owner, Property, Property Image, Property Trace

Main functionalities are: Create Property Building, Add Image from property, Change Price, Update property, List property with filters
