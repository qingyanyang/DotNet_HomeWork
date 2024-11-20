// 5 > if Statement Practice:
// Prompt the user to enter a username, and then prompt for a password. 
// If the username is "admin" and the password is "88888", display a message indicating it's correct. 
// Otherwise, if the username is not "admin", inform the user that the username does not exist. 
// If the username is "admin" but the password is incorrect, display a message indicating the password is incorrect.
Console.WriteLine("Please enter username:");
string? userName = Console.ReadLine();
Console.WriteLine("Please enter password:");
string? password = Console.ReadLine();
if (userName == "admin")
{
    if(password!= "88888")
    {
        Console.WriteLine("Password is wrong!");
    }
}
else
{
    Console.WriteLine("User does not exist!");
}

Console.WriteLine("Enter \"E\" to exit program...");
while (Console.ReadKey(false).Key != ConsoleKey.E)
{
};