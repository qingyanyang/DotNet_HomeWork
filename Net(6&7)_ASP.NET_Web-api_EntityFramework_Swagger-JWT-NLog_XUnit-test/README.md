## Assignment 06:

    1. Create Models and Generate Database Tables
    • Category Model:
    • Properties:
    • Id (Primary Key)
    • CategoryName: Name of the category.
    • CategoryLevel: Category level (used to represent whether it is a first-level or second-level category).
    • ParentId: Id of the parent category, used to implement the hierarchical relationship of categories.
    • Description: This model is mainly used to represent hierarchical relationships. For example, IT is a first-level category, and software is a subcategory of IT.
    • Course Model:
    • Properties:
    • Id (Primary Key)
    • CourseName: Name of the course.
    • Description: Description of the course.
    • CategoryId: Foreign key, linking to Category, indicating the category the course belongs to.
    • Relationships:
    • A Category can have multiple Course entities, meaning there is a one-to-many relationship between Category and Course.
    • The Category model also has a self-referencing relationship, with ParentId used to implement the parent-child relationship of categories.

    2. Implement Models and Relationships in DbContext
    • In EF Core’s DbContext, create DbSet<Category> and DbSet<Course>. Use Fluent API or data annotations to configure the one-to-many relationship between Category and Course and the self-referencing relationship in Category using ParentId.

    3. Configure Database Connection
    • Configure the database connection string in appsettings.json or Program.cs.
    • Use EF Core Migrations to generate the database tables.

    4. Implement REST API and Standard API
    • Category API:
    • Use REST API design to provide CRUD operations.
    • For example: GET /api/category to get all categories, POST /api/category to create a new category.
    • Course API:
    • Provide standard CRUD operations.
    • For example: GET /api/course to get all courses, POST /api/course to create a new course.

    5. Configure NLog
    • Refer to the official NLog documentation.
    • Write logs in your API operations or service layer, recording important actions (e.g., creating, deleting, updating data) and errors.

    6.	Configure Swagger
    • Refer to the official Swagger documentation.
    • Set up Swagger to auto-generate API documentation and use Swagger UI to test API endpoints.

    7. Advanced Section (JWT Authentication)
    • JWT Configuration:
    • Implement JWT token generation, creating a token during user login.
    • Validate the token received from the frontend in the API and use JWT to secure API routes.
    • Swagger Authorization:
    • Configure Authorization in Swagger to allow sending requests with JWT tokens in Swagger UI, making it possible to test protected API endpoints.

## Assignment 07：

    1. Create a xUnit test project for the Web API project, which is from the Lecture Web API project
    2. Add package moq to the xUnit test project
    3. Decide which controller/service need the unit test (for public method)
    4. Write the unit test for your selected API methods, if possible, can write you own public methods including the logic or calculation, which is better feasible for unit test

    - Given a few examples:

    _UserController.TestServcies method,_

    _TeacherController.Post method,_

    _TeacherService.GetUser method_

    1. Trying to use moq package to simulate the DBContext –e.g.CourseCategoryService.Add method in unit test
    2. Trying to use some fake external API in your Web API methods and write the unit test to mock this external API.

    Below are some candidates could be used from the fake external API - https://jsonplaceholder.typicode.com

    1. Trying to run the power shell script and generate the report - please go through the ps script and remember to replace the below value to your solution name
