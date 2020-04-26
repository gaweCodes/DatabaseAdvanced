using System;
using System.Data;
using System.Data.OleDb;

namespace ExportXml
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.Out.WriteLine("usage: Exportml <DbPath> <OutputFilePath>");
                return;
            }
            Console.WriteLine("Loading data ...");
            var connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + args[0];
            DataSet dataSet = LoadData(connectionString);
            if (dataSet != null)
            {
                Console.WriteLine("Writing data ...");
                dataSet.WriteXml(args[1], XmlWriteMode.WriteSchema);
            }
            else
                Console.WriteLine("Could not load data!");
            Console.ReadLine();
        }
        private static DataSet LoadData(string connectionString)
        {
            OleDbConnection connection = null;
            DataSet dataSet = null;
            try
            {
                connection = new OleDbConnection(connectionString);
                var adapter = new OleDbDataAdapter {MissingSchemaAction = MissingSchemaAction.AddWithKey};
                connection.Open();
                dataSet = string.IsNullOrWhiteSpace(connection.Database) ? new DataSet() : new DataSet(connection.Database);
                var schemaTable =
                    connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] {null, null, null, "TABLE"});
                adapter.SelectCommand = connection.CreateCommand();
                for (var i = 0; i < schemaTable?.Rows.Count; i++)
                {
                    var tableName = schemaTable.Rows[i]["TABLE_NAME"].ToString();
                    adapter.SelectCommand.CommandText = $"SELECT * FROM {tableName};";
                    adapter.Fill(dataSet, tableName);
                }
            }
            catch (Exception e)
            {
                HandleError(e);
            }
            finally
            {
                connection?.Close();
            }
            return dataSet;
        }
        private static void HandleError(Exception e) => Console.Out.WriteLine(e);
    }
}
