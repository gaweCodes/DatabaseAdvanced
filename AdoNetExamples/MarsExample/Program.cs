using System;
using System.Data;
using System.Data.SqlClient;

namespace MarsExample
{
    internal static class Program
    {
        private static void Main()
        {
            using (var connection =
                new SqlConnection(@"Data Source=EPICPCGAEBSTER;Database=Northwind;Integrated security=true;MultipleActiveResultSets=true"))
            {
                const string textProducts = "SELECT ProductName, UnitsInStock, SupplierID FROM Products";
                const string textSupplier = "SELECT CompanyName FROM Suppliers WHERE SupplierID=@SupplierID";
                var cmdProducts = new SqlCommand(textProducts, connection);
                var cmdSupplier = new SqlCommand(textSupplier, connection);
                var param = cmdSupplier.Parameters.Add("@SupplierID", SqlDbType.Int);
                connection.Open();
                var readerProducts = cmdProducts.ExecuteReader();
                while (readerProducts.Read())
                {
                    Console.Write("{0,-35}{1,-6}", readerProducts["ProductName"], readerProducts["UnitsInStock"]);
                    param.Value = readerProducts["SupplierID"];
                    var readerSupplier = cmdSupplier.ExecuteReader();
                    while (readerSupplier.Read())
                        Console.WriteLine(readerSupplier["Companyname"]);
                    readerSupplier.Close();
                    Console.WriteLine(new string('-', 80));
                }
                readerProducts.Close();
            }
            Console.ReadLine();
        }
    }
}
