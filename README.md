# Contact Management API

This is the backend API for a Contact Management application, built with .NET Core 8. It provides endpoints for managing contacts, using a local JSON file as a mock database.

## Setup Instructions

1. **Prerequisites**
   - Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
   - Ensure you have a text editor or IDE (e.g., Visual Studio, Visual Studio Code)

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
   - The API is configured to allow requests from `http://localhost:4200`. If your frontend runs on a different port, update the CORS policy in `Program.cs`.

## Running the Application

1. **Start the API**
   ```
   dotnet run
   ```
   The API will start running on `https://localhost:7206` and `http://localhost:5029` by default.

2. **Access Swagger UI**
   - Open a browser and navigate to `https://localhost:7206/swagger` to view and test the API endpoints.

## Design Decisions and Application Structure

### Framework
- **ASP.NET Core 8**: Chosen for its performance, cross-platform support, and rich ecosystem.

### Data Storage
- **Local JSON File**: Used as a mock database for simplicity. File is named `contacts.json` in the project root.

### Models
- `Contact`: Represents a contact with properties like Id, FirstName, LastName, Email, and IsActive.

### Controllers
- `ContactsController`: Handles CRUD operations for contacts.

### Error Handling
- Global error handling middleware implemented in `Program.cs`.
- Returns appropriate error responses to the frontend.

### CORS
- Configured to allow requests from the Angular frontend running on `http://localhost:4200`.



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
- Data annotations used in the `Contact` model for basic validation.

### Swagger
- Integrated for easy API documentation and testing.

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

## Notes
- This API uses a simple file-based storage system. For production use, consider implementing a proper database system.
- The application includes basic error handling and validation. Enhance these for production scenarios.
- Ensure to handle CORS appropriately when deploying to production.