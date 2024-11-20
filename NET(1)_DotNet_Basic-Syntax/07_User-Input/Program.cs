// 7 > Leap Year and Days in Month Practice:
// Prompt the user to enter a year and then a month.
// Output the number of days in that month and determine whether the input year is a leap year.
Console.WriteLine("Enter year:");
string? yearString = Console.ReadLine();
int year = 0;
while (!int.TryParse(yearString,out year))
{
    Console.WriteLine("Invalid number! Try again:");
    yearString = Console.ReadLine();
}

Console.WriteLine("Enter month:");
string? monthString = Console.ReadLine();
int month = 0;
while (!int.TryParse(monthString, out month))
{
    Console.WriteLine("Invalid number! Try again:");
    monthString = Console.ReadLine();
}

bool isLeapYear = (year % 4==0) && (year % 100 == 0) && (year % 400 == 0);
int days = 0;
if(month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10|| month == 12)
{
    days = 31;

}else if (month == 2)
{
   
    days = isLeapYear?29:28;
}
else
{
    days = 30;
}

string isLeapYearString = isLeapYear ? "is" : "isn't";

Console.WriteLine($"{days} days totally, and {isLeapYearString} leap year");

Console.WriteLine("Enter \"E\" to exit program...");
while (Console.ReadKey(false).Key != ConsoleKey.E)
{
};