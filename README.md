# WebAPI Dapper Service
A RESTful Web API built with ASP.NET Core and Dapper for managing Students and Teachers data with SQL Server database. This updated version includes improved security by using parameterized queries throughout the application.

## 📋 Project Overview
This project demonstrates a clean Web API implementation using:
* ASP.NET Core Web API - For building RESTful services
* Dapper - Lightweight ORM for data access
* SQL Server - Database backend
* Repository Pattern - With shared Dapper service
* Parameterized Queries - Enhanced security against SQL injection

## 🏗️ Project Structure
        text
        WebAPI_DapperService/
        ├── Controllers/
        │   ├── StudentsController.cs
        │   └── TeachersController.cs
        ├── Models/
        │   ├── StudentModels/
        │   │   ├── StudentReqModel.cs
        │   │   └── StudentResModel.cs
        │   └── TeacherModels/
        │       ├── TeacherReqModel.cs
        │       └── TeacherResModel.cs
        Dapper.Shared/
        └── DapperService.cs
## 🚀 Features
### Students Management
* ✅ Get all active students
* ✅ Create new student
* ✅ Update student information
* ✅ Soft delete student

### Teachers Management
* ✅ Get all active teachers
* ✅ Create new teacher
* ✅ Update teacher information
* ✅ Soft delete teacher

## 🔒 Security Improvements
The updated controllers now use parameterized queries for all database operations, providing:

* ✅ SQL Injection Protection - All user inputs are properly parameterized
* ✅ Type Safety - Strongly typed parameters
* ✅ Better Performance - Query plan reuse
* ✅ Cleaner Code - More maintainable and readable

## 📊 Database Schema
### Students Table
        sql
        CREATE TABLE Tbl_Student (
            Id INT PRIMARY KEY IDENTITY(1,1),
            RollNo NVARCHAR(50) NOT NULL,
            Name NVARCHAR(100) NOT NULL,
            Email NVARCHAR(100) NOT NULL,
            DeleteFlag BIT DEFAULT 0
        );
### Teachers Table
        sql
        CREATE TABLE Tbl_Teacher (
            Id INT PRIMARY KEY IDENTITY(1,1),
            Name NVARCHAR(100) NOT NULL,
            Phone NVARCHAR(20) NOT NULL,
            Subject NVARCHAR(100) NOT NULL,
            DeleteFlag BIT DEFAULT 0
        );

## 🔧 Installation & Setup
### Prerequisites
* .NET 8.0
* SQL Server
* Visual Studio 2022

### Configuration
1. Clone the repository

        bash
        git clone https://github.com/thetnaing-dh/WebAPI_Dapper-Service
        cd WebAPI_DapperService
2. Update connection strings in both controllers:

        csharp
        private readonly string _connectionString = "Server=.;Database=StudentDB;User Id=sa;Password=your_password;TrustServerCertificate=True;";
3. Run the application

        bash
        dotnet run
## 📚 API Endpoints
### Students Controller
* GET	api/students	Get all active students	-
* POST	api/students	Create new student	StudentReqModel in body
* PUT	api/students/{id}	Update student	id (int), StudentReqModel in body
* DELETE	api/students/{id}	Soft delete student	id (int)
### Teachers Controller
* GET	api/teachers	Get all active teachers	-
* POST	api/teachers	Create new teacher	TeacherReqModel in body
* PUT	api/teachers/{id}	Update teacher	id (int), TeacherReqModel in body
* DELETE	api/teachers/{id}	Soft delete teacher	id (int)
## 📝 Models
### Student Request Model
        csharp
        public class StudentReqModel
        {
            public string RollNo { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }
### Student Response Model
        csharp
        public class StudentResModel
        {
            public int Id { get; set; }
            public string RollNo { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public bool DeleteFlag { get; set; }
        }
### Teacher Request Model
        csharp
        public class TeacherReqModel
        {
            public string Name { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Subject { get; set; } = string.Empty;
        }
### Teacher Response Model
        csharp
        public class TeacherResModel
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Phone { get; set; } = string.Empty;
            public string Subject { get; set; } = string.Empty;
            public bool DeleteFlag { get; set; }
        }
## 🛠️ Dapper Service
The shared Dapper service provides three main methods:

        csharp
        public class DapperService
        {
            // Execute query and return list of objects
            public List<T> Query<T>(string query, object? param = null)
            
            // Execute query and return first result or default
            public T QueryFirstOrDefault<T>(string query, object? param = null)
            
            // Execute non-query commands (INSERT, UPDATE, DELETE)
            public int Execute(string query, object? param = null)
        }
## 🔍 Usage Examples
### Get All Students
        http
        GET /api/students
* Response:

        json
        [
            {
                "rollNo": "S001",
                "name": "mgmg",
                "email": "mgmg@mail.com"
            }
        ]
### Create Student
        http
        POST /api/students
        Content-Type: application/json
        
        {
            "rollNo": "S001",
            "name": "mgmg",
            "email": "mgmg@mail.com"
        }
* Response:

        json
        "Inserting Successfully."
### Update Student
        http
        PUT /api/students/1
        Content-Type: application/json
        
        {
            "rollNo": "S001",
            "name": "koko",
            "email": "koko@mail.com"
        }
* Response:

        json
        "Updating Successfully."
### Delete Student
        http
        DELETE /api/students/1
* Response:

        json
        "Deleting Successfully."
## 🎯 Key Improvements in Updated Code
1. Parameterized INSERT Operations

                   csharp
                // Before (Vulnerable to SQL injection):
                string query = $@"INSERT INTO Tbl_Student(RollNo,Name,Email,DeleteFlag) 
                                VALUES('{StudentResModel.RollNo}','{StudentResModel.Name}','{StudentResModel.Email}',0);";
                
                // After (Secure parameterized query):
                string query = @"INSERT INTO Tbl_Student(RollNo,Name,Email,DeleteFlag) 
                                VALUES(@RollNo, @Name, @Email,0);";
                var parameter = new StudentReqModel { ... };
3. Parameterized UPDATE Operations

                   csharp
                // Secure update with parameters
                string query = @"UPDATE Tbl_Student SET 
                                RollNo = @RollNo,
                                Name = @Name,
                                Email = @Email                           
                                WHERE Id = @Id;";
                var parameter = new StudentResModel { ... };
5. Consistent Model Usage
* Request Models (StudentReqModel, TeacherReqModel) for input operations
* Response Models (StudentResModel, TeacherResModel) for output operations and internal processing

## ⚠️ Best Practices Implemented
* ✅ Parameterized Queries - Protection against SQL injection
* ✅ Separation of Concerns - Clear separation between request and response models
* ✅ Soft Deletes - Data preservation with DeleteFlag
* ✅ Error Handling - Proper HTTP status codes and messages
* ✅ Consistent Naming - Clear and descriptive method names

## 🚀 Future Enhancements
* Add Dependency Injection configuration
* Implement repository pattern
* Add validation attributes to models
* Implement global exception handling
* Add logging
* Implement pagination
* Add Swagger/OpenAPI documentation
* Add unit tests
* Implement authentication and authorization

## 🤝 Contributing
1. Fork the project
2. Create your feature branch (git checkout -b feature/AmazingFeature)
3. Commit your changes (git commit -m 'Add some AmazingFeature')
4. Push to the branch (git push origin feature/AmazingFeature)
5. Open a Pull Request

## 📄 License
This project is licensed under the MIT License - see the LICENSE file for details.
