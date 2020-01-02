using System;
using System.Collections.Generic;
using System.Linq;


namespace ProductManagementSystem
{
    class Product
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal QtyInhand { get; set; }
        public decimal SellQty { get; set; }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Product product1 = new Product()
            {
                ProductCode = "P01",
                ProductName = "iPhone X",
                UnitPrice = 50000,
                QtyInhand = 10


            };
            Product product2 = new Product()
            {
                ProductCode = "P02",
                ProductName = "Redmi Note 7 pro",
                UnitPrice = 21000,
                QtyInhand = 50
            };

            List<Product> Products = new List<Product>();
            List<Product> BoughtProducts = new List<Product>();
            Products.Add(product1);
            Products.Add(product2);
            bool allDone = false;

            while (!allDone)
            {
                Console.WriteLine("\nPlease select an option!");
                Console.WriteLine("1: Add Product to Store");
                Console.WriteLine("2: Show Product list of Store");
                Console.WriteLine("3: Buy Product");
                Console.WriteLine("4: Show all bought Product list");
                Console.WriteLine("5: Show total Price of all bought product");
                Console.WriteLine("6: Delete a product from store");
                Console.WriteLine("7: Exit");

                Console.Write("Please select an option: ");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice >= 1 || choice <= 7)
                {
                    if (choice == 7)
                    {
                        Console.WriteLine("Thank You");
                        allDone = true;
                        break;
                    }
                    switch (choice)
                    {
                        case 1:

                            Console.Write("Product code: ");
                            string code = Console.ReadLine();

                            while (Products.Where(p => p.ProductCode.ToLower() == code.ToLower()).Count() >= 1)
                            {
                                Console.WriteLine("This code is already taken. try with another one!!");
                                Console.Write("Product code: ");
                                code = Console.ReadLine();
                            }

                            Console.Write("Product Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Product Price: ");
                            decimal unitPrice = decimal.Parse(Console.ReadLine());

                            Console.Write("Product Stock: ");
                            decimal stockInHand = decimal.Parse(Console.ReadLine());

                            Product item = new Product()
                            {
                                ProductCode = code,
                                ProductName = name,
                                UnitPrice = unitPrice,
                                QtyInhand = stockInHand
                            };
                            Products.Add(item);
                            Console.Clear();
                            Console.WriteLine("Product Added successfully");
                            Console.Write("Please select an option: ");
                            break;

                        case 2:
                            Console.Clear();
                            foreach (var product in Products)
                            {
                                Console.WriteLine("Code= {0}, Name= {1}, Price= {2}, Remaining Stock= {3}",
                                    product.ProductCode, product.ProductName, product.UnitPrice, product.QtyInhand);
                            }
                            Console.Write("Please select an option: ");
                            break;

                        case 3:
                            Console.WriteLine();
                            Console.Write("Product code: ");
                            code = Console.ReadLine();

                            Console.Write("Product Quantity: ");
                            decimal sellQty = decimal.Parse(Console.ReadLine());

                            var InHandQty = Products.Where(p => p.ProductCode.ToLower() == code.ToLower()).Select(y => y.QtyInhand).FirstOrDefault();
                            if (sellQty <= InHandQty)
                            {
                                foreach (var product in Products)
                                {
                                    if (product.ProductCode.ToLower() == code.ToLower())
                                    {
                                        product.QtyInhand = product.QtyInhand - sellQty;

                                        Product sellItem = new Product()
                                        {
                                            ProductCode = product.ProductCode,
                                            ProductName = product.ProductName,
                                            UnitPrice = product.UnitPrice,
                                            QtyInhand = sellQty
                                        };
                                        BoughtProducts.Add(sellItem);

                                    }
                                }
                                Console.Clear();
                                Console.WriteLine("Successfully Product bought!!");
                            }
                            else
                            {
                                Console.WriteLine("Sorry given quantity is not available in stock");
                            }

                            Console.WriteLine("Please select an option: ");
                            break;
                        case 4:
                            Console.Clear();
                            foreach (var boughtProduct in BoughtProducts)
                            {
                                Console.WriteLine("Code= {0}, Name= {1}, Price= {2}, Remaining Stock= {3}",
                                    boughtProduct.ProductCode, boughtProduct.ProductName, boughtProduct.UnitPrice, boughtProduct.QtyInhand);
                            }
                            Console.WriteLine("Please select an option: ");
                            break;

                        case 5:
                            Console.Clear();
                            decimal totalExpense = BoughtProducts.Sum(x => x.QtyInhand * x.UnitPrice);
                            Console.WriteLine("Total Price = {0}", totalExpense);
                            Console.WriteLine("Please select an option: ");
                            break;

                        case 6:
                            Console.Write("Product code: ");
                            code = Console.ReadLine();

                            var itemToRemove = Products.SingleOrDefault(p => p.ProductCode.ToLower() == code.ToLower());
                            Products.Remove(itemToRemove);

                            Console.Clear();
                            Console.WriteLine("Successfully Product Deleted");
                            Console.WriteLine("Please select an option: ");
                            break;

                    }
                }

            }

        }


    }


}
