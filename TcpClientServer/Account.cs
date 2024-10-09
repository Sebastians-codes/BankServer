using System.Text;

namespace TcpClientServer;

public abstract class Account
{
    public int Number { get; init; }
    public decimal Balance { get; private set; }
    private List<Movement> _transactions;

    public Account(int number, List<Movement> transactions)
    {
        Number = number;
        _transactions = transactions;

        InitializeBalance();
    }

    public string GetHistory()
    {
        var reversed = new List<Movement>(_transactions.Count);
        int index = 0;

        for (int i = _transactions.Count - 1; i > 0; i--)
        {
            reversed[index++] = _transactions[i];
        }

        StringBuilder sb = new();
        decimal balance = 0;

        foreach (var transaction in reversed)
        {
            balance += transaction.Amount;
            sb.Append($"Date: {transaction.Date}, Transaction: {transaction.Amount}, Balance: {balance}\n");
        }

        return sb.ToString();
    }

    public void AddTransaction(Movement movement)
    {
        Balance += movement.Amount;
        _transactions.Add(movement);
    }

    private void InitializeBalance()
    {
        decimal balance = 0;

        if (_transactions.Count < 1)
        {
            Balance = 0;
            return;
        }

        foreach (var transaction in _transactions)
        {
            balance += transaction.Amount;
        }

        Balance = balance;
    }
}
