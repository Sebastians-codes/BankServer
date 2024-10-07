using Client1.Server;
using TcpProtocl;
using Client1;
using System.Text.Json;

ClientServer server = new("127.0.0.1", 7120, "BIGFUCKINGSECRET!");

bool loggedIn = false;
var loggedInAccount = new Account[1];

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
            Account account = new Account(ssn, pin);
            MessageType type = choice switch
            {
                "1" => MessageType.Login,
                "2" => MessageType.CreateLogin
            };

            (object Type, string Response) = server.SendMessage(type, JsonSerializer.Serialize(account));

            if (type == MessageType.Login)
            {
                switch (Type)
                {
                    case MessageType.Ok:
                        loggedIn = true;
                        loggedInAccount[0] = account;
                        break;
                    case MessageType.Err:
                        Console.WriteLine("Invalid Credentials");
                        break;
                }
            }
            else if (type == MessageType.CreateLogin)
            {
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
            Transaction transaction = new(loggedInAccount[0], amount);

            (object Type, string Response) = server.SendMessage(MessageType.Command, JsonSerializer.Serialize(transaction), CommandType.Transaction);
        }
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}