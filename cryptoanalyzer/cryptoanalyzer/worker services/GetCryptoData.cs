using cryptoanalyzer;
using Microsoft.Data.SqlClient;
using System.Text.Json;

public class GetCryptoData
{
    private static readonly string apiKey = "2cca784b-3b00-49ae-9f69-c284de9dbea1";
    private static readonly string apiUrl = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";
    private static readonly string connectionString = "Data Source=.;Initial Catalog=test;Integrated Security=True;Encrypt=False";

    

   
    public static async Task FetchAndSaveCryptoData()
    {
        try
        {
            var cryptoData = await FetchCryptoData(); // دریافت داده‌ها از API
            
            SaveSelectedCryptosToSql(cryptoData); // ذخیره داده‌های انتخابی در دیتابیس
            SaveAllCryptosToSql(cryptoData);//ذخیره همه داده ها تو دیتا بیس
            SaveCurrentDataToSql(cryptoData);
            Console.WriteLine("information saved succesfully.");
            
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        }
    }
    //یادم باشه یه متد هم به جایه پاک کردن بعد بیاره همون دفعه آپدیت کنه
    private static async Task<JsonDocument> FetchCryptoData()
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiKey);
            client.DefaultRequestHeaders.Add("Accepts", "application/json");

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonDocument.Parse(responseBody);
        }
    }

    private static void SaveSelectedCryptosToSql(JsonDocument cryptoData)
    {
        // لیست ارزهای مورد نظر
        var selectedSymbols = new[] { "BTC", "SOL", "DOGE", "USDT", "BGB", "ADA", "ETH", "BCH", "SHIB","TON", "LTC" };

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            foreach (var crypto in cryptoData.RootElement.GetProperty("data").EnumerateArray())
            {
                string? symbol = crypto.GetProperty("symbol").GetString();

                // چک کردن اینکه آیا این نماد در لیست مورد نظر ما هست
                if (Array.Exists(selectedSymbols, s => s == symbol))
                {
                    string? name = crypto.GetProperty("name").GetString();

                    decimal price = crypto.GetProperty("quote").GetProperty("USD").GetProperty("price").GetDecimal();

                    string query = "INSERT INTO CryptoPrices (Name, Symbol, Price, DateRetrieved) VALUES (@Name, @Symbol, @Price, @DateRetrieved)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Symbol", symbol);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@DateRetrieved", DateTime.Now);

                        command.ExecuteNonQuery();
                    }
                }
            }
            connection.Close();
        }
    }
    private static void SaveAllCryptosToSql(JsonDocument cryptoData)
    {
        


        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            foreach (var crypto in cryptoData.RootElement.GetProperty("data").EnumerateArray())
            {
                string? symbol = crypto.GetProperty("symbol").GetString();

                string? name = crypto.GetProperty("name").GetString();

                decimal? price = crypto.GetProperty("quote").GetProperty("USD").GetProperty("price").GetDecimal();

                string query = "INSERT INTO T_Crypto (Name, Symbol, Price, DateRetrieved) VALUES (@Name, @Symbol, @Price, @DateRetrieved)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Symbol", symbol);
                    command.Parameters.AddWithValue("@Price", price);
                    command.Parameters.AddWithValue("@DateRetrieved", DateTime.Now);

                    command.ExecuteNonQuery();
                }

            }
            connection.Close();
        }
    }
    private static void SaveCurrentDataToSql(JsonDocument cryptoData)
    {
        var selectedSymbols = new[] { "BTC", "SOL", "DOGE", "USDT", "BGB", "ADA", "ETH", "BCH", "SHIB", "TON", "LTC" };
        // delet hameye data haro
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();



            string query = "DELETE  FROM Cucrypto";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }


            connection.Close();
        }
        // save con data haye jadid ro
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            foreach (var crypto in cryptoData.RootElement.GetProperty("data").EnumerateArray())
            {
                string? symbol = crypto.GetProperty("symbol").GetString();

                // چک کردن اینکه آیا این نماد در لیست مورد نظر ما هست
                if (Array.Exists(selectedSymbols, s => s == symbol))
                {
                    string? name = crypto.GetProperty("name").GetString();

                    decimal price = crypto.GetProperty("quote").GetProperty("USD").GetProperty("price").GetDecimal();

                    string query = "INSERT INTO Cucrypto (Name, Symbol, Price, DateRetrieved) VALUES (@Name, @Symbol, @Price, @DateRetrieved)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Symbol", symbol);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@DateRetrieved", DateTime.Now);

                        command.ExecuteNonQuery();
                    }
                }
            }
            connection.Close();
        }
        //Dictionary<int, (string? name, string? symbol, decimal? price)> cryptos = new Dictionary<int, (string? name, string? symbol, decimal? price)>();
        //// خوندن 11 دیتا اخر
        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{
        //    connection.Open();

        //    SqlCommand commandread = new SqlCommand();
        //    commandread.Connection = connection;
        //    commandread.CommandType = System.Data.CommandType.Text;
        //    commandread.CommandText = "SELECT TOP 11 * FROM CryptoPrices ORDER BY DateRetrieved DESC";
        //    SqlDataReader reader = commandread.ExecuteReader();
        //    int n = 0;
        //    while (reader.Read() && n < 11)
        //    {

        //        string? name = reader[reader.GetName(0)].ToString();
        //        string? symbol = reader[reader.GetName(1)].ToString();
        //        decimal price;
        //        decimal.TryParse(reader[reader.GetName(2)].ToString(), out price);
        //        cryptos.Add(n, (name, symbol, price));
        //        //Console.WriteLine($"{name}   {symbol}   {price}");
        //        n++;
        //    }

        //    connection.Close();

        //}
        //// حذف کردن همه دیتا های جدول
        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{
        //    connection.Open();



        //    string query = "DELETE  FROM Cucrypto";
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        command.ExecuteNonQuery();
        //    }


        //    connection.Close();
        //}
        //// اضافه کردن دیتا های جدید
        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{
        //    connection.Open();

        //    foreach (var crypto in cryptos)
        //    {
        //        string? symbol = crypto.Value.name;

        //        string? name = crypto.Value.symbol;

        //        decimal? price = crypto.Value.price;

        //        string query = "INSERT INTO Cucrypto (Name, Symbol, Price, DateRetrieved) VALUES (@Name, @Symbol, @Price, @DateRetrieved)";
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@Name", name);
        //            command.Parameters.AddWithValue("@Symbol", symbol);
        //            command.Parameters.AddWithValue("@Price", price);
        //            command.Parameters.AddWithValue("@DateRetrieved", crypto.Value.DateRetrieved);

        //            command.ExecuteNonQuery();
        //        }

        //    }
        //    connection.Close();
        //}

    }
    public static void ShowDbTable(string tableName, ILogger<Worker> logger)
    {
        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {

            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = $"SELECT * FROM {tableName}"; // کوئری موردنظر


            sqlConnection.Open();
            Console.WriteLine(sqlConnection.State);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                
                while (reader.Read())
                {
                    
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader.GetName(i) + $": {reader[reader.GetName(i)]}\t\t");
                    }
                    Console.WriteLine();
                    //Console.WriteLine($"ID: {reader["id"]}, Name: {reader["Crypto_name"]}, Price: {reader["Price"]}  , Date: { reader["Date"]}");
                }
            }


            logger.LogInformation("close the sql server ...: {time}", DateTimeOffset.Now);
            sqlConnection.Close();
        }
    }

}