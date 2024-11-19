namespace NET_09_Assignment;

public class StudentCourse
{
    public Guid CourseId { get; set; }
    public Course Course { get; set; }
    public Student Student { get; set; }
    public Guid StudentId { get; set; }
    
    public double? Mark { get; set; }

    public StudentCourse(Guid courseId, Course course, Guid studentId, Student student)
    {
        CourseId = courseId;
        Course = course;
        StudentId = studentId;
        Student = student;
    }
}