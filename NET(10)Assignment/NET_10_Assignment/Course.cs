namespace NET_10_Assignment;

public delegate void ScheduledTimeChanged();
public class Course
{
    public string CourseName { get; set; }
    
    public event ScheduledTimeChanged ScheduledTimeChangeEvent;

    public void ScheduleTimeChange()
    {
        if (ScheduledTimeChangeEvent != null)
        {
            ScheduledTimeChangeEvent();
        }
    }
}