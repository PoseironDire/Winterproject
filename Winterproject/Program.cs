using System.Text.Json;

Functions functions = new Functions();
Random generator = new Random();

Leader[] leaders = { new Leader(), new Leader() };

string json = File.ReadAllText("names.json");
List<string> arrayName = JsonSerializer.Deserialize<List<string>>(json);

for (int i = 0; i < leaders.Length; i++)
{
    //Decide HP
    int[] arrayHp = { 15 };
    int decideHP = generator.Next(arrayHp.Length);
    leaders[i].hp = arrayHp[decideHP];
    //Decide Name
    int decideName = generator.Next(arrayName.Count);
    leaders[i].name = arrayName[decideName];
    arrayName.RemoveAt(decideName);
    //Decide Deck Size
    int decideDeckSize = generator.Next(31, 41);
    leaders[i].deck = decideDeckSize;
    //Decide Color
    if (i == 0)
        leaders[i].color = ConsoleColor.Yellow;
    if (i == 1)
        leaders[i].color = ConsoleColor.Blue;
}
functions.WriteLineTripleColor(leaders[0].name + " HP: " + leaders[0].hp, leaders[0].color, " --VS-- ", ConsoleColor.White, leaders[1].name + " HP: " + leaders[1].hp, leaders[1].color);
functions.ReadLineNotice();
Console.Clear();

if (leaders[0].hp != leaders[1].hp)
{
    if (leaders[0].hp < leaders[1].hp)
    {
        leaders[0].chooses = true;
    }
    else if (leaders[1].hp < leaders[0].hp)
    {
        leaders[1].chooses = true;
    }
    Leader lchooses = leaders.First(x => x.chooses == true);
    Leader llooks = leaders.First(x => x.chooses == false);

    functions.WriteLineDuoColor(lchooses.name, lchooses.color, " Has less HP and thus may Choose who goes first!");
    functions.ReadLineNotice();
    string answer = "bob";
    while (answer != lchooses.name.ToLower() && answer != llooks.name.ToLower())
    {
        Console.Clear();
        functions.WriteDuoColor(lchooses.name, lchooses.color, ", who goes first? (");
        functions.WriteTripleColor(lchooses.name, lchooses.color, " / ", ConsoleColor.White, llooks.name, llooks.color);
        System.Console.WriteLine(")");
        answer = Console.ReadLine().ToLower();
        if (answer == lchooses.name.ToLower())
        {
            lchooses.starts = true;
        }
        else if (answer == llooks.name.ToLower())
        {
            llooks.starts = true;
        }
        else
        {
            functions.WriteLineColor("That's not a current Leader name,", ConsoleColor.Red);
            functions.ReadLineNotice();
        }
    }
}
else
{
    Console.WriteLine("Both Leaders Have " + leaders[0].hp + " HP & must therefore throw a dice to deside who goes first");
    functions.ReadLineNotice();
    while (leaders[0].diceThrow == leaders[1].diceThrow)
    {
        for (var i = 0; i < 2; i++)
        {
            leaders[i].diceThrow = generator.Next(6) + 1;
            ConsoleColor throwColor = ConsoleColor.White;
            if (leaders[1].diceThrow > leaders[0].diceThrow && i == 1)
                throwColor = ConsoleColor.Green;
            else if (leaders[1].diceThrow < leaders[0].diceThrow && i == 1)
                throwColor = ConsoleColor.Red;
            else if (leaders[1].diceThrow == leaders[0].diceThrow && i == 1)
                throwColor = ConsoleColor.DarkGray;
            functions.WriteLineTripleColor(leaders[i].name, leaders[i].color, " got: ", ConsoleColor.White, leaders[i].diceThrow.ToString(), throwColor);
            functions.ReadLineNotice();
        }
    }
    if (leaders[0].diceThrow > leaders[1].diceThrow)
    {
        leaders[0].starts = true;
    }
    else if (leaders[0].diceThrow < leaders[1].diceThrow)
    {
        leaders[1].starts = true;
    }
}
if (leaders[0].starts == true)
{
    functions.WriteLineDuoColor(leaders[0].name, ConsoleColor.Yellow, " goes first!");
    leaders[0].DrawStartingHandStart();
    leaders[1].DrawStartingHandWait();
}
if (leaders[1].starts == true)
{
    functions.WriteLineDuoColor(leaders[1].name, ConsoleColor.Blue, " goes first!");
    leaders[1].DrawStartingHandStart();
    leaders[0].DrawStartingHandWait();
}
functions.ReadLineNotice();
Console.Clear();

int round = 0;
bool play = true;
while (play)
{
    round++;
    Leader lStart = leaders.First(x => x.starts == true);
    Leader lWait = leaders.First(x => x.starts == false);
    Console.WriteLine("Round: " + round);
    lStart.Turn();
    lWait.Turn();
}