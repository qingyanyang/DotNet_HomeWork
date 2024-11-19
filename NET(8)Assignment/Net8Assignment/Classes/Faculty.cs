namespace Net8Assignment;

public abstract class Faculty
{
    public virtual List<String> Activities { get; set; } = new List<string>();
    public virtual List<String> Lectures { get; set; } = new List<string>();
    public abstract void PlanLearningActivities(string newActivity);
    public abstract void PreparationLectures(string newLecture);
    public virtual void DefineLearningObjectives(string[] objectives)
    {
        Console.WriteLine("Learning Objectives: ");
        foreach (var obj in objectives)
        {
            Console.WriteLine($"* {obj}");
        }
    }
}