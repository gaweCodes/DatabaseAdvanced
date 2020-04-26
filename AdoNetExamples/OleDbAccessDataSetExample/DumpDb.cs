using System;
using System.Data;
using System.Data.OleDb;

namespace OleDbAccessDataSetExample
{
    internal static class DumpDb
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("usage: <Appname> <DbName> <TableName>");
                return;
            }
            var dataSet = new DataSet();
            var adapter = new OleDbDataAdapter();
            using (var connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + args[0]))
            {
                for (var i = 1; i < args.Length; i++)
                {
                    var tableName = args[i];
                    adapter.SelectCommand = new OleDbCommand("SELECT * FROM " + tableName, connection);
                    adapter.Fill(dataSet, tableName);
                }
            }
            foreach (DataTable table in dataSet.Tables)
            {
                Console.WriteLine(table.TableName);
                foreach (DataColumn col in table.Columns)
                    Console.Write(col.ColumnName + "\t");
                Console.WriteLine();
                foreach (DataRow row in table.Rows)
                {
                    foreach (var obj in row.ItemArray) 
                        Console.Write(obj + "\t");
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
