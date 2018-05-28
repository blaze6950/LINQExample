using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExample
{
    class DbLibrary : IDisposable
    {
        private String _connectionString;
        private String _factoryName;
        private DbProviderFactory _factory;
        private DbConnection _connection;

        public DbLibrary()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["LINQExampleCS"].ConnectionString;
            _factoryName = ConfigurationManager.ConnectionStrings["LINQExampleCS"].ProviderName;

            _factory = DbProviderFactories.GetFactory(_factoryName);
            _connection = _factory.CreateConnection();
            _connection.ConnectionString = _connectionString;
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Book> GetListBooks()
        {
            List<Book> newList = new List<Book>();
            DbCommand command = _factory.CreateCommand();
            command.Connection = _connection;
            command.CommandText =
                "SELECT Books.Id, Books.Name, Books.Pages, Books.YearPress, Themes.Name as 'Theme', Categories.Name as 'Category', Authors.LastName, Authors.FirstName, Press.Name as 'Press', Books.Comment, Books.Quantity FROM Books JOIN Themes ON Id_Themes = Themes.Id JOIN Categories ON Id_Category = Categories.Id JOIN Authors ON Id_Author = Authors.Id JOIN Press ON Id_Press = Press.Id";
            DbDataReader reader = null;
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    newList.Add(new Book((String) reader["Name"], (String) reader["Press"],
                        (String) reader["YearPress"], (String) reader["Category"], (String) reader["Theme"],
                        (String) reader["LastName"], (String) reader["FirstName"], (String) reader["Quantity"]));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                reader?.Close();
            }
            return newList;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
