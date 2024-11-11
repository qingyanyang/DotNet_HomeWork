// ## Advanced Assignment
// Develop a simple console game. The game should include the following parts:

// **Requirements:**
// 1. Game Header: First, draw the game title and description.
// 2. Initialize Map: Load map resources and convert numbers in an integer array to special strings 
//    displayed in the console. This step initializes the map.
// 3. Draw Map: Display the initialized map in the console, showing the player's position 
//    and special events.
// 4. Game Logic: Define the rules of the game, handle player movement, and trigger events.

//**Game Rules:**
//• Stepping on Player B: When Player A steps on Player B, Player B moves back 6 spaces.
//• Stepping on a Mine: When a player steps on a mine, they move back 6 spaces.
//• Stepping on a Time Tunnel: When a player steps on a time tunnel, they move forward 10 spaces.
//• Stepping on a Lucky Wheel: The lucky wheel has two possibilities:

//1. Swap positions.
//2. Bomb the opponent, causing them to move back 6 spaces.

//• Stepping on a Pause: Skip one turn.
//• Stepping on a Blank Space: Do nothing.

GameManager gameManager = new GameManager();

gameManager.LoadGameHeader();

ConsoleKey inputKey;
do
{
    inputKey = Console.ReadKey(false).Key;

    if (inputKey != ConsoleKey.Y && inputKey != ConsoleKey.N)
    {
        Console.WriteLine("\nInvalid character! Only 'Y' and 'N' are accepted.");
    }

} while (inputKey != ConsoleKey.Y && inputKey != ConsoleKey.N);

if (inputKey == ConsoleKey.Y)
{
    bool pauseA = false;
    bool pauseB = false;
    bool wonA = false;
    bool wonB = false;

    gameManager.ClearPanel();
    gameManager.InitializeMap();
    // press any key to continue
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();

    while (true)
    {
        if (!pauseA)
        {
            int jumpStepsA = gameManager.ThrowDice();
            Console.WriteLine($"A throw dice:{jumpStepsA}");
            // press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            
            gameManager.Forward(jumpStepsA, "A", out wonA, out wonB);
            gameManager.CheckWon(wonA, wonB);
            gameManager.ClearPanel();
            gameManager.DisplayMap();
            gameManager.TriggerRuleAndReloadMap(out pauseA, out pauseB, out wonA, out wonB);
            gameManager.CheckWon(wonA,  wonB);
            gameManager.ClearPanel();
            gameManager.DisplayMap();
            // press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        else{
            // press any key to continue
            Console.WriteLine("Pause A for one turn, press any key to continue...");
            Console.ReadKey();
        }

        if (!pauseB)
        {
            int jumpStepsB = gameManager.ThrowDice();
            Console.WriteLine($"B throw dice:{jumpStepsB}");
            // press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            gameManager.Forward(jumpStepsB, "B", out wonA, out wonB);
            gameManager.CheckWon(wonA, wonB);
            gameManager.TriggerRuleAndReloadMap(out pauseA, out pauseB, out wonA, out wonB);
            gameManager.CheckWon( wonA,  wonB);
            gameManager.ClearPanel();
            gameManager.DisplayMap();
        }
        else
        {
            // press any key to continue
            Console.WriteLine("Pause B for one turn, press any key to continue...");
            Console.ReadKey();
        }
    }
}



class GameManager
{
    Map? Map;
    int AX = -1, AY = 0, BX = -1, BY = 0;

    public void LoadGameHeader()
    {
        Console.WriteLine(@"Welcome to the Adventure Board Game!In this game, you’ll take control of two players, Player A and Player B, as they navigate a board filled with surprises, traps, and challenges. The goal is to advance through the board while avoiding dangers and using special events to your advantage.
Game Rules:

	•	Step on Player B: If Player A steps on Player B, Player B will move back 6 spaces.
	•	Step on a Mine: If any player steps on a mine, they will move back 6 spaces.
	•	Step on a Time Tunnel: Step on a time tunnel, and the player will move forward 10 spaces.
	•	Step on a Lucky Wheel: The lucky wheel offers two possibilities:
	1.	Swap positions with the other player.
	2.	Bomb the opponent, causing them to move back 6 spaces.
	•	Step on a Pause: The player will skip their next turn.
	•	Step on a Blank Space: Nothing happens.

Board Symbols:

	•	Player A: A
	•	Player B: B
	•	Mine: X
	•	Time Tunnel: O
	•	Lucky Wheel: L
	•	Pause: P
	•	Blank Space: []

Are you ready to take on the challenge?[Y/N]
");
    }
    public void InitializeMap()
    {
        Map = new Map();
        Map.DisplayMapGrid(null,null,null,null);
    }
    public void DisplayMap()
    {
        if (Map != null)
        {
            Map.DisplayMapGrid(AX, AY, BX, BY);
        }
    }
    public int ThrowDice()
    {
        Random rnd = new Random();
        // hard code: 10
        int jumpSteps = rnd.Next(1,11);
        return jumpSteps;
    }
    public void Forward(int jumpSteps, string role, out bool wonA, out bool wonB)
    {
        wonA = false;
        wonB = false;
        if (role == "A")
        {
            // 1,2,3,4,5,6,7,8,9,10
            // hard code: 10
            AY += (AX + jumpSteps) / 10;
            AX = (AX + jumpSteps) % 10;
            // check if reach end point
            if ((AX == 9 && AY == 9) || AY > 9)
            {
                wonA = true;
            }
        }
        if (role == "B")
        {
            BY += (BX + jumpSteps) / 10;
            BX = (BX + jumpSteps) % 10;
            if ((BX == 9 && BY == 9) || BY > 9)
            {
                wonB = true;
            }
        }
    }
    public void Backward(int backSteps, string role)
    {
        if (role == "A")
        {
            AY -= (AX - backSteps) < 0 ? 1 : 0;
            AX = (AX - backSteps + 10) % 10;
            
            if (AY < 0)
            {
                AY = 0;
                AX = 0;
            }
        }
        if (role == "B")
        {
            BY -= (BX - backSteps) < 0 ? 1 : 0;
            BX = (BX - backSteps + 10) % 10;

            if (BY < 0)
            {
                BY = 0;
                BX = 0;
            }
        }
    }
    public void TriggerRuleAndReloadMap(out bool pauseA, out bool pauseB, out bool wonA, out bool wonB)
    {
        //**Game Rules:**
        //• Stepping on Player B: When Player A steps on Player B, Player B moves back 6 spaces.
        //• Stepping on a Mine: When a player steps on a mine, they move back 6 spaces.
        //• Stepping on a Time Tunnel: When a player steps on a time tunnel, they move forward 10 spaces.
        //• Stepping on a Lucky Wheel: The lucky wheel has two possibilities:

        //1. Swap positions.
        //2. Bomb the opponent, causing them to move back 6 spaces.

        //• Stepping on a Pause: Skip one turn.
        //• Stepping on a Blank Space: Do nothing.

        
        pauseA = false;
        pauseB = false;
        wonA = false;
        wonB = false;

        // Stepping on Player B/A
        if (AX == BX && AY == BY)
        {
            //  Player A steps on Player B, Player B moves back 6 spaces
            Console.WriteLine("Stepping on Player B: Player A steps on Player B, Player B moves back 6 spaces.");
            // press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Backward(6, "B");
            return;
        }

        //• Stepping on a Mine
        if (Map!.MapGrid[AY, AX] == "X")
        {
            Console.WriteLine("Stepping on a Mine: Player A steps on a mine, move back 6 spaces.");
            // press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Backward(6, "A");
            return;
        }
        if (BX != -1 && Map!.MapGrid[BY, BX] == "X")
        {
            Console.WriteLine("Stepping on a Mine: Player B steps on a mine, move back 6 spaces.");
            // press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Backward(6, "B");
            return;
        }

        //• Stepping on a Time Tunnel
        if (Map!.MapGrid[AY, AX] == "O")
        {
            Console.WriteLine("Stepping on a Time Tunnel: Player A steps on a time tunnel, move forward 10 spaces.");
            // press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Forward(10, "A", out wonA, out wonB);
            return;
        }
        if (BX != -1 && Map!.MapGrid[BY, BX] == "O")
        {
            Console.WriteLine("Stepping on a Time Tunnel: Player B steps on a time tunnel, move forward 10 spaces.");
            // press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Forward(10, "B",out wonA, out wonB);
            return;
        }

        //• Stepping on a Lucky Wheel
        if (Map!.MapGrid[AY, AX] == "L" || BX != -1 && Map!.MapGrid[BY, BX] == "L")
        {

            Random ran = new Random();
            int i = ran.Next(0, 2);
            if (i == 0)
            {
                Console.WriteLine("Stepping on a Lucky Wheel: Bomb the opponent, causing them to move back 6 spaces.");
                // press any key to continue
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                if (Map!.MapGrid[AY, AX] == "L")
                {
                    Backward(6, "B");
                }
                else
                {
                    Backward(6, "A");
                }
                
            }
            else
            {
                //switch
                Console.WriteLine("Stepping on a Lucky Wheel:  Swap positions.");
                // press any key to continue
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                int tempX = AX;
                AX = BX;
                BX = tempX;
               
                int tempY = AY;
                AY = BY;
                BY = tempY;
            }

        }

        //• Stepping on a Pause
        if (Map!.MapGrid[AY, AX] == "P")
        {
            Console.WriteLine("Stepping on a Pause: Player A skip one turn.");
            // press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            pauseA = true;
            return;
        }
        if (BX != -1 && Map!.MapGrid[BY, BX] == "P")
        {
            Console.WriteLine("Stepping on a Pause: Player B skip one turn.");
            // press any key to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            pauseB = true;
            return;
        }

        //• Stepping on a Blank Space
        Console.WriteLine("Stepping on a Blank Space: Do nothing");
        // press any key to continue
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public void ClearPanel()
    {
        Console.Clear();
    }
    public void CheckWon( bool wonA,  bool wonB)
    {
        if (wonA)
        {
            Console.Clear();
            Console.WriteLine("A won!");
            Environment.Exit(0);
        }
        if (wonB)
        {
            Console.Clear();
            Console.WriteLine("B won!");
            Environment.Exit(0);
        }

    }
}

class Map
{
    public string[,] MapGrid = new string[10, 10];
    static Dictionary<int, string> numberToElementMapping = new Dictionary<int, string>();
    public Map()
    {
        numberToElementMapping.Add(1, "X");
        numberToElementMapping.Add(2, "O");
        numberToElementMapping.Add(3, "L");
        numberToElementMapping.Add(4, "P");
        numberToElementMapping.Add(5, "[]");

        Random rnd = new Random();

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {

                MapGrid[i, j] = numberToElementMapping[rnd.Next(1, 6)];

            }
        }

    }
    public void DisplayMapGrid(int? AX, int? AY, int? BX, int? BY)
    {
        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {

                if (AX == x && AY == y)
                {
                    Console.Write("A ");
                }
                else if (BX == x && BY == y)
                {
                    Console.Write("B ");
                }
                else
                {
                    Console.Write($"{MapGrid[y, x]} ");
                }
            }
            Console.WriteLine();
        }
    }
}

