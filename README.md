# InOutBoxPattern

This project demonstrates the implementation of the Inbox and Outbox patterns using .NET 9.0, Entity Framework Core, and Kafka.

## Project Structure

- **InboxService**: Handles incoming requests and stores them in the Inbox database.
- **OutboxService**: Processes events from the Inbox and stores them in the Outbox database.
- **Shared**: Contains shared contracts, constants, and middlewares used by both services.

## Prerequisites

- .NET 9.0 SDK
- PostgreSQL
- Kafka

## Getting Started

### Setting Up the Database

1. Update the connection strings in `appsettings.json` for both `InboxService` and `OutboxService` to point to your PostgreSQL instance.

### Running the Services

1. Navigate to the `InboxService` directory and run the following commands:
    ```sh
    dotnet restore
    dotnet ef database update
    dotnet run
    ```

2. Navigate to the [OutboxService](http://_vscodecontentref_/0) directory and run the following commands:
    ```sh
    dotnet restore
    dotnet ef database update
    dotnet run
    ```

### API Endpoints

#### InboxService

- **GET /api/Users**: Retrieve all users.
- **GET /api/Users/{userId}**: Retrieve a specific user by ID.
- **POST /api/Users**: Add a new user.
- **PUT /api/Users/{userId}**: Update an existing user.

#### OutboxService

- **GET /api/Patients**: Retrieve all patients.

### Error Handling

Both services use a custom error handling middleware to return consistent error responses.

### Event Handling

The [InboxService](http://_vscodecontentref_/1) publishes events to Kafka, which are then consumed by the [OutboxService](http://_vscodecontentref_/2).

## License

This project is licensed under the MIT License.