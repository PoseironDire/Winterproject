
using System.Text.Json;
using System.Text.Json.Serialization;

public class Player
{
    Program program = new Program();
    Card card = new Card();
    Function function = new Function();

    //Properties;
    [JsonPropertyName("Persona")]
    public string Name { get; set; }
    [JsonPropertyName("DMG")]
    public int DMG { get; set; }
    [JsonPropertyName("SPD")]
    public int SPD { get; set; }
    [JsonPropertyName("HP")]
    public int HP { get; set; }
    public int leaderRANK = 1;
    public int leaderENERGY = 1;
    public int turn = 0;

    //Locations
    public int deck;
    public int hand = 0;
    public int board = 0;
    public int idlePile = 0;

    //Extra
    public int diceThrow = 0;
    public bool starts = false;
    public bool plays = false;
    public bool chooses = false;
    public ConsoleColor color;

    public void Turn()
    {
        plays = true;
        function.WriteLineDuoColor(Name + "'s", color, " Turn:");
        program.Move();
        Cycle();
        function.ReadLineNotice();
        plays = false;
    }
    public void DrawStartingHandStart()
    {
        deck -= 5;
        hand += 5;
    }
    public void DrawStartingHandWait()
    {
        deck -= 4;
        hand += 4;
    }
    void Cycle()
    {
        if (turn > 0)
        {
            program.Move();
            if (leaderRANK < 5)
            {
                leaderRANK++;
                function.WriteLineDuoColor(Name + "'s", color, " RANK was Increased to: " + leaderRANK);
                function.DelayWidget(0.01f);
            }
            leaderENERGY = 1;
            function.WriteLineDuoColor(Name, color, " Gained 1 Leader ENERGY");
            function.DelayWidget(0.01f);
            card.Charge();
            function.WriteLineDuoColor(Name + "'s", color, " cards got Charged");
            function.DelayWidget(0.01f);
            hand++;
            function.WriteLineDuoColor(Name, color, " Drew 1 card");
            function.DelayWidget(0.01f);
            if (hand > 5)
            {
                function.WriteLineDuoColor(Name, color, " Has above 5 cards in their Hand & must therefore Discard 1 card:");
                function.DelayWidget(0.01f);
            }
        }
        turn++;
    }
}