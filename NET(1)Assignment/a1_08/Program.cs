// 8 > while Loop Practice:
// The teacher asks the student, "Have you learned how to solve this problem?" 
// If the student answers "yes (y)", they can leave. 
// If the student answers "no (n)", the teacher explains it again and asks the same question.
// This repeats until the student answers "yes" or the teacher has explained it 10 times, 
// at which point the student is dismissed, whether they understand or not.

bool getIt = false;
string? getItString;
int times = 0;

while (!getIt && times < 10)
{
 
    Console.WriteLine("Have you got that?(y/n)");
    getItString = Console.ReadLine();

    if (getItString == "y")
    {
        getIt = true;
    }
    else if(getItString == "n")
    {
        getIt = false;
        times++;
    }
    else
    {
        Console.WriteLine("Invalid input! Try again:");
    }
}

Console.WriteLine("time out! 886");

Console.WriteLine("Enter \"E\" to exit program...");
while (Console.ReadKey(false).Key != ConsoleKey.E)
{
};