// 3 > Placeholder Practice:
// Write a program to calculate how many weeks and days are in a given number of days (e.g., 46 days).
// The result should be displayed as: "6 weeks and 4 days".
int week = 0;
int day = 0;
string? totalDaysString;

Console.WriteLine("Enter total days: ");
totalDaysString = Console.ReadLine();

bool inputTotalDayIsNum = int.TryParse(totalDaysString, out int totalDays);
while (!inputTotalDayIsNum)
{
    Console.WriteLine("Invalid input, Please reEnter:");
    totalDaysString = Console.ReadLine();
    inputTotalDayIsNum = int.TryParse(totalDaysString, out totalDays);
}

week = totalDays / 7;
day = totalDays % 7;

Console.WriteLine($"week {week} and days {day}");

Console.WriteLine("Enter \"E\" to exit program...");
while (Console.ReadKey(false).Key != ConsoleKey.E)
{
};

