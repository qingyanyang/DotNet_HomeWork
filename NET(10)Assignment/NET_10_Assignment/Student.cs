namespace NET_10_Assignment;

public delegate void HonourBonus();

public delegate void CourseStarting();

public class Student:IComparable<Student>
{
    public string Name { get; set; }
    public double Mark { get; set; }
    
    
    public void ReceiveBonus()
    {
        Console.WriteLine($"{Name} is the top1 student, receiving bonus");
    }

    public void Start()
    {
        Console.WriteLine($"{Name} is starting the course.");
    }

    public int CompareTo(Student? other)
    {
        if (Mark.CompareTo(other.Mark) == 0)
        {
            return Name.CompareTo(other.Name);
        }
        return Mark.CompareTo(other.Mark);
    }

    public void UpdateStudentInfo()
    {
        Console.WriteLine($"Student: {Name} Info Updated");
    }
}