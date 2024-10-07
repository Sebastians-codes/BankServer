using Npgsql;

namespace TcpServer;

public class PgSqlRepo
{
    private readonly string _connectionString = "Server=localhost;Port=5432;Database=tcptest;Username=zerq;Password=Testar1234";

    public Customer? GetAccount(Customer account)
    {
        using NpgsqlConnection connection = new(_connectionString);
        using NpgsqlCommand command = new("select ssn, pin from account " +
                                          $"where ssn = '{account.Ssn}' and " +
                                          $"pin = '{account.Pin}';", connection);

        connection.Open();

        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            string firstName = reader.GetString(reader.GetOrdinal("first_name"));
            string lastName = reader.GetString(reader.GetOrdinal("last_name"));
            string email = reader.GetString(reader.GetOrdinal("email"));
            string country = reader.GetString(reader.GetOrdinal(""))
            string ssn = reader.GetString(reader.GetOrdinal("ssn"));
            string pin = reader.GetString(reader.GetOrdinal("pin"));

            return new Customer(ssn, pin);
        }

        return null;
    }

    public bool CustomerExist(Customer account)
    {
        using NpgsqlConnection connection = new(_connectionString);
        using NpgsqlCommand command = new("select count(*) from account " +
                                          $"where ssn = '{account.Ssn}';", connection);

        connection.Open();

        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            return reader.GetInt32(0) != 0;
        }

        return false;
    }

    public void InsertCustomer(Customer account)
    {
        using NpgsqlConnection connection = new(_connectionString);
        using NpgsqlCommand command = new("insert into account(ssn,pin) " +
                                          $"values (@ssn, @pin);", connection);

        command.Parameters.AddWithValue("@ssn", account.Ssn);
        command.Parameters.AddWithValue("@pin", account.Pin);

        connection.Open();
        command.ExecuteNonQuery();
    }

    public void InsertTransaction(string ssn, string amount)
    {
        using NpgsqlConnection connection = new(_connectionString);
        using NpgsqlCommand command = new("insert into transaction(account, amount) " +
                                          $"values (@account, @amount)", connection);

        command.Parameters.AddWithValue("@account", ssn);
        command.Parameters.AddWithValue("@amount", amount);

        connection.Open();
        command.ExecuteNonQuery();
    }
}
