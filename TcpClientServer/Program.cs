using TcpClientServer;
using TcpClientServer.Server;

ClientServer server = new("127.0.0.1", 7120, "BIGFUCKINGSECRET!");
IUserInterface userInterface = new ConsoleUserInterface();
IUserInteraction userInteraction = new ConsoleUserInteraction();
Input input = new(userInterface, userInteraction);
CustomerFactory customerFactory = new(input, server);
LoginMenu loginMenu = new(userInterface, userInteraction, input, customerFactory, server);

bool loggedIn = false;
Customer? loggedInCustomer;

try
{
    server.Connect();
    while (true)
    {
        if (!loggedIn)
        {
            Customer? customer = loginMenu.Show();

            if (customer != null)
            {
                loggedIn = true;
                loggedInCustomer = customer;
            }
        }

        if (loggedIn)
        {
            Console.Write("Enter amount to deposit -> ");
            string amount = Console.ReadLine()!;
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}