using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET2Assignment.Courses;

namespace NET2Assignment
{
    internal class Teacher
    {
        private static int _counter = 0;
        public int TeacherID { get; private set; }
        public  string Name { get; set; }
        public  List<Course> Courses { get; private set; } = new List<Course>();

        public Teacher(string name) { 
            Name = name;
            TeacherID = _counter++;
        }

        public void AddCourseToTeacher(Course course)
        {
            Courses.Add(course);  
        }
        public void PrintCourseStudentsInfo(Student[] students)
        {
            Console.WriteLine("\n===========================================================");
            Console.WriteLine($"Teacher: {Name} (Teacher ID: {TeacherID})");

            foreach (Course course in Courses)
            {
                Console.WriteLine("\nCourses Teaching: ");
                Console.WriteLine($"Course: {course.Name} (Course ID: {course.CourseID})");
                Console.WriteLine($"Course Hours: {course.Hours}");
                Console.WriteLine("Students Enrolled:");

                var enrolledStudents = students.Where(student=>course.StudentIDs.Contains(student.StudentID))
                    .Select(student=> new
                    {
                        student.StudentID,
                        student.Name,
                        student.Age,
                        Mark = student.Courses.FirstOrDefault(c => c.CourseID == course.CourseID)?.Mark??0
                    })
                    .ToList();

                if (enrolledStudents.Any())
                {
                    foreach (var student in enrolledStudents)
                    {
                        Console.WriteLine($"- Student ID: {student.StudentID}, Name: {student.Name}, Age: {student.Age}, Mark: {student.Mark}");
                    }
                }
                else
                {
                    Console.WriteLine("No students enrolled.");
                }
            }
        }

        public void MarkStudent(int courseID, int studentID, int mark, Student[] students)
        {
            Course? course = Courses.FirstOrDefault(course => course.CourseID == courseID);
            if (course != null) {
                Student? student = students.FirstOrDefault(student => student.StudentID == studentID);
                if (student != null) {
                    Course? studentCourse = student.Courses.FirstOrDefault(course => course.CourseID == courseID);
                    if (studentCourse != null)
                    {
                        studentCourse.Mark = mark;
                    }    
                }
            }
        }
    }
}
