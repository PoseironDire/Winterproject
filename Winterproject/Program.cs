using System.Text.Json;

class Program
{
    static Function function = new Function();
    static Random generator = new Random();

    //Players
    public static Player[] players = { new Player(), new Player() };
    //Player Names
    public static string json = File.ReadAllText("names.json");
    public static List<Player> nameList = JsonSerializer.Deserialize<List<Player>>(json);

    static void Main(string[] args)
    {
        //Generate Players
        for (int i = 0; i < players.Length; i++)
        {
            int name = generator.Next(nameList.Count);
            //Decide HP
            players[i].HP = nameList[name].HP;
            //Decide Name
            players[i].Name = nameList[name].Name;
            nameList.RemoveAt(name);
            //Decide Deck Size
            players[i].deck = generator.Next(31, 41);
            //Decide Color
            if (i == 0)
                players[i].color = ConsoleColor.Yellow;
            if (i == 1)
                players[i].color = ConsoleColor.Blue;
        }
        //Write Starting "Screen"
        function.WriteLineTripleColor(players[0].Name + " HP: " + players[0].HP, players[0].color, " --VS-- ", ConsoleColor.White, players[1].Name + " HP: " + players[1].HP, players[1].color);
        function.ReadLineNotice();
        Console.Clear();

        // players[0].otherPlayer = players[1];
        // players[1].otherPlayer = players[0];


        //Decide Which Player Goes First
        if (players[0].HP != players[1].HP)
        {
            if (players[0].HP < players[1].HP)
            {
                players[0].chooses = true;
            }
            else if (players[1].HP < players[0].HP)
            {
                players[1].chooses = true;
            }
            Player lchooses = players.First(x => x.chooses == true);
            Player llooks = players.First(x => x.chooses == false);

            function.WriteLineDuoColor(lchooses.Name, lchooses.color, " Has less HP and thus may Choose who goes first!");
            function.ReadLineNotice();
            string answer;
            bool validAnswer = false;
            bool retry = false;
            while (!validAnswer)
            {
                Console.Clear();
                function.WriteDuoColor(lchooses.Name, lchooses.color, ", who goes first? (");
                function.WriteTripleColor(lchooses.Name, lchooses.color, " / ", ConsoleColor.White, llooks.Name, llooks.color);
                Console.WriteLine(")");
                if (!validAnswer && retry)
                    function.WriteLineDuoColor("That's not a current Leader name,", ConsoleColor.Red, " try again!");
                answer = Console.ReadLine().ToLower();
                if (answer == lchooses.Name.ToLower() || answer == "me")
                {
                    lchooses.starts = true;
                    validAnswer = true;
                }
                else if (answer == llooks.Name.ToLower() || answer == "not me")
                {
                    llooks.starts = true;
                    validAnswer = true;
                }
                else
                {
                    retry = true;
                    validAnswer = false;
                }
            }
        }
        else
        {
            Console.WriteLine("Both Leaders Have " + players[0].HP + " HP & must therefore throw a dice to deside who goes first");
            function.ReadLineNotice();
            while (players[0].diceThrow == players[1].diceThrow)
            {
                for (var i = 0; i < 2; i++)
                {
                    players[i].diceThrow = generator.Next(6) + 1;
                    ConsoleColor throwColor = ConsoleColor.White;
                    if (players[1].diceThrow > players[0].diceThrow && i == 1)
                        throwColor = ConsoleColor.Green;
                    else if (players[1].diceThrow < players[0].diceThrow && i == 1)
                        throwColor = ConsoleColor.Red;
                    else if (players[1].diceThrow == players[0].diceThrow && i == 1)
                        throwColor = ConsoleColor.DarkGray;
                    function.WriteLineTripleColor(players[i].Name, players[i].color, " got: ", ConsoleColor.White, players[i].diceThrow.ToString(), throwColor);
                    function.ReadLineNotice();
                }
            }
            if (players[0].diceThrow > players[1].diceThrow)
            {
                players[0].starts = true;
            }
            else if (players[0].diceThrow < players[1].diceThrow)
            {
                players[1].starts = true;
            }
        }
        if (players[0].starts == true)
        {
            function.WriteLineDuoColor(players[0].Name, ConsoleColor.Yellow, " goes first!");
            players[0].DrawStartingHandStart();
            players[1].DrawStartingHandWait();
        }
        if (players[1].starts == true)
        {
            function.WriteLineDuoColor(players[1].Name, ConsoleColor.Blue, " goes first!");
            players[1].DrawStartingHandStart();
            players[0].DrawStartingHandWait();
        }
        function.ReadLineNotice();
        Console.Clear();

        Player lStart = players.First(x => x.starts == true);
        Player lWait = players.First(x => x.starts == false);
        bool play = true;
        while (play)
        {
            Round();
            lStart.Turn();
            lWait.Turn();
        }
    }

    static int round = 0;
    static void Round()
    {
        Console.Clear();
        round++;
        Console.WriteLine("Round: " + round);
        function.DelayWidget(0.01f);
    }

    public void Move()
    {
        Player lStart = players.First(x => x.starts == true);
        Player lWait = players.First(x => x.starts == false);

        if (players[0].plays)
        {
            function.WriteLineDuoColor(players[0].Name, players[0].color, " do you wish to Intend a Move? (Yes / No)");
            string answer = Console.ReadLine().ToLower();
            if (answer == "yes" || answer == "y")
                function.WriteLineDuoColor(players[0].Name, players[0].color, " Ok'ed", ConsoleColor.Magenta);
            else
                function.WriteLineDuoColor(players[0].Name, players[0].color, " Passed", ConsoleColor.Magenta);
            function.WriteLineDuoColor(players[1].Name, players[1].color, " do you wish to Intend a Move? (Yes / No)");
            answer = Console.ReadLine().ToLower();
            if (answer == "yes" || answer == "y")
                function.WriteLineDuoColor(players[1].Name, players[1].color, " Ok'ed", ConsoleColor.Magenta);
            else
                function.WriteLineDuoColor(players[1].Name, players[1].color, " Passed", ConsoleColor.Magenta);
        }
        else if (players[1].plays)
        {
            function.WriteLineDuoColor(players[1].Name, players[1].color, " do you wish to Intend a Move? (Yes / No)");
            string answer = Console.ReadLine().ToLower();
            if (answer == "yes" || answer == "y")
                function.WriteLineDuoColor(players[1].Name, players[1].color, " Ok'ed", ConsoleColor.Magenta);
            else
                function.WriteLineDuoColor(players[1].Name, players[1].color, " Passed", ConsoleColor.Magenta);
            function.WriteLineDuoColor(players[0].Name, players[0].color, " do you wish to Intend a Move? (Yes / No)");
            answer = Console.ReadLine().ToLower();
            if (answer == "yes" || answer == "y")
                function.WriteLineDuoColor(players[0].Name, players[0].color, " Ok'ed", ConsoleColor.Magenta);
            else
                function.WriteLineDuoColor(players[0].Name, players[0].color, " Passed", ConsoleColor.Magenta);
        }
    }
}

