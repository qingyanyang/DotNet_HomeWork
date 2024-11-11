// 10 > for Loop Practice:
// Find all the Narcissistic numbers between 100 and 999.
// A Narcissistic number is defined as a three-digit number where the sum of the cubes 
// of its hundreds, tens, and units digits equals the number itself.
int unitPlace = 0;
int tensPlace = 0;
int hundredsPlace = 0;

for (int i =100;i<1000;i++) {
    unitPlace = i / 100;
    tensPlace = (i - unitPlace * 100) / 10;
    hundredsPlace = (i - unitPlace * 100) % 10;
    if (Math.Pow(unitPlace,3) + Math.Pow(tensPlace, 3)+ Math.Pow(hundredsPlace, 3) == i)
    {
        Console.WriteLine($"{i} is Daffodils number.");
    }
}