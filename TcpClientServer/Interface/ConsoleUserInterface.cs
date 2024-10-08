namespace TcpClientServer;

public class ConsoleUserInterface : IUserInterface
{
    public void Clear() =>
        Console.Clear();

    public void Write(string messsage) =>
        Console.Write(messsage);

    public void WriteLine(string message) =>
        Console.WriteLine(message);
}
