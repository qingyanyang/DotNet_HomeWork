// 15 > Method with out Parameter Practice:
// Prompt the user separately to input a username and password.
// Write a method to check whether the input is correct.
// Return a login result to the user, and also return a separate login message.
// If the username is incorrect, in addition to the login result, 
// also return a message saying "Username is incorrect".


string? userName = null;
string? password = null;

Console.WriteLine("enter username:");
while (userName == null) 
{
    userName = Console.ReadLine();
}

Console.WriteLine("enter password:");
while (password == null)
{
    password = Console.ReadLine();
}

CorrectInput(userName!, password!, out LoginResult loginRes);

loginRes.PrintLoginResult();

void CorrectInput(string username, string password, out LoginResult result)
{
    if (username == "admin")
    {
        if(password == "8888")
        {
            result = new LoginResult(true, new LoginInfo(username, password),null);
        }
        else
        {
            result = new LoginResult(false, null, "Password is not correct!");
        }
    }
    else
    {
        result = new LoginResult(false, null, "Username is not existing!");
    }
}

class LoginResult
{
    public bool CorrectInput { get; set; }
    public LoginInfo? LoginInfo { get; set; } 
    public string? ErrorMessage { get; set; }

    public LoginResult(bool correctInput, LoginInfo? loginInfo = null, string? errorMessage = null)
    {
        CorrectInput = correctInput;
        LoginInfo = loginInfo;
        ErrorMessage = errorMessage;
    }

    public void PrintLoginResult() { 

        Console.WriteLine($"correctInput: {CorrectInput}");
        if (LoginInfo != null)
        {
            Console.WriteLine("login info:");
            Console.WriteLine($"username: {LoginInfo.Username}");
            Console.WriteLine($"password: {LoginInfo.Username}");
        }

        if (ErrorMessage != null)
        {
            Console.WriteLine($"errorMessage: {ErrorMessage}");
        }
    }
}
class LoginInfo 
{ 
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginInfo(string username, string password)
    {
        Username = username;
        Password = password;
    }
}