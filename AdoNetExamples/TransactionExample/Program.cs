using System;
using System.Data;
using System.Data.SqlClient;

namespace TransactionExample
{
    internal static class Program
    {
        private static void Main()
        {
            const string connectionString = "Data Source=EPICPCGAEBSTER;Integrated Security=SSPI;Initial Catalog=Northwind";
            IDbConnection connection = new SqlConnection(connectionString);
            IDbTransaction transaction = null;
            try
            {
                connection.Open();
                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

                var deleteOrderCommandByStaticId = connection.CreateCommand();
                deleteOrderCommandByStaticId.CommandText = "DELETE [Order Details] WHERE OrderId = 10258";
                deleteOrderCommandByStaticId.Transaction = transaction;
                deleteOrderCommandByStaticId.ExecuteNonQuery();

                var deleteOrderCommandByStaticId1 = connection.CreateCommand();
                deleteOrderCommandByStaticId1.CommandText = "DELETE Orders WHERE OrderId = 10258";
                deleteOrderCommandByStaticId1.Transaction = transaction;
                deleteOrderCommandByStaticId1.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                transaction?.Rollback();
            }
            finally
            {
                connection.Close();
            }
            Console.ReadLine();
        }
    }
}
