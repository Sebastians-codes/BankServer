using Npgsql;

namespace TcpServer;

public class PgSqlRepo
{
    private readonly string _connectionString = "Server=localhost;Port=5432;Database=tcptest;Username=zerq;Password=Testar1234";

    public Customer? GetAccount(Customer customer)
    {
        string queryCommand = "SELECT first_name, last_name, country.name, city.name, " +
                              "street, email, ssn, pin FROM customer " +
                              "INNER JOIN country on customer.country_id = country.id " +
                              "INNER JOIN city on customer.city_id = city.id " +
                              $"WHERE ssn = '{customer.Ssn}' AND pin = '{customer.Pin}' " +
                              $"AND closed = false;";

        using NpgsqlConnection connection = new(_connectionString);
        using NpgsqlCommand command = new(queryCommand, connection);

        connection.Open();

        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            string firstName = reader.GetString(reader.GetOrdinal("first_name"));
            string lastName = reader.GetString(reader.GetOrdinal("last_name"));
            string email = reader.GetString(reader.GetOrdinal("email"));
            string country = reader.GetString(reader.GetOrdinal("country.name"));
            string city = reader.GetString(reader.GetOrdinal("city.name"));
            string street = reader.GetString(reader.GetOrdinal("street"));
            string ssn = reader.GetString(reader.GetOrdinal("ssn"));
            string pin = reader.GetString(reader.GetOrdinal("pin"));

            return new Customer(firstName, lastName, email, country, city, street, ssn, pin);
        }

        return null;
    }

    public bool CustomerExist(Customer customer)
    {
        using NpgsqlConnection connection = new(_connectionString);
        using NpgsqlCommand command = new("select count(*) from account " +
                                          $"where ssn = '{customer.Ssn}';", connection);

        connection.Open();

        using NpgsqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            return reader.GetInt32(0) != 0;
        }

        return false;
    }

    public void InsertCustomer(Customer customer)
    {
        string queryCommand = "INSERT INTO account(first_name, last_name, " +
                              "ssn, email, country_id, city_id, street, pin) " +
                              "VALUES (@first_name, @last_name, @ssn, @email, " +
                              "@country_id, @city_id, @street, @pin);";

        using NpgsqlConnection connection = new(_connectionString);
        using NpgsqlCommand command = new(queryCommand, connection);

        command.Parameters.AddWithValue("@first_name", customer.FirstName);
        command.Parameters.AddWithValue("@last_name", customer.LastName);
        command.Parameters.AddWithValue("@ssn", customer.Ssn);
        command.Parameters.AddWithValue("@email", customer.Email);
        command.Parameters.AddWithValue("@country_id", customer.CountryId);
        command.Parameters.AddWithValue("@city_id", customer.CityId);
        command.Parameters.AddWithValue("@street", customer.Street);
        command.Parameters.AddWithValue("@pin", customer.Pin);

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
