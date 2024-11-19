
using Net8Assignment;
using Net8Assignment.Enums;
CourseTeachingService.TeachCourse("Math");
CourseTeachingService.TeachCourse("Math",true);
CourseTeachingService.TeachCourse("Math",false,4.5);
Console.WriteLine("");

List<Student> students = new List<Student>
{
    new Student { Name = "Alice" },
    new Student { Name = "Bob"},
    new Student { Name = "Charlie" },
    new Student { Name = "David"},
    new Student { Name = "Eve"},
    new Student { Name = "Frank"},
    new Student { Name = "Grace"},
    new Student { Name = "Hank"},
    new Student { Name = "Ivy"},
    new Student { Name = "Jack"}
};

Faculty professorFaculty = new Professor();
Faculty tutorFaculty = new Tutor();
Faculty instructorFaculty = new Instructor();

professorFaculty.PlanLearningActivities("Research Seminar");
professorFaculty.PreparationLectures("Advanced Programming Concepts");

tutorFaculty.PlanLearningActivities("Math Workshop");
tutorFaculty.PreparationLectures("Calculus Revision");

instructorFaculty.PlanLearningActivities("Practical Lab");
instructorFaculty.PreparationLectures("Lab Safety and Procedures");

Console.WriteLine("\n--- Professor ---");
Console.WriteLine("Learning Activities: " + string.Join(", ", professorFaculty.Activities));
Console.WriteLine("Prepared Lectures: " + string.Join(", ", professorFaculty.Lectures));

Console.WriteLine("\n--- Tutor ---");
Console.WriteLine("Learning Activities: " + string.Join(", ", tutorFaculty.Activities));
Console.WriteLine("Prepared Lectures: " + string.Join(", ", tutorFaculty.Lectures));

Console.WriteLine("\n--- Instructor ---");
Console.WriteLine("Learning Activities: " + string.Join(", ", instructorFaculty.Activities));
Console.WriteLine("Prepared Lectures: " + string.Join(", ", instructorFaculty.Lectures));

Console.WriteLine("");

IRefer professor = new Professor();
IAssess tutor = new Tutor();
INotifify instructor = new Instructor();

foreach (var student in students)
{
    Console.WriteLine("");
    instructor.Notification(student,"Class begins.");
    tutor.AssessStudentGrade(student,StudentGrade.A);
    professor.ReferTheJob(student);
}







