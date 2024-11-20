// 1 > Variable Practice:
// Define four variables to store a person's name, gender, age, and telephone number, respectively. 
// Then print them on the screen in the format: 
// "My name is X, I am X years old, I am a X, and my phone number is XX" 
// (Consider what type to use for the phone number, e.g., "010-12345")
string name = "Chelsea";
string gender = "female";
int age = 18;
string phoneNumber = "61-0456136020";
Console.WriteLine($"my name is {name}, I am {age} years old, I am {gender} and my phone number is {phoneNumber}");

Console.WriteLine("Enter \"E\" to exit program...");
while (Console.ReadKey(false).Key != ConsoleKey.E)
{
};
