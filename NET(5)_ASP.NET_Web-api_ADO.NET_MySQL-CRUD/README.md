### Assignment: CRUD Operations with MySQL and [ADO.NET](http://ado.net/)

1. **Database Setup**
   Task: Create a user table in MySQL with the following columns:
   id (Primary Key, int, auto-increment)
   username (varchar(45))
   password (varchar(45))
   email (varchar(200))
   age (int)
   gender (int)
   active (bool, corresponds to BINARY(1) in the database)
   address (varchar(500))

1. **UserService Implementation**
   Task: Create a UserService class that implements the IUserService interface.
   Use [ADO.NET](http://ado.net/) to implement the following methods:
   Insert (Create new user)
   Update (Modify user details)
   Delete (Remove a user)
   Search (Query users)
   Data Retrieval Methods:
   Implement two ways to retrieve data:
   Row-by-Row reading (using MySqlDataReader)
   MySqlDataAdapter for data handling.

1. **UserController**
   Task: Create a UserController class and write the necessary Action methods to handle CRUD operations (Create, Read, Update, Delete) by calling the methods from UserService.
   Use Dependency Injection to inject UserService into UserController.
   Register the IUserService interface and its implementation (UserService) in the DI container for proper service resolution.

1. **API Testing with Postman**
   Task: Test the CRUD operations using Postman to ensure all API endpoints for creating, reading, updating, and deleting users work as expected.
