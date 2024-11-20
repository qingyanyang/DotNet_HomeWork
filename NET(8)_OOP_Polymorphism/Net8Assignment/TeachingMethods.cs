namespace Net8Assignment;

public static class CourseTeachingService
{
    public static void TeachCourse(string courseName)
    {
        Console.WriteLine($"Teaching {courseName}");
    }

    public static void TeachCourse(string courseName, bool isOnsite)
    {
        Console.WriteLine($"Teaching {courseName} and is {(isOnsite ? "" : "not ")}onsite");
    }

    public static void TeachCourse(string courseName, bool isOnsite, double hours)
    {
        Console.WriteLine($"Teaching {courseName} and is {(isOnsite ? "" : "not ")}onsite and will spend {hours} hours");
    }
}