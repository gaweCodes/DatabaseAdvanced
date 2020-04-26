using System;
using System.Linq;
using TypedDataSet.NorthwindDataSetTableAdapters;

namespace TypedDataSet
{
    internal static class Program
    {
        private static void Main()
        {
            var dataSet = new NorthwindDataSet();
            var tblAdapter = new ProductsTableAdapter();
            tblAdapter.Fill(dataSet.Products);

            var tblAdapterCategories = new CategoriesTableAdapter();
            tblAdapterCategories.Fill(dataSet.Categories);

            foreach (var row in dataSet.Products)
                Console.WriteLine($"{row.ProductName}");
            var peterProduct = dataSet.Products.FirstOrDefault(x => x.ProductName == "Peter");
            var gabrielProduct = dataSet.Products.FirstOrDefault(x => x.ProductName == "Gabriel");
            if (gabrielProduct == default(NorthwindDataSet.ProductsRow) && peterProduct == default(NorthwindDataSet.ProductsRow))
            {
                var newRow = dataSet.Products.NewProductsRow();
                newRow.ProductName = "Gabriel";
                newRow.Discontinued = false;
                newRow.CategoriesRow = dataSet.Categories.First();
                dataSet.Products.AddProductsRow(newRow);
            }
            else if(peterProduct == default(NorthwindDataSet.ProductsRow))
            {
                gabrielProduct.BeginEdit();
                gabrielProduct.ProductName = "Peter";
                gabrielProduct.EndEdit();
            }
            else if (gabrielProduct == default(NorthwindDataSet.ProductsRow))
                peterProduct.Delete();
            tblAdapter.Update(dataSet);
            Console.ReadLine();
        }
    }
}
