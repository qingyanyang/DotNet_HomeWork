## Assignment

Create a console app and design several classes. Besides the properties or fields mentioned below, you can add additional information. 1. Class Definitions
• Define a base class Course with properties or fields including:
• ID
• CourseName
• Score
• StudentIDs array (since multiple students might enroll in the same course)
• ClassHours (recommended to use properties rather than fields)
• Define a Student class with properties or fields including:
• ID
• Name
• Age
• Courses array (since one student can choose multiple courses; the courses can be represented by either their IDs or the Course objects)
• Define a Teacher class with properties or fields including:
• ID
• Name
• CoursesTaught array (since one teacher might teach multiple courses; the courses can be represented by either their IDs or the Course objects) 2. Inheritance
• Define subclasses that inherit from the Course class, such as:
• CSharpCourse
• HTMLCourse
• ReactCourse 3. Using the Classes and Inheritance
• 3.1 Initialize objects:
• Create an array of Course objects (3 courses)
• Create an array of Teacher objects (3 teachers corresponding to the three different courses)
• Create an array of Student objects (e.g., 10 student objects)
• Use new to call constructors and create objects
• 3.2 Associate teachers, students, and courses:
• For example, Teacher A teaches the C# course, with 3 students enrolled. Teacher B teaches the HTML course, with 2 students enrolled, and so on.
• 3.3 In the Teacher class, define a method that prints out the courses the teacher teaches and the information of the students enrolled.
• 3.4 In the Course class, define a method that prints out the information of the top 3 students based on scores.
