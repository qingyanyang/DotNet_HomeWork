using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Info_Manager
{
    internal class StudentList
    {
        private List<Student> Students { get; set; } = new List<Student>();
        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void SortStudentsByAge()
        {
            Students.Sort((cur,next)=>cur.Age.CompareTo(next.Age));// by age
        }
        public void PrintStudents()
        {
            Console.WriteLine($"{"Name",-20}{"Age",-20}{"Course",-20}");
            foreach(Student student in Students)
            {
                student.PrintStudentInfo();
            }
        }

        public void SearchStudentByName(string InputName)
        {
            foreach (Student student in Students)
            {
                if(student.Name == InputName)
                {
                    Console.WriteLine($"{"Name",-20}{"Age",-20}{"Course",-20}");
                    student.PrintStudentInfo();
                    return;
                }
            }
            Console.WriteLine("Not Exist");
        }
        
    }
}
