using Net8Assignment;
using Net8Assignment.Enums;


public class Professor: Faculty,IRefer
{
    public override void PlanLearningActivities(string newActivity)
    {
        base.Activities.Add(newActivity);
        Console.WriteLine($"professor planned new learning activity: {newActivity}");
    }

    public override void PreparationLectures(string newLecture)
    {
        base.Lectures.Add(newLecture);
        Console.WriteLine($"professor prepared new lecture: {newLecture}");
    }

    public override void DefineLearningObjectives(string[] objectives)
    {
        Console.WriteLine("Professor defined the following objectives:");
        foreach (var obj in objectives)
        {
            Console.WriteLine($"* {obj}");
        }
    }

    public void ReferTheJob(Student student)
    {
        if (student.Grade == StudentGrade.A)
        {
            Console.WriteLine($"professor referred to student {student.Name}");
        }
        else
        {
            Console.WriteLine("this student\'s grade is not the same as A");
        }
    }
}