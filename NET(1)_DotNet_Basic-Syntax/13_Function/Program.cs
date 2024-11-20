// 13 > Method Practice:
// Write a method to calculate and return the maximum value between two integers.
int MaxInteger(int integer01, int integer02)
{
    return Math.Max( integer01, integer02 );
}

Console.WriteLine("max value between {0} and {1} is {2}", 4, 5, MaxInteger(4, 5));