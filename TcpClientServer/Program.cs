using TcpProtocol;
using System.Text.Json;
using TcpClientServer;
using TcpClientServer.Server;

ClientServer server = new("127.0.0.1", 7120, "BIGFUCKINGSECRET!");

bool loggedIn = false;
var loggedInCustomer = new Customer[1];

try
{
    server.Connect();
    while (true)
    {
        if (!loggedIn)
        {
            Console.Write("1 - LogIn\n2 - CreateAccount\n-> ");
            string choice = Console.ReadLine();
            string ssn = Console.ReadLine();
            string pin = Console.ReadLine();
            LoginCredentials customer = new(ssn, pin);
            MessageType type = choice switch
            {
                "1" => MessageType.Login,
                "2" => MessageType.CreateLogin,
                "3" => MessageType.Country,
                "4" => MessageType.City
            };


            if (type == MessageType.Login)
            {
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
                int country = 164;
                int city = 331;
                Console.Write("street -> ");
                string street = Console.ReadLine();

                CreateCustomer newCustomer = new(firstName, lastName, email, country, city, street, ssn, pin);

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
            else if (type == MessageType.Country)
            {
                (object Type, string Response) = server.SendMessage(type, "");

                List<Country> countries = JsonSerializer.Deserialize<List<Country>>(Response);

                foreach (var item in countries)
                {
                    Console.WriteLine($"{item.Id}, {item.Name}, {item.Code}");
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