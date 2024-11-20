// 16 > ref Practice:
// Use a method to swap two int variables.


int a = 3;
int b = 4;

Console.WriteLine($"Before: a: {a}, b: {b}");

void SwitchTwoInts(ref int a, ref int b)
{
    int c = a;
    a = b;
    b = c;
}

SwitchTwoInts(ref a, ref b);

Console.WriteLine($"After: a: {a}, b: {b}");