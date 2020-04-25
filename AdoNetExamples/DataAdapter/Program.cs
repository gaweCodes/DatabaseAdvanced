using System;
using System.Data;
using System.Data.OleDb;

namespace DataAdapter
{
    internal static class Program
    {
        private static void Main()
        {
            var dataSet = LoadData();
            Print(dataSet);
            dataSet.WriteXml("data.xml");
            Console.ReadLine();
        }

        private static DataSet LoadData()
        {
            var dataSet = new DataSet("PersonContacts");
            IDbDataAdapter dataAdapter = new OleDbDataAdapter();
            dataAdapter.SelectCommand = GetSelectAllCommand();
            dataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            dataAdapter.TableMappings.Add("Table", "Person");
            dataAdapter.TableMappings.Add("Table1", "Contact");
            dataAdapter.Fill(dataSet);
            if (dataSet.HasErrors)
                dataSet.RejectChanges();
            else
                dataSet.AcceptChanges();
            ((IDisposable) dataAdapter).Dispose();
            return dataSet;
        }

        private static IDbCommand GetSelectAllCommand() =>
            new OleDbCommand
            {
                Connection =
                    new OleDbConnection(
                        "Provider=SQLOLEDB;Data Source=EPICPCGAEBSTER;Integrated Security=SSPI;Initial Catalog=playground"),
                CommandText = "SELECT * FROM Person; SELECT * FROM Contact"
            };
        private static void Print(DataSet dataSet)
        {
            Console.WriteLine($"DataSet {dataSet.DataSetName}");
            Console.WriteLine();
            foreach (DataTable dataTable in dataSet.Tables)
            {
                PrintTable(dataTable);
                Console.WriteLine();
            }
        }
        private static void PrintTable(DataTable dataTable)
        {
            Console.WriteLine($"Table {dataTable.TableName}");
            foreach (DataColumn col in dataTable.Columns)
                Console.Write(col.ColumnName + "|");
            Console.WriteLine();
            for (var i = 0; i < 40; i++) 
                Console.Write("-");
            Console.WriteLine();
            
            var nrOfCols = dataTable.Columns.Count;
            foreach (DataRow row in dataTable.Rows)
            {
                for (var i = 0; i < nrOfCols; i++)
                {
                    Console.Write(row[i]); 
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }
    }
}
