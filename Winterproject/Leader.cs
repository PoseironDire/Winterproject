public class Leader
{
    Card card = new Card();
    Functions functions = new Functions();

    //Properties
    public int hp;
    public string name;
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
    public bool chooses = false;
    public ConsoleColor color;

    public void Turn()
    {
        functions.WriteLineDuoColor(name + "'s", color, " Turn:");
        functions.ReadLineNotice();
        Cycle();
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
    public void Cycle()
    {
        if (turn > 0)
        {
            if (leaderRANK < 5)
            {
                leaderRANK++;
                functions.WriteLineDuoColor(name + "'s", color, " RANK was Increased by 1!");
            }
            leaderENERGY = 1;
            functions.WriteLineDuoColor(name + "'s", color, " ENERGY was Refreshed!");
            card.Charge();
            functions.WriteLineDuoColor(name + "'s", color, " cards were Charged!");
            hand++;
            functions.WriteLineDuoColor(name, color, " Drew 1 card!");
            if (hand > 5)
            {
                functions.WriteLineDuoColor(name, color, " Has more than 5 cards in their Hand, Discard 1 card:");
            }
            functions.ReadLineNotice();
        }
        turn++;
    }
}