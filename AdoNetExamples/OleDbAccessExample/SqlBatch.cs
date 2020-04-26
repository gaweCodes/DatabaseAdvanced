using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace OleDbAccessExample
{
    internal static class SqlBatch
    {
        private static void Main(string[] args)
        {
            if (args.Length != 2)
            { 
                Console.WriteLine("usage: <Appname> <inputFilePath> <outputFilePath>");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"The file {args[0]} does not exist.");
                return;
            }

            var inputStreamReader = new StreamReader(new FileStream(args[0], FileMode.Open));
            var outputstreamWriter = new StreamWriter(new FileStream(args[1], FileMode.Create));
            var connectionString = inputStreamReader.ReadLine();
            if(connectionString == null) return;
            using (var connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
                var command = connection.CreateCommand();
                command.Transaction = transaction;
                try
                {
                    var sql = inputStreamReader.ReadLine();
                    while (sql != null)
                    {
                        command.CommandText = sql;
                        Execute(command, outputstreamWriter);
                        sql = inputStreamReader.ReadLine();
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    outputstreamWriter.WriteLine(e.Message);
                    transaction.Rollback();
                }
                inputStreamReader.Close();
                outputstreamWriter.Close();
            }
        }
        private static void Execute(IDbCommand command, TextWriter outStreamWriter)
        {
            outStreamWriter.WriteLine(command.CommandText);
            var dataReader = command.ExecuteReader();
            var row = new object[dataReader.FieldCount];
            while (dataReader.Read())
            {
                var columns = dataReader.GetValues(row);
                for (var i = 0; i < columns; i++)
                    outStreamWriter.Write(row[i] + "\t");
                outStreamWriter.WriteLine();
            }
            outStreamWriter.WriteLine();
            dataReader.Close();
        }
    }
}
