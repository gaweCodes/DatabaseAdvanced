using System;
using System.Data;

namespace DataSetExample
{
    internal static class Program
    {
        private static void Main()
        {
            var ds = CreatePersonContactDataSet();
            var personDataRow = ds.Tables["Person"].NewRow();
            personDataRow[1] = "Wolfgang";
            personDataRow["Name"] = "Beer";
            ds.Tables["Person"].Rows.Add(personDataRow);

            var contactDataRow = ds.Tables[1].NewRow();
            contactDataRow[1] = "Hans";
            contactDataRow[2] = "Meier";
            contactDataRow[3] = "Housi";
            contactDataRow[4] = "hmeier@hsr.ch";
            contactDataRow[5] = "379";

            contactDataRow["PersonID"] = (long)personDataRow["ID"];
            ds.Tables[1].Rows.Add(contactDataRow);

            contactDataRow = ds.Tables[1].NewRow();
            contactDataRow[1] = "Vreni";
            contactDataRow[2] = "Müller";
            contactDataRow[3] = "Vreni";
            contactDataRow[4] = "vmueller@hsr.ch";
            contactDataRow[5] = "382";

            contactDataRow["PersonID"] = (long)personDataRow["ID"];
            ds.Tables[1].Rows.Add(contactDataRow);

            ds.AcceptChanges();

            foreach (DataRow person in ds.Tables["Person"].Rows)
            {
                Console.WriteLine($"Contacts of {person["Name"]}:");
                foreach (var contact in person.GetChildRows("PersonHasContacts"))
                    Console.WriteLine($"{contact[0]}, {contact["Name"]}: {contact["Phone"]}");
            }
            Console.ReadLine();
        }
        private static DataSet CreatePersonContactDataSet()
        {
            var dataSet = new DataSet("PersonContacts");
            var personTable = new DataTable("Person");

            var dataColumnToAddToTable = new DataColumn
            {
                DataType = typeof(long),
                ColumnName = "ID",
                ReadOnly = true,
                Unique = true,
                AutoIncrement = true,
                // Index starts with -1 and continues with -2, -3, ...
                AutoIncrementSeed = -1,
                AutoIncrementStep = -1
            };
            personTable.Columns.Add(dataColumnToAddToTable);
            personTable.PrimaryKey = new[] { dataColumnToAddToTable };

            dataColumnToAddToTable = new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "FirstName"
            };
            personTable.Columns.Add(dataColumnToAddToTable);

            dataColumnToAddToTable = new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "Name"
            };
            personTable.Columns.Add(dataColumnToAddToTable);
            dataSet.Tables.Add(personTable);

            var contactTable = new DataTable("Contact");
            dataColumnToAddToTable = new DataColumn
            {
                DataType = typeof(long),
                ColumnName = "ID",
                ReadOnly = true,
                Unique = true,
                AutoIncrement = true,
                AutoIncrementSeed = -1,
                AutoIncrementStep = -1
            };
            contactTable.Columns.Add(dataColumnToAddToTable);
            contactTable.PrimaryKey = new[] { dataColumnToAddToTable };

            dataColumnToAddToTable = new DataColumn 
            {
                DataType = typeof(string),
                ColumnName = "FirstName"
            };
            contactTable.Columns.Add(dataColumnToAddToTable);

            dataColumnToAddToTable = new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "Name"
            };
            contactTable.Columns.Add(dataColumnToAddToTable);

            dataColumnToAddToTable = new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "NickName"
            };
            contactTable.Columns.Add(dataColumnToAddToTable);

            dataColumnToAddToTable = new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "email"
            };
            contactTable.Columns.Add(dataColumnToAddToTable);

            dataColumnToAddToTable = new DataColumn
            {
                DataType = typeof(string),
                ColumnName = "Phone"
            };
            contactTable.Columns.Add(dataColumnToAddToTable);

            dataColumnToAddToTable = new DataColumn
            {
                DataType = typeof(long),
                ColumnName = "PersonID"
            };
            contactTable.Columns.Add(dataColumnToAddToTable);
            dataSet.Tables.Add(contactTable);

            var parentColumn = dataSet.Tables["Person"].Columns["ID"];
            DataColumn childColumn = dataSet.Tables["Contact"].Columns["PersonID"];
            
            var dataRelation = new DataRelation("PersonHasContacts", parentColumn, childColumn);
            dataSet.Relations.Add(dataRelation);
            return dataSet;
        }
    }
}
