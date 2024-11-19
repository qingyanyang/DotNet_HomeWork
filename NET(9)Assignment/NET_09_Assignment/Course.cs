namespace NET_09_Assignment;

public class Course
{
    public Guid Id { get; set; }
    public string CourseName { get; set; }
    
    public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public Course(string courseName)
    {
        Id = Guid.NewGuid();
        CourseName = courseName;
    }

    public void MarkCourse(Guid studentId, double markValue)
    {
        StudentCourses.FirstOrDefault(studentCourse =>
                studentCourse.StudentId == studentId && studentCourse.CourseId == Id)!
            .Mark = markValue;
    }

    public IEnumerable<double> getMarks()
    {
        var marks = StudentCourses.Where(studentCourse => studentCourse.CourseId == Id)?
            .Select(studentCourse => studentCourse.Mark) // [90,null,91,null...]
            .Where(mark => mark.HasValue) //[90,91,...]
            .Select(mark => mark.Value);//??
        return marks;
    }

    public ILookup<double, string> getStudentNamesWithSameMark()
    {
        
        var studentCourses = StudentCourses
            .Where(studentCourse=>studentCourse.CourseId == Id);

        var res = new List<MarkStudentName>();
        foreach (var studentCourse in studentCourses)
        {
            res.Add(new MarkStudentName{Mark=studentCourse.Mark.Value,StudentName=studentCourse.Student.Name});
        }

        return (Lookup<double, string>)res.ToLookup(item=>item.Mark,item=>item.StudentName);
    }

    internal class MarkStudentName
    {
        public double Mark { get; set; }
        public string StudentName { get; set; }
    }
}