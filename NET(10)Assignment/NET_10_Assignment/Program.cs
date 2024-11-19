namespace NET_10_Assignment;

class Program
{
    static void Main(string[] args)
    {
        List<Student> students = new List<Student>()
        {
            new Student(){Name = "A1",Mark = 91},
            new Student(){Name = "A2",Mark = 90},
            new Student(){Name = "A3",Mark = 89},
            new Student(){Name = "A4",Mark = 67},
            new Student(){Name = "A5",Mark = 91},
            new Student(){Name = "A6",Mark = 87},
            new Student(){Name = "A7",Mark = 92},
            new Student(){Name = "A8",Mark = 90},
            new Student(){Name = "A9",Mark = 91},
            new Student(){Name = "A10",Mark = 89},
            new Student(){Name = "A11",Mark = 81},
            new Student(){Name = "A12",Mark = 77},
        };
        
        students.Sort();
        students.Reverse();
        
        Student topStudent = students[0];
        HonourBonus honourBonus = topStudent.ReceiveBonus;
        honourBonus.Invoke();
        
        CourseStarting? courseStarting = null;
        foreach (var student in students)
        {
            courseStarting += student.Start;
        }
        courseStarting.Invoke();
        
        
        Course course = new Course();
        foreach (var student in students)
        {
            course.ScheduledTimeChangeEvent += student.UpdateStudentInfo;
        }
        course.ScheduleTimeChange();
    }
}