namespace TcpClientServer;

public class CitySelector(IUserInterface userInterface, IUserInteraction userInteraction)
{
    private readonly IUserInterface _userInterface = userInterface;
    private readonly IUserInteraction _userInteraction = userInteraction;

    public int Get(List<City> cities)
    {
        char[] pointer = new char[cities.Count];
        pointer[^1] = '>';
        int pointerIndex = pointer.Length - 1;

        do
        {
            _userInterface.Clear();

            for (int i = 0; i < pointer.Length; i++)
            {
                Console.WriteLine($"{pointer[i]} {cities[i].Name}");
            }

            char input = char.ToLowerInvariant(_userInteraction.ReadKey(true));

            if (input == 'q')
            {
                return 0;
            }

            if (input == 'e')
            {
                return cities[pointerIndex].Id;
            }

            if (input == 'k' && pointerIndex != 0)
            {
                pointer[pointerIndex] = '\0';
                pointer[--pointerIndex] = '>';
            }

            if (input == 'j' && pointerIndex != pointer.Length - 1)
            {
                pointer[pointerIndex] = '\0';
                pointer[++pointerIndex] = '>';
            }

        } while (true);
    }
}
