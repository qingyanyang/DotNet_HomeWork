## Assignment Requirements:

Implement CRUD operations using Web API and REST API, supporting multiple parameter passing methods and using model classes with model validation. All APIs must be tested and verified using Postman, with a return type of JsonResult.

# Basic Requirements:

1. UserController:

   • Implement CRUD operations for users using a standard Web API, supporting the following three parameter passing methods:

   1. Route Parameters: Pass parameters through the URL path.
   2. Query String Parameters: Pass parameters through the query string after ?.
   3. Request Body Parameters: Pass complex objects through the request body in POST and PUT requests.
      • Use [FromBody] and [FromForm] in POST and PUT requests to try different ways of passing data.
      • All API return types should be JsonResult.

2. TeacherController:

   • Implement CRUD operations for teachers using a RESTful API, supporting the same three parameter passing methods:

   1. Route Parameters: Pass parameters through the URL path.
   2. Query String Parameters: Pass parameters through the query string after ?.
   3. Request Body Parameters: Pass complex objects through the request body in POST and PUT requests.
      • Also, use [FromBody] and [FromForm] in POST and PUT requests to try different ways of passing data.
      • All API return types should be JsonResult.

# Model Classes and Validation Requirements:

User Model:
• Fields include:
• UserName: The name of the user.
• Email: The user’s email address.
• Address: The user’s address.
• Gender: The user’s gender (enum: Male, Female, Other).
• Password: The user’s password.
• Phone: The user’s phone number.
• Validation Requirements: 1. UserName: Cannot be empty. 2. Email: Must follow a valid email format. 3. Phone: Must follow a custom Australian phone number format (using custom model validation). 4. Gender: Must be an enum of Male, Female, or Other.

Teacher Model:
• Fields include:
• UserId: A foreign key pointing to the User table, must be provided.
• Department: The department the teacher belongs to, cannot be empty.
• Description: A description of the teacher, maximum of 500 characters.
• Specialty: Courses the teacher specializes in.
• Validation Requirements: 1. Description: Cannot exceed 500 characters. 2. UserId: Must be provided, as a foreign key pointing to the User table. 3. Department: Cannot be empty.

# Detailed Parameter Passing Functionality:

1. Route Parameters:
   • Pass parameters through the URL path. Examples:
   • /api/user/{id}: Get user information by user ID.
   • /api/teacher/{id}: Get teacher information by teacher ID.

2. Query String Parameters:
   • Pass parameters using the query string after ?. Examples:
   • /api/user?email=user@example.com: Get user information by email.
   • /api/teacher?email=teacher@example.com: Get teacher information by email.

3. Request Body Parameters:
   • Pass objects through the request body using POST or PUT requests, with [FromBody] or [FromForm]. Examples:
   • Use a POST request to create a user, placing the full user data in the request body as JSON or form format.
   • Use a PUT request to update teacher data, placing the full teacher data in the request body as JSON or form format.
