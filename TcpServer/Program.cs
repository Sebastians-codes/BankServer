using TcpServer;

Server server = new("127.0.0.1", 7120, "BIGFUCKINGSECRET!");

try
{
    server.Start();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
