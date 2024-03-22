using EcommerceStore.Models;
using EcommerceStore.Services;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

public class Services : IServices
{
    SqlConnection Connection = new SqlConnection("Server=DESKTOP-L6DHQ4D;Database=EcommerceStore;Trusted_Connection=True; TrustServerCertificate=True");
    SqlCommand Command = new SqlCommand(); 

    public User Search(User user)
    {
        Command.Connection = Connection; 
        Command.CommandType = System.Data.CommandType.Text;
        Command.CommandText = "select * from Users where username=@Username and password=@Password";
        Command.Parameters.AddWithValue("@Username", user.Username);
        Command.Parameters.AddWithValue("@Password", user.Password);
        using (Connection)
        {
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
            User Found = new User();
            while (reader.Read())
            {
                Found.Id = reader.GetInt32(reader.GetOrdinal("id"));
                Found.Username = reader["username"].ToString();
                Found.Password = reader["password"].ToString();
                Found.Email = reader["email"].ToString();
                Found.Phone = reader["phone"].ToString();
                int Admin = reader.GetInt32(reader.GetOrdinal("id"));
                if (Admin == 0)
                    Found.IsAdmin = false;
                else
                    Found.IsAdmin = true;
            }
            return Found;
        }
    }

    public void CreateUser(User user)
    {
        SqlConnection Connection = new SqlConnection("Server=DESKTOP-L6DHQ4D;Database=EcommerceStore;Trusted_Connection=True; TrustServerCertificate=True");
        Command = Connection.CreateCommand();
        Command.CommandType = System.Data.CommandType.Text;
        Command.CommandText = "insert into Users values(@Username,@Password,@Email,@Phone,0)";
        Command.Parameters.AddWithValue("@Username", user.Username);
        Command.Parameters.AddWithValue("@Password", user.Password);
        Command.Parameters.AddWithValue("@Email", user.Email);
        Command.Parameters.AddWithValue("@Phone", user.Phone);
        using (Connection)
        {
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
        }
    }
    public  void CreateOrder(Order order,User user)
    {
      
        SqlConnection Connection = new SqlConnection("Server=DESKTOP-L6DHQ4D;Database=EcommerceStore;Trusted_Connection=True; TrustServerCertificate=True");
        Command = Connection.CreateCommand();
        Command.CommandType = System.Data.CommandType.Text;
        Command.CommandText = "INSERT INTO Orders (UserID, bagId, quantity) values(@UserId, @BagId, @quantity)";
        Command.Parameters.AddWithValue("@UserId", user.Id );
        Command.Parameters.AddWithValue("@BagId", order.BagId);
        Command.Parameters.AddWithValue("@quantity", order.Quantity);
        using (Connection)
        {
            Connection.Open();
            SqlDataReader reader = Command.ExecuteReader();
        }

    }
    public IEnumerable<Order> GetOrders()
    {
        List<Order> orders = new List<Order>();

        using (SqlConnection connection = new SqlConnection("Server=DESKTOP-L6DHQ4D;Database=EcommerceStore;Trusted_Connection=True; TrustServerCertificate=True"))
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM Orders", connection))
            {
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            BagId = reader.GetInt32(reader.GetOrdinal("bagId")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                            OrderStatus = reader.GetString(reader.GetOrdinal("Status")),
                            OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")).ToString("yyyy-MM-dd")
                        };

                        orders.Add(order);
                    }
                }
            }
        }

        return orders;
    }
}
