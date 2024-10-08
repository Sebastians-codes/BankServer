namespace TcpClientServer;

public class Pin(IUserInterface userInterface, IUserInteraction userInteraction)
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IUserInteraction _userInteraction = userInteraction;

    public string Get() =>
        ValidatePin();

    private string ValidatePin()
    {
        do
        {
            _userInterface.Write("Exit to quit\nEnter a pin 4 integers long -> ");
            string input = _userInteraction.ReadLine();

            if (input.ToLowerInvariant() == "exit")
            {
                return "q";
            }

            if (int.TryParse(input, out _) && input.Length == 4)
            {
                return input;
            }

            _userInterface.Clear();
            _userInterface.WriteLine("The pin must be 4 intergers long try again.");

        } while (true);
    }
}
