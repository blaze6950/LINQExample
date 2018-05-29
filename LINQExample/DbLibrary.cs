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
                        (int) reader["YearPress"], (String) reader["Category"], (String) reader["Theme"],
                        (String) reader["LastName"], (String) reader["FirstName"], (int) reader["Quantity"]));
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

        public List<Book> GetListBooksWithStudents()
        {
            List<Book> newList = new List<Book>();
            DbCommand command = _factory.CreateCommand();
            DbCommand command1 = _factory.CreateCommand();
            command1.Connection = _connection;
            command.Connection = _connection;
            command.CommandText =
                "SELECT Books.Id, Books.Name, Books.Pages, Books.YearPress, Themes.Name as 'Theme', Categories.Name as 'Category', Authors.LastName, Authors.FirstName, Press.Name as 'Press', Books.Comment, Books.Quantity FROM Books JOIN Themes ON Id_Themes = Themes.Id JOIN Categories ON Id_Category = Categories.Id JOIN Authors ON Id_Author = Authors.Id JOIN Press ON Id_Press = Press.Id";
            DbDataReader reader = null;
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    newList.Add(new Book((String)reader["Name"], (String)reader["Press"],
                        (int)reader["YearPress"], (String)reader["Category"], (String)reader["Theme"],
                        (String)reader["LastName"], (String)reader["FirstName"], (int)reader["Quantity"]));
                    command1.CommandText =
                        $"SELECT Students.Id, Students.FirstName, Students.LastName FROM Students JOIN S_Cards ON Students.Id = S_Cards.Id_Student JOIN Books ON Books.Id = S_Cards.Id_Book WHERE Books.Id = {(int)reader["Id"]}";
                    DbDataReader reader2 = null;
                    try
                    {
                        reader2 = command1.ExecuteReader();
                        while (reader2.Read())
                        {
                            newList[newList.Count - 1].Students.Add(new Student((String) reader2["FirstName"],
                                (String) reader2["LastName"]));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                    finally
                    {
                        reader2?.Close();
                    }
                    
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
