namespace TcpClientServer;

public class ConsoleUserInteraction : IUserInteraction
{
    public string ReadLine() =>
        Console.ReadLine();
}
