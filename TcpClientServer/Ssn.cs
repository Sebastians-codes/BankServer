namespace TcpClientServer;

public class Ssn(IUserInterface userInterface, IUserInteraction userInteraction)
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IUserInteraction _userInteraction = userInteraction;

    public string Get() =>
        ValidateBirthYear();

    private string ValidateBirthYear()
    {
        do
        {
            _userInterface.Write("Exit to Quit\nEnter your Social Security Number\nIn this format yyyy-mm-dd-xxxx\n-> ");
            string input = _userInteraction.ReadLine();

            if (input.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
            {
                return "q";
            }

            string[] parts = input.Split('-');
            string dateString = string.Join('-', parts[..^1]);
            string ssn = parts[^1];

            if (DateTime.TryParse(dateString, out DateTime date) &&
                date < DateTime.Now.AddYears(-18) &&
                date > DateTime.Now.AddYears(-130))
            {
                if (int.TryParse(ssn, out _) && ssn.Length == 4)
                {
                    return string.Join("", parts);
                }
            }
            _userInterface.Clear();
            _userInterface.WriteLine("Invalid BirthYear, try again'.");
        } while (true);
    }
}
