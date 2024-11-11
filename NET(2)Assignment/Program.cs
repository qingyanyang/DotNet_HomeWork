using NET2Assignment;
using NET2Assignment.Courses;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Declare courses
            Course[] courses = {
                new CSharp(0,"CSharp",30),
                new CSharp(1,"HTML",3),
                new CSharp(2,"React",18)
            };

            // Declare teachers
            Teacher[] teachers = {
                new Teacher("Dai"),
                new Teacher("Ally"),
                new Teacher("Justin"),
            };

            // Assign teachers to courses
            AssignTeacherToCourse(teachers[0], courses[0]);
            AssignTeacherToCourse(teachers[1], courses[1]);
            AssignTeacherToCourse(teachers[2], courses[2]);

            // Declare students
            Student[] students = {
                new Student("A1", 18),
                new Student("A2", 19),
                new Student("A3", 18),
                new Student("A4", 20),
                new Student("A5", 18),
                new Student("A6", 19),
                new Student("A7", 18),
                new Student("A8", 18),
                new Student("A9", 19),
                new Student("A10", 20),
            };

            // Assign students to courses
            EnrollStudentInCourse(students[0], courses[0]);
            EnrollStudentInCourse(students[1], courses[0]);
            EnrollStudentInCourse(students[2], courses[0]);

            EnrollStudentInCourse(students[2], courses[1]);
            EnrollStudentInCourse(students[4], courses[1]);

            EnrollStudentInCourse(students[3], courses[2]);
            EnrollStudentInCourse(students[5], courses[2]);
            EnrollStudentInCourse(students[6], courses[2]);
            EnrollStudentInCourse(students[7], courses[2]);

            // Mark students
            //(int courseID, int studentID, int mark, Student[] students
            teachers[0].MarkStudent(courses[0].CourseID, students[0].StudentID, 90, students);
            teachers[0].MarkStudent(courses[0].CourseID, students[1].StudentID,  80, students);
            teachers[0].MarkStudent(courses[0].CourseID, students[2].StudentID, 100, students);

            teachers[1].MarkStudent(courses[1].CourseID, students[2].StudentID, 90, students);
            teachers[1].MarkStudent(courses[1].CourseID, students[4].StudentID, 80, students);

            teachers[2].MarkStudent(courses[2].CourseID, students[3].StudentID, 100, students);
            teachers[2].MarkStudent(courses[2].CourseID, students[5].StudentID, 90, students);
            teachers[2].MarkStudent(courses[2].CourseID, students[6].StudentID, 80, students);
            teachers[2].MarkStudent(courses[2].CourseID, students[7].StudentID, 100, students);

            // Print teacher course info
            teachers[0].PrintCourseStudentsInfo(students);
            teachers[1].PrintCourseStudentsInfo(students);
            teachers[2].PrintCourseStudentsInfo(students);

            // Print top3 student info of a course
            courses[0].PrintTop3StudentsInfo(students);
            courses[1].PrintTop3StudentsInfo(students);
            courses[2].PrintTop3StudentsInfo(students);
        }

        // Method to assign a teacher to a course and add course to teacher
        static void AssignTeacherToCourse(Teacher teacher, Course course)
        {
            teacher.AddCourseToTeacher(course);
            course.SetTeacher(teacher);
        }

        // Method to enroll a student in a course and add course to student
        static void EnrollStudentInCourse(Student student, Course course)
        {
            course.AddStudentByID(student.StudentID);

            var studentCourse = new Course(course.CourseID, course.Name, course.Hours)
            {
                Teacher = course.Teacher
            };
            student.AddCourseToStudent(studentCourse);
        }
    }
}