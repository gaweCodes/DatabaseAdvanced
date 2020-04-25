using System;
using System.Data;
using System.Data.SqlClient;

namespace DataReaderExample
{
    internal static class Program
    {
        private static void Main()
        {
            const string connectionString = "Data Source=EPICPCGAEBSTER;Integrated Security=SSPI;Initial Catalog=Northwind";
            IDbConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT EmployeeID, LastName, FirstName FROM Employees";
                var dataReader = command.ExecuteReader();
                var dataRow = new object[dataReader.FieldCount];
                while (dataReader.Read())
                {
                    var cols = dataReader.GetValues(dataRow);
                    for (var i = 0; i < cols; i++)
                        Console.Write($"| {dataRow[i]} ");
                    Console.WriteLine();
                }
                dataReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                try
                {
                    connection?.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.ReadLine();
        }
    }
}
