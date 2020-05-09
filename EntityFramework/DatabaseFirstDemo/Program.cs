using System;

namespace DatabaseFirstDemo 
{
    internal static class Program 
    {
        private static void Main() 
        {
            using (var context = new NorthwindDB()) 
            {
                context.Database.Log = Console.Write;
                var product = new Product
                {
                    ProductName = "Schokolade",
                    Discontinued = false,
                    UnitPrice = 2.55m
                };
                context.Products.Add(product);
                context.SaveChanges();
                Console.WriteLine($"ProductID = {product.ProductID}");
            }
            Console.ReadLine();

            using (var context = new NorthwindDB()) 
            {
                context.Database.Log = Console.Write;
                var category = new Category
                {
                    CategoryName = "Backwaren"
                };
                var product = new Product() {
                    ProductName = "Kuchen",
                    Discontinued = false,
                    UnitPrice = 2.55m,
                    Category = category 
                };
                context.Products.Add(product);
                context.SaveChanges();
                Console.WriteLine($"ProductID = {product.ProductID}");
                Console.WriteLine($"CategoryID = {category.CategoryID}");
            }
            Console.ReadLine();

            using (var context = new NorthwindDB()) 
            {
                context.Database.Log = Console.Write;
                var sales = context.SalesByYear(new DateTime(1998, 3, 1), new DateTime(1998, 3, 31));
                foreach (var sale in sales) 
                    Console.WriteLine(sale.OrderID);
            }
            Console.ReadLine();

            using (var context = new NorthwindDB()) 
            {
                context.Database.Log = Console.Write;
                var customer = new Customer
                {
                    CustomerID = "asf",
                    CompanyName = "adf",
                    CustomerType = CustomerType.Kunde
                };
                context.Customers.Add(customer);
                context.SaveChanges();
                 Console.WriteLine($"CustomerID = {customer.CustomerID}");
            }
            Console.ReadLine();
        }
    }
}
