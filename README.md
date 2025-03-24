# Entrance Exam Scheduling and Evaluation System (Backend)

This is the backend implementation of the Entrance Exam Scheduling and Evaluation System developed for Samar State University. Built with .NET 8, the system follows Clean Architecture principles and leverages CQRS, MediatR, JWT authentication with refresh tokens, and xUnit for testing. Admins can create exam schedules, and applicants can select their preferred schedules online. While the exam and interview are conducted in person, results are recorded in the system, automating score computation and determining pass/fail status.
## Key Features:
- **Schedule Management**: Admins can create, update, and delete exam schedules (date, time, venue, and slots).
- **Applicant Scheduling**: Applicants can select their preferred schedule based on availability.
- **Automated Scoring**: Admins can input exam and interview scores, and the system will automatically compute pass/fail results.
- **Secure Authentication**: The API uses **JWT** for secure authentication and **Refresh Tokens** for session management.
- **CQRS and MediatR**: The system follows the **CQRS** pattern with **MediatR** to separate command and query logic, ensuring a clean and scalable architecture.
- **Testing**: Unit and integration tests are implemented using **xUnit** with an **SQLite In-Memory** database for fast and isolated testing environments.
- **Frontend Not Included**: The frontend UI, built with **Blazor WebAssembly** and **MudBlazor**, is not included in this repository.

## Tech Stack:
- **Backend**: .NET 8 Web API
- **Frontend (Not Included)**: Blazor WebAssembly, MudBlazor (for UI components)
- **Database**: Microsoft SQL Server (MSSQL)
- **Architecture**: CQRS (Command Query Responsibility Segregation)
- **ORM**: Entity Framework Core
- **Mediator**: MediatR
- **Authentication**: JWT (JSON Web Tokens), Refresh Tokens
- **Testing**: xUnit, SQLite In-Memory

