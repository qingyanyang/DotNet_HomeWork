// 6 > switch Case Practice:
// At the end of the year, Li Si's performance rating will affect his salary adjustment:
// - If rated A, his salary increases by 500 yuan.
// - If rated B, his salary increases by 200 yuan.
// - If rated C, his salary remains unchanged.
// - If rated D, his salary decreases by 200 yuan.
// - If rated E, his salary decreases by 500 yuan.
// Assume Li Si's original salary is 5000 yuan. 
// Prompt the user to enter Li Si's performance rating, then display his salary for the next year.
Console.WriteLine("Enter the evaluation of SiLi:");
string? evaluation = Console.ReadLine();
decimal originPay = 5000;

switch (evaluation)
{
    case "A":
        originPay += 500;
        break;
    case "B":
        originPay += 200;
        break;
    case "D":
        originPay -= 200;
        break;
    case "E":
        originPay -= 500;
        break;
    default:
        Console.WriteLine("wrong level");
        break;
}

decimal futurePay = originPay;

Console.WriteLine($"wage for next year: {futurePay}");

Console.WriteLine("Enter \"E\" to exit program...");
while (Console.ReadKey(false).Key != ConsoleKey.E)
{
};