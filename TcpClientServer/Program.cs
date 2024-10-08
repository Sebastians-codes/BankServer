using TcpProtocol;
using System.Text.Json;
using TcpClientServer;
using TcpClientServer.Server;

ClientServer server = new("127.0.0.1", 7120, "BIGFUCKINGSECRET!");
IUserInterface userInterface = new ConsoleUserInterface();
IUserInteraction userInteraction = new ConsoleUserInteraction();
LoginMenu loginMenu = new(userInterface, userInteraction);
Ssn ssn = new(userInterface, userInteraction);
Pin pin = new(userInterface, userInteraction);

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

            string ssN = ssn.Get();
            string piN = pin.Get();

            if (type == MessageType.Login)
            {
                LoginCredentials customer = new(ssN, piN);
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
                Console.Write("firstName -> ");
                string firstName = Console.ReadLine();
                Console.Write("lastName -> ");
                string lastName = Console.ReadLine();
                Console.Write("email -> ");
                string email = Console.ReadLine();
                List<Country> countries = JsonSerializer.Deserialize<List<Country>>(server.SendMessage(MessageType.Data, "country").Content)!;
                int country = 164;
                int city = 331;
                Console.Write("street -> ");
                string street = Console.ReadLine();

                DbCustomer newCustomer = new(firstName, lastName, email, street, ssN, piN, country, city);

                (object Type, string Response) = server.SendMessage(type, JsonSerializer.Serialize(newCustomer));

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