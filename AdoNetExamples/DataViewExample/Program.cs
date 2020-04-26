using System;
using System.Data;
using System.Data.SqlClient;

namespace DataViewExample
{
    internal static class Program
    {
        private static void Main()
        {
            using (var connection = new SqlConnection())
            {
                connection.ConnectionString = "Database=Northwind;Server=EPICPCGAEBSTER;Integrated Security=sspi";
                var cmd = new SqlCommand
                {
                    Connection = connection, CommandText = "SELECT ProductName, UnitPrice FROM Products"
                };
                var dataSet = new DataSet();
                var adapter = new SqlDataAdapter(cmd) {MissingSchemaAction = MissingSchemaAction.AddWithKey};
                adapter.Fill(dataSet);
                var dataView = new DataView(dataSet.Tables[0]);
                
                var newRow = dataView.AddNew();
                newRow["ProductName"] = "Schokolade";
                newRow["UnitPrice"] = 15.99;
                newRow.EndEdit();
                
                var rowToEdit = dataView[0];
                rowToEdit.BeginEdit();
                rowToEdit["ProductName"] = "Eisbein";
                rowToEdit.EndEdit();

                var rowToDelete = dataView[1];
                rowToDelete.Delete();

                dataView.RowStateFilter = DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedOriginal;
                foreach (DataRowView rowView in dataView)
                    Console.WriteLine(rowView["ProductName"]);
                Console.ReadLine();
            }
        }
    }
}
