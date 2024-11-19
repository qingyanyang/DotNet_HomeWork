using Net8Assignment.Enums;

public class Student
{
    public static string CourseName { get; } = "C#";
    public string Name { get; set; }
    public StudentGrade Grade { get; set; }
    public List<string> Notifications { get; set; }= new List<string>();
}