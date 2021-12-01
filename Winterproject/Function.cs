public class Function
{
    //WriteColor Methods (allows for easier & faster writing with color)
    public void WriteLineColor(string text = "This method WriteLines text in color", ConsoleColor color = ConsoleColor.Green)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public void WriteColor(string text = "This method Writes text in color", ConsoleColor color = ConsoleColor.Green)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public void WriteLineDuoColor(string preText = "This method WriteLines", ConsoleColor preColor = ConsoleColor.Green, string text = " certain text in color", ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = preColor;
        Console.Write(preText);
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public void WriteDuoColor(string preText = "This method Writes", ConsoleColor preColor = ConsoleColor.Green, string text = " certain text in color", ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = preColor;
        Console.Write(preText);
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public void WriteLineTripleColor(string preText = "This method WriteLines", ConsoleColor preColor = ConsoleColor.Green, string text = "three", ConsoleColor color = ConsoleColor.Red, string postText = " text in different colors", ConsoleColor postColor = ConsoleColor.White)
    {
        Console.ForegroundColor = preColor;
        Console.Write(preText);
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = postColor;
        Console.WriteLine(postText);
        Console.ForegroundColor = ConsoleColor.White;
    }
    public void WriteTripleColor(string preText = "This method Writes", ConsoleColor preColor = ConsoleColor.Green, string text = "three", ConsoleColor color = ConsoleColor.Red, string postText = " text in different colors", ConsoleColor postColor = ConsoleColor.White)
    {
        Console.ForegroundColor = preColor;
        Console.Write(preText);
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = postColor;
        Console.Write(postText);
        Console.ForegroundColor = ConsoleColor.White;
    }

    //Fancy ReadLine
    public void ReadLineNotice()
    {
        WriteLineColor("Press Enter to continue...", ConsoleColor.DarkYellow);
        Console.ReadLine();
    }

    //Delay
    public void DelayWidget(float delay = 1)
    {
        delay *= 1000;
        Console.Write(".");
        Thread.Sleep((int)delay);
        Console.Write(".");
        Thread.Sleep((int)delay);
        Console.WriteLine(".");
        Thread.Sleep((int)delay);
    }
}