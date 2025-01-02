Setup Instructions
Prerequisites
Before running the project, make sure you have the following installed:

.NET 8 SDK - for the backend API.
Node.js (>=14.x) - for the frontend Angular project.
SQL Server - for database management.
Angular CLI - for Angular project development.
Backend Setup
Clone the repository:

bash
Copy code
git clone https://github.com/your-repo/ecommerce-management-system.git
Open the backend project in Visual Studio or your preferred IDE.

Restore the NuGet packages:

bash
Copy code
dotnet restore
Set up the database connection string in appsettings.json:

json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "Server=your-server-name;Database=your-database-name;User Id=your-username;Password=your-password;"
}
Apply database migrations:

bash
Copy code
dotnet ef database update
Run the backend API:

bash
Copy code
dotnet run
The API should be accessible at http://localhost:5000.

Frontend Setup
Navigate to the frontend directory:

bash
Copy code
cd frontend
Install dependencies:

bash
Copy code
npm install
Start the Angular development server:

bash
Copy code
ng serve
The frontend application will be available at http://localhost:4200.

Running the Project
Once both the backend and frontend are running, the Angular app should automatically connect to the API to fetch data and display products, categories, and other management features.
