using Net8Assignment.Enums;

namespace Net8Assignment;

public class Tutor: Faculty,IAssess
{
    public override void PlanLearningActivities(string newActivity)
    {
        base.Activities.Add(newActivity);
        Console.WriteLine($"Tutor plan new learning activity: {newActivity}");
    }

    public override void PreparationLectures(string newLecture)
    {
        base.Lectures.Add(newLecture);
        Console.WriteLine($"Tutor plan new learning lecture: {newLecture}");
    }

    public override void DefineLearningObjectives(string[] objectives)
    {
        Console.WriteLine("Tutor defined the following objectives:");
        foreach (var obj in objectives)
        {
            Console.WriteLine($"* {obj}");
        }
    }
    public void AssessStudentGrade(Student student, StudentGrade grade)
    {
        student.Grade = grade; 
    }
}