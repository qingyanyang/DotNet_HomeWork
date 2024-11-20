namespace Net8Assignment;

public class Instructor: Faculty,INotifify
{
    public override void PlanLearningActivities(string newActivity)
    {
        base.Activities.Add(newActivity);
        Console.WriteLine($"Instructor planed the new Activity: {newActivity}");
    }

    public override void PreparationLectures(string newLecture)
    {
        base.Lectures.Add(newLecture);
        Console.WriteLine($"Instructor planed the new Lecture: {newLecture}");
    }

    public override void DefineLearningObjectives(string[] objectives)
    {
        Console.WriteLine("Instructor defined the following objectives:");
        foreach (var obj in objectives)
        {
            Console.WriteLine($"* {obj}");
        }
    }

    public void Notification(Student student, string message)
    {
        Console.WriteLine($"Instructor notification: {message}");
        student.Notifications.Add(message);
    }
}