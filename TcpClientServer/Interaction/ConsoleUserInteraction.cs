namespace TcpClientServer;

public class ConsoleUserInteraction : IUserInteraction
{
    public string ReadLine() =>
        Console.ReadLine();

    public char ReadKey(bool print) =>
        Console.ReadKey(print).KeyChar;
}
