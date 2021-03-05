using eCommerceClassLibrary;
using eCommerceClassLibrary.Models;
using System;

namespace eCommerceConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            eCommerceDBContext context = new eCommerceDBContext();
            eCommerceRepository repository = new eCommerceRepository(context);
            Products product = new Products();
            product.Sku = "BLUEPOLO0001";
            product.Name = "Blue Formal Polo Shirt";
            product.CategoryId = 1;
            //repository.ModifyProduct(product);
            //repository.AddProduct(product);
            //repository.DeleteProduct(product);

            //Console.WriteLine(repository.AddCategory("Belts"));
            //Console.WriteLine(repository.RenameCategory(12, "Belt"));
            Console.WriteLine(repository.DeleteCategory(12));

        }
    }
}
