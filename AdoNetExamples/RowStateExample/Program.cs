using System;
using System.Data;

namespace RowStateExample
{
    internal static class Program
    {
        private static void Main()
        {
            DemonstrateRowState();
            Console.ReadLine();
        }
        private static void DemonstrateRowState()
        {
            var myTable = MakeTable();

            var myDataRow = myTable.NewRow();
            Console.WriteLine("New Row " + myDataRow.RowState);

            myTable.Rows.Add(myDataRow);
            Console.WriteLine("AddRow " + myDataRow.RowState);

            myTable.AcceptChanges();
            Console.WriteLine("AcceptChanges " + myDataRow.RowState);

            myDataRow["FirstName"] = "Scott";
            Console.WriteLine("Modified " + myDataRow.RowState);

            myDataRow.Delete();
            Console.WriteLine("Deleted " + myDataRow.RowState);
        }
        private static DataTable MakeTable()
        {
            var dataTable = new DataTable("myTable");
            var dcFirstName = new DataColumn("FirstName", typeof(string));
            dataTable.Columns.Add(dcFirstName);
            return dataTable;
        }
    }
}
