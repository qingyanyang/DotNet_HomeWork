namespace NET_09_Assignment;

public class Student
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public Student(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public IDictionary<string, double?> GetGrades()
    {
        IDictionary<string, double?> grades = new Dictionary<string, double?>();
        var studentCourses = StudentCourses.Where(studentCourse => studentCourse.StudentId == Id);
        foreach (var studentCourse in studentCourses)
        {
            grades.Add($"{studentCourse.Student.Name} + {studentCourse.Course.CourseName}", studentCourse.Mark );
        }
        return grades;
    }
}