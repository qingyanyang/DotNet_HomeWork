// 11 > Nested Loop Practice:
// Print the multiplication table.

for (int i = 1; i<10; i++) {
    for (int j = 1; j <=i; j++)
    {
        Console.Write($"{j}x{i} ");
    }
    Console.WriteLine("");
}