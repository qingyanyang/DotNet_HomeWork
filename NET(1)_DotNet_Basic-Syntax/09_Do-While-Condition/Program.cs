// 9 > do-while Loop Practice:
// Continuously prompt the user to enter student names.
// The loop should stop when the user inputs "q".
string? studentName;

do
{
    Console.WriteLine("Enter student name:");
    Console.WriteLine("Enter \"Q\" to exit...");
    studentName = Console.ReadLine();

    if (studentName?.ToUpper() == "Q")
    {
        break;
    }

    Console.WriteLine($"Student name entered: {studentName}");

} while (true);