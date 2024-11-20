## Assignment

    • Practice Polymorphism - Method Overloading (Overload) and Method Overriding (Override)
    • Choosing Between Interfaces and Abstract Classes

# Requirements:

    1. Choose any class and create at least two overloaded methods within it, and call them to understand what compile-time polymorphism (static polymorphism) is.

    2. Create a base class Faculty and define the following:

        2.1 Properties – (consider if they should be abstract)

        • Activities (Teaching objectives, which can be string[], List<string>, or a custom Activity class)
        • Lectures (Teaching materials/notes, with the same options as above)

        2.2 Abstract Methods

        • PlanLearningActivities
        • PreparationLectures

        2.3 Virtual Methods (with default implementation)

        • DefineLearningObjectives() – You can decide whether to include parameters and define the type and number of parameters.

    3. Define classes that inherit from Faculty and implement the abstract/virtual methods:
        • Professor
        • Tutor
        • Instructor

    4. Define an interface IRefer with a method ReferTheJob. Only Professor should have the functionality to refer a job. Implement this in Professor, ensuring that the job referral is only for students with Grade A.
        • Define another interface IAssess with a method AssessStudentGrade. Only Tutor should have the functionality to grade students.
        • Define another interface INotify with a method Notification. Only Instructor should have the responsibility to notify students about classes.

# Implementation:

    1. Create a Professor object, a Tutor object, and an Instructor object. Print their:
        • Learning Activities
        • Prepared Lectures

    2. Create 10 student objects, only needing one C# Course (no need for multiple courses).

# Functionality:

    1. The Instructor should notify students about the class.
    2. The Tutor should grade and rate students.
    3. The Professor should refer jobs to Grade A students (simulate this using Console.WriteLine()).
