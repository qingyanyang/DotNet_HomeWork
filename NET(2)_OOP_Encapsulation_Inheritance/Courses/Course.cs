using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NET2Assignment.Courses
{
    internal class Course
    {
        public int CourseID { get; set; }
        public string Name { get; set; }
        public int Mark { get; set; }
        // Q: list of id/obj difference
        public List<int> StudentIDs { get; private set; } = new List<int>();
        public Teacher? Teacher { get; set; }
        public int Hours { get; set; }

        public Course(int courseID, string name, int hours)
        {
            CourseID = courseID; 
            Name = name;
            Hours = hours;
        }
        public virtual void SetTeacher(Teacher teacher)
        {
            Teacher = teacher;
        }  
        public virtual void AddStudentByID(int studentID)
        {
            if (!StudentIDs.Contains(studentID))
            {
                StudentIDs.Add(studentID);
            }
        } 

        public void PrintTop3StudentsInfo(Student[] students)
        {
            List<Student> top3Students= students
                .Where(student=>StudentIDs.Contains(student.StudentID))
                .OrderByDescending(student=>student.Courses.FirstOrDefault(course=>course.CourseID == this.CourseID)?.Mark)
                .Take(3)
                .ToList();
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine($"\nTop 3 students in course: {Name}");
            int rank = 1;
            foreach (Student student in top3Students) {
                Course? course = student.Courses.FirstOrDefault(course=>course.CourseID==this.CourseID);
                if (course != null) {
                    Console.WriteLine($"NO.{rank++}:");
                    Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, Mark: {course.Mark}");
                }   
            }
        }
    }
}
