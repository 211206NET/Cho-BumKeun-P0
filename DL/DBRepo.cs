using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
//using System.Linq;
namespace DL;

public class DBRepo : IRepo
{
    private string _connectionString;
    public DBRepo(string connectionString) {
        _connectionString = connectionString;
    }

    public void AddCustomer(Customer customerToAdd)
    {
        DataSet restoSet = new DataSet();
        string selectCmd = "SELECT * FROM UserAccount WHERE Id = -1";
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            using(SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCmd, connection))
            {
                dataAdapter.Fill(restoSet, "UserAccount");
                
                DataTable restoTable = restoSet.Tables["UserAccount"];
                DataRow newRow = restoTable.NewRow();
                
                newRow["Username"] = customerToAdd.UserName;
                newRow["Password"] = customerToAdd.Password ?? "";
                
                restoTable.Rows.Add(newRow);

                string insertCmd = $"INSERT INTO Restaurant (Username, Password) VALUES ('{customerToAdd.UserName}', '{customerToAdd.Password}')";

                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);

                dataAdapter.InsertCommand = cmdBuilder.GetInsertCommand();
  
                dataAdapter.Update(restoTable);
            }
        }
    }

    public void AddOrder(int storeId, int productId, string storeName, string productName, int quantity, decimal price, int userId, DateTime time)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "INSERT INTO Orders (StoreId, StoreName, ProductId, ProductName, Quantity, TotalPrice, UserId, Time) VALUES (@stoId, @stoname, @prodId, @prodname, @quantity, @totalprice, @userId, @time)";
    
            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = new SqlParameter("@stoId", storeId);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@stoName", storeName);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@prodId", productId);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@prodName", productName);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@quantity", quantity);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@totalprice", price*quantity);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@userId", userId);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@time", DateTime.Now);
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void UpdateInventory(int productId, int newQuantity)
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Product SET Inventory = @Quantity WHERE Id = @Id";

            using(SqlCommand cmd = new SqlCommand(sqlCmd, connection))
            {
                SqlParameter param = new SqlParameter("@Quantity", newQuantity);
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Id", productId);
                cmd.Parameters.Add(param);

                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public void ReplenishInventory()
    {
        using(SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            string sqlCmd = "UPDATE Product SET Inventory = 100";
            SqlCommand cmd = new SqlCommand(sqlCmd, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    /// <summary>
    /// Search for Username by name
    /// </summary>
    /// <param name="searchTerm">string param to search USername by</param>
    /// <returns>List of Restaurants that contains the search term, an empty list otherwise</returns>
    public List<Customer> SearchCustomers(string searchTerm)
    {
        string searchQuery = $"SELECT * FROM UserAccount WHERE Username LIKE '%{searchTerm}%'";

        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand(searchQuery, connection);
        using SqlDataAdapter adapter = new SqlDataAdapter(cmd);

        //Getting ourselves a bucket for our data
        DataSet customerSet = new DataSet();

        //Telling the data adapter to fill our dataset
        adapter.Fill(customerSet, "Username");

        //Our result of executing the searchQuery
        DataTable customerTable = customerSet.Tables["Username"];

        List<Customer> searchResult = new List<Customer>();

        //Processing data from rows of data to list of restaurants
        //so rest of our app can consume
        foreach(DataRow row in customerTable.Rows)
        {
            Customer uName = new Customer(row);
            searchResult.Add(uName);
        }
        return searchResult;
    }

    public List<Store> GetAllStores()
    {
        List<Store> allStores = new List<Store>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string stoSelect = "Select * From Store";
        //string productSelect = "Select * From Review";
        
        //A single dataSet to hold all our data
        DataSet StoreSet = new DataSet();

        //Two different adapters for different tables
        using SqlDataAdapter stoAdapter = new SqlDataAdapter(stoSelect, connection);
        //using SqlDataAdapter reviewAdapter = new SqlDataAdapter(reviewSelect, connection);

        stoAdapter.Fill(StoreSet, "Store");
        //reviewAdapter.Fill(StoreSet, "Review");

        DataTable? StoreTable = StoreSet.Tables["Store"];
        //DataTable? ReviewTable = RRSet.Tables["Review"];

        if(StoreTable != null)
        {
            foreach(DataRow row in StoreTable.Rows)
            {
                Store sto = new Store(row);
                // sto.Id = (int) row["Id"];
                // sto.StoreName = row["Name"].ToString() ?? "";
                // sto.City = row["City"].ToString() ?? "";
                // sto.State = row["State"].ToString() ?? "";

                // resto.Reviews = ReviewTable.AsEnumerable().Where(r => (int) r["RestaurantId"] == resto.Id).Select(
                //     r =>
                //         new Review {
                //             Id = (int) r["Id"],
                //             RestaurantId = (int) r["RestaurantId"],
                //             Rating = (int) r["Rating"],
                //             Note = r["NOTE"].ToString() ?? ""
                //         }
                // ).ToList();
                allStores.Add(sto);
            }
        }
        return allStores;
    }

    public List<Product> GetAllProducts()
    {
        List<Product> allProducts = new List<Product>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string prodSelect = "Select * From Product";
        //string productSelect = "Select * From Review";
        
        //A single dataSet to hold all our data
        DataSet ProductSet = new DataSet();

        //Two different adapters for different tables
        using SqlDataAdapter prodAdapter = new SqlDataAdapter(prodSelect, connection);
        //using SqlDataAdapter reviewAdapter = new SqlDataAdapter(reviewSelect, connection);

        prodAdapter.Fill(ProductSet, "Product");
        //reviewAdapter.Fill(StoreSet, "Review");

        DataTable? ProductTable = ProductSet.Tables["Product"];
        //DataTable? ReviewTable = RRSet.Tables["Review"];

        if(ProductTable != null)
        {
            foreach(DataRow row in ProductTable.Rows)
            {
                Product prod = new Product(row);
                // sto.Id = (int) row["Id"];
                // sto.StoreName = row["Name"].ToString() ?? "";
                // sto.City = row["City"].ToString() ?? "";
                // sto.State = row["State"].ToString() ?? "";

                // resto.Reviews = ReviewTable.AsEnumerable().Where(r => (int) r["RestaurantId"] == resto.Id).Select(
                //     r =>
                //         new Review {
                //             Id = (int) r["Id"],
                //             RestaurantId = (int) r["RestaurantId"],
                //             Rating = (int) r["Rating"],
                //             Note = r["NOTE"].ToString() ?? ""
                //         }
                // ).ToList();
                allProducts.Add(prod);
            }
        }
        return allProducts;
    }

    public List<Order> GetAllOrders(int Id)
    {
        List<Order> allOrders = new List<Order>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"Select * From Orders WHERE UserId = {Id}";
        
        DataSet OrderSet = new DataSet();

        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable != null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    public List<Order> StoreOrders(int storeId)
    {
        List<Order> allOrders = new List<Order>();

        using SqlConnection connection = new SqlConnection(_connectionString);
        string ordSelect = $"SELECT * FROM Orders WHERE StoreId = {storeId}";
        
        DataSet OrderSet = new DataSet();

        using SqlDataAdapter ordAdapter = new SqlDataAdapter(ordSelect, connection);
        ordAdapter.Fill(OrderSet, "Order");
        DataTable? OrderTable = OrderSet.Tables["Order"];
        if(OrderTable!= null)
        {
            foreach(DataRow row in OrderTable.Rows)
            {
                Order ord = new Order(row);
                allOrders.Add(ord);
            }
        }
        return allOrders;
    }

    /// <summary>
    /// Search for the Username for exact match of name
    /// </summary>
    /// <param name="username">username object to search for dup</param>
    /// <returns>bool: true if there is duplicate, false if not</returns>
    public bool IsDuplicate(Customer customer)
    {
        string searchQuery = $"SELECT * FROM UserAccount WHERE Username='{customer.UserName}'";
        
        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand(searchQuery, connection);

        connection.Open();

        using SqlDataReader reader = cmd.ExecuteReader();

        if(reader.HasRows)
        {
            return true;
        }
        return false;
    }

    public Customer Login(Customer customer)
    {
        string searchQuery = $"SELECT * FROM UserAccount WHERE Username='{customer.UserName}'";
        
        using SqlConnection connection = new SqlConnection(_connectionString);
        using SqlCommand cmd = new SqlCommand(searchQuery, connection);

        connection.Open();

        using SqlDataReader reader = cmd.ExecuteReader();
        Customer acc = new Customer();

        if(reader.Read())
        {
            acc.Id = reader.GetInt32(0);
            acc.UserName = reader.GetString(1);
            acc.Password = reader.GetString(2);
        }
        //no record was returned. No duplicate record in the db
        return acc;
    }
}