// 2 > Arithmetic Operator Practice:
// The price of a T-shirt in a store is 35 yuan each, 
// and the price of trousers is 120 yuan each. 
// Xiao Ming bought 3 T-shirts and 2 pairs of trousers in the store.
// Please calculate and display how much money Xiao Ming should pay.
decimal tShirtPrice = 35m;
decimal trousersPrice = 120m;
decimal totalPrice = 3 * tShirtPrice + 2 * trousersPrice;
Console.WriteLine("total price is: {0}", totalPrice);

Console.WriteLine("Enter \"E\" to exit program...");
while (Console.ReadKey(false).Key != ConsoleKey.E)
{
};