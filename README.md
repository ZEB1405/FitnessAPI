# Fitness API

A REST API for tracking body weight and fitness-related progress.

This project is currently being developed as part of a developer course assignment focused on building REST APIs with ASP.NET Core Controllers. It is also intended to serve as the backend foundation for a future personal fitness tracking application.

## Current Status

The API currently supports creating, retrieving, sorting, and deleting weight entries.

The project uses ASP.NET Core Controllers, Entity Framework Core, and SQLite for persistent data storage.

## Features

* Create weight entries
* Retrieve all weight entries
* Retrieve a specific weight entry by ID
* Sort weight entries by recording date
* Delete weight entries
* Input validation
* Centralized error handling
* Persistent SQLite database
* Asynchronous database operations
* Swagger/OpenAPI documentation

## Tech Stack

* C#
* .NET 10
* ASP.NET Core
* Entity Framework Core
* SQLite
* Swagger / OpenAPI

## Getting Started

### Prerequisites

* .NET 10 SDK
* .NET Entity Framework Core CLI tools

### Run the project

Clone the repository and navigate to the project directory:

```bash
git clone <https://github.com/ZEB1405/FitnessAPI>
cd FitnessApi
```

Restore the dependencies:

```bash
dotnet restore
```

Apply the database migrations:

```bash
dotnet ef database update
```

Start the application:

```bash
dotnet run
```

The API can then be tested using Swagger at:

```text
http://localhost:<port>/swagger
```

The port may differ depending on the local development environment.

## Database

The application uses SQLite with Entity Framework Core.

The database is created from EF Core migrations. To apply existing migrations:

```bash
dotnet ef database update
```

To create a new migration after changing the model:

```bash
dotnet ef migrations add <MigrationName>
```

Then apply it with:

```bash
dotnet ef database update
```

## API Endpoints

| Method | Endpoint                  | Description                      |
| ------ | ------------------------- | -------------------------------- |
| GET    | `/api/WeightEntries`      | Retrieve all weight entries      |
| GET    | `/api/WeightEntries/{id}` | Retrieve a specific weight entry |
| POST   | `/api/WeightEntries`      | Create a new weight entry        |
| DELETE | `/api/WeightEntries/{id}` | Delete a weight entry            |

### Sorting

Weight entries can be sorted by their recording date using the `sort` query parameter.

```text
GET /api/WeightEntries?sort=asc
```

Returns the oldest entries first.

```text
GET /api/WeightEntries?sort=desc
```

Returns the newest entries first.

The parameter is case-insensitive.

Unsupported values return `400 Bad Request`.

## Example POST Request

```json
{
  "weight": 93.4,
  "recordedAt": "2026-09-19T08:00:00",
  "notes": "Morning weigh-in"
}
```

A successful request returns:

```text
201 Created
```

The response contains the newly created weight entry and a link to retrieve it by ID.

## Validation

Weight entries validate the `weight` property using data annotations.

Weight must be between `0` and `1000`.

Invalid input returns:

```text
400 Bad Request
```

## Error Handling

The API uses centralized exception handling for unexpected server errors.

Known client errors, such as invalid input or requesting a weight entry that does not exist, return appropriate HTTP status codes.

| Status Code | Meaning                          |
| ----------- | -------------------------------- |
| 200         | Request completed successfully   |
| 201         | Resource created successfully    |
| 204         | Resource deleted successfully    |
| 400         | Invalid request or input         |
| 404         | Requested resource was not found |
| 500         | Unexpected server error          |

## Testing

The API can be manually tested through Swagger/OpenAPI.

The following scenarios should be tested:

* Retrieve an empty collection
* Create a valid weight entry
* Retrieve the created entry
* Retrieve a nonexistent entry
* Create an entry with invalid weight
* Sort entries ascending and descending
* Provide an invalid sort value
* Delete an existing entry
* Attempt to delete a nonexistent entry

## Future Plans

The API is intended to become the backend for a larger fitness tracking application.

Potential future functionality includes:

* User accounts and authentication
* Weight history and progress tracking
* Nutrition tracking
* Training and workout tracking
* Progress statistics
* Additional filtering and pagination
* A dedicated frontend application
* PostgreSQL as a production database

The scope of these features may change as the project develops.
