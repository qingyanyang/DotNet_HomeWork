## Assignment

Building on the first Web API assignment, the new requirement is to introduce a unified return type. All API responses should maintain a consistent structure to improve code readability, maintainability, and ensure that each API provides a uniform response format, making it easier for the frontend and other consumers to handle the returned data.

1. Unified Return Structure:

   1.1 All API response types should include the following properties:
   • Success: Indicates whether the operation was successful.
   • Message: A message indicating success or failure.
   • Data: The specific data being returned, which can be null.
   • Errors: A list of error messages, returned when the operation fails.

   1.2 Use of Generics: Implement the unified return structure using a generic class to ensure different types of data can be passed through the consistent response structure.

2. Implement a custom filter inheriting from ActionFilterAttribute to intercept requests before each action is executed. The filter should capture the action name and parameter information.

3. Create a CustomExceptionFilter class implementing the IExceptionFilter interface.

   • All exceptions thrown in the API should be caught by this filter and returned using the unified response structure defined earlier.

   • Apply the ExceptionFilter to all controllers in the project.

4. Implement a Result Filter:

   • This filter should intercept the result after an action is executed and perform any necessary modifications.

   • Apply the Result Filter to all controllers in the project, ensuring all returned results use the previously defined unified response structure.

   • Additionally, add a success timestamp to successful responses.
