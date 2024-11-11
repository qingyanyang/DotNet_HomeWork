using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET2Assignment.Courses;

namespace NET2Assignment
{
    internal class Student
    {
        private static int _counter = 0;
        public int StudentID { get; private set; }
        public  string Name { get; set; }
        public int Age { get; set; }
        public  List<Course> Courses { get; set; } = new List<Course>();

        public Student(string name, int age)
        {
            Name = name;
            Age = age;
            StudentID = _counter++;
        }
        public void AddCourseToStudent(Course course) {
            Courses.Add(course);
        }
    }
}
