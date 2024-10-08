using TcpProtocol;
using System.Text.Json;
using TcpClientServer;
using TcpClientServer.Server;

ClientServer server = new("127.0.0.1", 7120, "BIGFUCKINGSECRET!");
IUserInterface userInterface = new ConsoleUserInterface();
IUserInteraction userInteraction = new ConsoleUserInteraction();
LoginMenu loginMenu = new(userInterface, userInteraction);
Input input = new(userInterface, userInteraction);
CustomerFactory customerFactory = new(input, server);

bool loggedIn = false;
var loggedInCustomer = new Customer[1];

try
{
    server.Connect();
    while (true)
    {
        if (!loggedIn)
        {
            MessageType type = loginMenu.Show();

            if (type == MessageType.Login)
            {
                string ssn = input.GetSsn();
                string pin = input.GetPin();

                LoginCredentials customer = new(ssn, pin);
                (object Type, string Response) = server.SendMessage(type, JsonSerializer.Serialize(customer));

                switch (Type)
                {
                    case MessageType.Ok:
                        loggedIn = true;
                        loggedInCustomer[0] = JsonSerializer.Deserialize<Customer>(Response);
                        Console.WriteLine(loggedInCustomer[0].ToString());
                        break;
                    case MessageType.Err:
                        Console.WriteLine("Invalid Credentials");
                        break;
                }
            }
            else if (type == MessageType.CreateLogin)
            {
                (object Type, string Response) =
                    server.SendMessage(type, JsonSerializer.Serialize(customerFactory.Make()));

                switch (Type)
                {
                    case MessageType.Ok:
                        Console.WriteLine("Account created");
                        break;
                    case MessageType.Err:
                        Console.WriteLine("Account not created");
                        break;
                }
            }
        }

        if (loggedIn)
        {
            Console.Write("Enter amount to deposit -> ");
            string amount = Console.ReadLine();
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}