# Contact Management API

Hey fellow developers! This is the backend API for our Contact Management application, built with .NET Core 8. It provides endpoints for managing contacts, and we're currently using a local JSON file as a mock database.

## Setup Instructions

1. **Prerequisites**
   - Make sure you have [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed
   - Grab your favorite text editor or IDE (I'm partial to Visual Studio Code, but use what you like)

2. **Clone the Repository**
   ```
   git clone https://github.com/som-bee-int/ContactsAppAPI.git
   cd ContactsAppAPI
   ```

3. **Restore Dependencies**
   ```
   dotnet restore
   ```

4. **Configure CORS**
   - I've set up CORS to allow requests from `http://localhost:4200`. If your frontend's running on a different port, you'll need to update the CORS policy in `Program.cs`.

## Running the Application

1. **Start the API**
   ```
   dotnet run
   ```
   The API should fire up on `https://localhost:7206` and `http://localhost:5029` by default.

2. **Access Swagger UI**
   - Open your browser and head to `https://localhost:7206/swagger` to view and test the API endpoints.

## Design Decisions and Application Structure

### Framework
- I went with **ASP.NET Core 8** for its performance, cross-platform support, and rich ecosystem.

### Data Storage
- We're using a **local JSON file** as a mock database for simplicity. You'll find it named `contacts.json` in the project root.

### Models
- `Contact`: This represents a contact with properties like Id, FirstName, LastName, Email, and IsActive.

### Controllers
- `ContactsController`: This handles all the CRUD operations for contacts.

### Error Handling
- I've implemented global error handling middleware in `Program.cs`.
- It should return appropriate error responses to the frontend.

### CORS
- Currently configured to allow requests from the Angular frontend running on `http://localhost:4200`.

## API Endpoints

- GET `/api/contacts`: Retrieve all contacts
  - Supports searching with a query parameter: `/api/contacts?search=<term>`
  - Search is performed on FirstName, LastName, and Email fields
- GET `/api/contacts/{id}`: Retrieve a specific contact
- POST `/api/contacts`: Create a new contact
- PUT `/api/contacts/{id}`: Update an existing contact
- DELETE `/api/contacts/{id}`: Soft delete a contact (sets IsActive to false)

## Features

### Contact Retrieval and Search
The API provides a flexible way to retrieve contacts:
- Fetching all contacts: `GET /api/contacts`
- Searching contacts: `GET /api/contacts?search=<term>`
The search functionality:
- Is case-insensitive
- Matches partial strings in FirstName, LastName, and Email fields
- Only returns active contacts (where IsActive is true)
Example:
```
GET /api/contacts?search=john
```
This will return all active contacts where "john" appears in the FirstName, LastName, or Email.

### Validation
- I've used data annotations in the `Contact` model for basic validation.

### Swagger
- Integrated Swagger for easy API documentation and testing.

## Project Structure
```
ContactsAppAPI/
├── Controllers/
│   └── ContactsController.cs
├── Models/
│   └── Contact.cs
├── Program.cs
├── contacts.json
└── [Other project files]
```

## Scalability

### Current Design and Scalability Support
Our current design of the Contact Management API provides a foundation for scalability, but we'll need some modifications to handle a large number of contacts efficiently:

1. **JSON File Storage**: Works well for small-scale applications or prototyping, but it won't support concurrent access well and could become a bottleneck with a large number of contacts.

2. **In-Memory Operations**: We're likely loading all contacts into memory for operations like searching and filtering. This works for small datasets but will consume excessive memory with a large number of contacts.

3. **Pagination**: We haven't implemented pagination yet, which could lead to performance issues when retrieving all contacts in a large dataset.

### Recommendations for Scaling
To scale our application for a large number of contacts, we should consider:

1. **Database Migration**: Replace the JSON file with a scalable database system (e.g., SQL Server, PostgreSQL) to support efficient querying, indexing, and concurrent access.

2. **Implement Pagination**: Add pagination to the GET `/api/contacts` endpoint to limit the number of records returned in a single request.

3. **Caching**: Implement caching mechanisms (e.g., Redis) to store frequently accessed data and reduce database load.

4. **Asynchronous Operations**: Ensure all database operations are asynchronous to improve responsiveness and throughput.

5. **Search Optimization**: For large datasets, consider implementing full-text search capabilities (e.g., Elasticsearch) for efficient searching across contact fields.

6. **API Versioning**: Implement API versioning to allow for future improvements without breaking existing client integrations.

7. **Load Balancing**: Set up multiple instances of the API behind a load balancer to distribute incoming requests.

8. **Monitoring and Logging**: Implement comprehensive logging and monitoring to identify and address performance bottlenecks.

By implementing these changes, we can adapt our Contact Management API to efficiently handle a large number of contacts while maintaining good performance and responsiveness.

## Notes
- This API uses a simple file-based storage system. For production use, we'll need to implement a proper database system.
- I've included basic error handling and validation. We should enhance these for production scenarios.
- Remember to handle CORS appropriately when deploying to production.

That's it! If you have any questions or run into any issues, feel free to reach out. Happy coding!