using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Info_Manager
{
    internal class Student
    {
        private string _name = string.Empty;
        private int _age; //default 0
        private string _course= string.Empty;
        public string Name
        {
            get{ return _name; }

            set{
                if (value.Length == 0 || value.Length > 20)
                {
                    throw new ArgumentException("name length should greater than 0 and less than 20!");
                }
                else
                {
                    _name = value;
                }
            }
            
        }

        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 16 || value > 35)
                {
                    throw new ArgumentException("age should greater or equeal to 16 and less than or equal to 35!");
                }
                else
                {
                    _age = value;
                }
            }
        }

        public string Course
        {
            get { return _course; }

            set
            {
                if (value.Length == 0 || value.Length > 20)
                {
                    throw new ArgumentException("course length should greater than 0 and less than 20!");
                }
                else
                {
                    _course = value;
                }
            }

        }
        public Student()
        {
            
        }
        public Student(string name, int age, string course)
        {
            Name = name;
            Age =   age;
            Course = course;
        }

        public void PrintStudentInfo()
        {
            Console.WriteLine($"{this._name,-20}{this._age,-20}{this._course,-20}");
        }
    }
}
