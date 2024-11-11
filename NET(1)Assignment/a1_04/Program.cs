// 4 > $ Practice:
// Write a program to calculate how many days, hours, minutes, and seconds are in 107653 seconds.

int day = 0;
int hour = 0;
int minute = 0;

string? secondsString;
int seconds = 0;

Console.WriteLine("Enter a number, which stands for seconds:");
secondsString = Console.ReadLine();
while(!int.TryParse(secondsString,out seconds))
{
    Console.WriteLine("Invalid number, please try again:");
    secondsString = Console.ReadLine();
}
int secondsForDay = 24 * 60 * 60;
int secondsForHour = 60 * 60;

day = seconds / secondsForDay;
hour = (seconds - secondsForDay * day) / secondsForHour;
minute = (seconds - secondsForDay * day) % secondsForHour;

Console.WriteLine($"{day}days {hour}hours {minute}seconds");

Console.WriteLine("Enter \"E\" to exit program...");
while(Console.ReadKey(false).Key!= ConsoleKey.E)
{
};
