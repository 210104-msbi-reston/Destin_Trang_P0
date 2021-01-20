using System;
using System.Collections.Generic;
using StoreDatabase.Models;

namespace StoreUI
{
    class Program
    {


        static void Main(string[] args)
        {
            
            DatabaseOperations operations = new DatabaseOperations();
            StoreOperations front = new StoreOperations(); 

            Console.WriteLine("===== Welcome to Pretty Pizza Pizzaria!");
            Console.WriteLine("===== Enter 1 if you are a customer; Enter 2 if you are a manager:");
            var inp = front.GetNumericalInput (1, 2);

            //Customer loop
            if (inp == 1) {

                //All customers get a shopping cart
                ShoppingCart cart = new ShoppingCart();
                
                while (true) {
                    //Choosing a store location
                    List<Location> stores = operations.GetAllLocations();
                    Console.WriteLine("===== Welcome customer! Select your store location from the list below:");
                    Console.WriteLine("<Enter a valid number to view a store, or enter 0 to exit>");
                    Console.WriteLine("<Enter 'S' to view your shopping cart, or 'O' to view order history>");
                    int optionCounter = 1;
                    foreach (Location s in stores) {
                        Console.WriteLine(optionCounter.ToString() + "  |  " + s.city);
                        optionCounter++;
                    }
                    var storeInp = front.GetCustomerInput (0, stores.Count);
                    if (storeInp == 0) break; //Break out of the purchase loop
                    else if (storeInp == -1) {
                        front.DisplayShoppingCart(cart, operations);
                        continue;
                    }
                    else if (storeInp == -2) {
                        front.DisplayCustomerOrderHistory(operations);
                        continue;
                    }

                    while (true) {
                        //Choosing a product
                        List<Inventory> stock = operations.GetStoreInventory(stores[storeInp - 1]);
                        Console.WriteLine("===== You are viewing the " + stores[storeInp - 1].city + " branch!");
                        Console.WriteLine("===== Here are our available products:");
                        Console.WriteLine("<Enter a valid product number, or enter 0 to return to store selection>");
                        Console.WriteLine("<Enter 'S' to view your shopping cart, or 'O' to view order history>");
                        optionCounter = 1;
                        foreach (Inventory i in stock) {
                            Console.WriteLine(optionCounter.ToString() + "   |   " + string.Format("{0, -20}", operations.GetProduct(i.productID).name) + " |   QTY: " + i.quantity);
                            optionCounter++;
                        }
                        var productInp = front.GetCustomerInput (0, stock.Count);
                        if (productInp == 0) break; //Break out of the product loop
                        else if (productInp == -1) {
                            front.DisplayShoppingCart(cart, operations);
                            continue;
                        }
                        else if (productInp == -2) {
                            front.DisplayCustomerOrderHistory(operations);
                            continue;
                        }

                        //Product quantity desired
                        string productName = operations.GetProduct(stock[productInp - 1].productID).name;
                        Console.WriteLine("===== " + productName + ": How many would you like?");
                        Console.WriteLine("===== REMAINING: " + stock[productInp - 1].quantity);
                        var quantityInp = front.GetNumericalInput (0, stock[productInp - 1].quantity);
                        if (quantityInp > 0) {
                            operations.DepleteInventory(stock[productInp - 1], quantityInp);
                            Console.WriteLine("== " + quantityInp + " " + productName + "s added to the cart! ==");
                            cart.AddToCart(stock[productInp - 1].productID, stores[storeInp - 1].storeID, quantityInp);
                        }
                    }
                }
                
            }


            else if (inp == 2) {
                
                while (true) {
                    //Choosing a store location
                    List<Location> stores = operations.GetAllLocations();
                    Console.WriteLine("===== Welcome manager! Select a store to begin managing");
                    Console.WriteLine("<Enter a valid number to view a store, or enter 0 to exit>");
                    int optionCounter = 1;
                    foreach (Location s in stores) {
                        Console.WriteLine(optionCounter.ToString() + "  |  " + s.city);
                        optionCounter++;
                    }
                    var storeInp = front.GetNumericalInput (0, stores.Count);
                    if (storeInp == 0) break; //Break out of the purchase loop
                    
                    while (true) {
                        //Choosing an action
                        Console.WriteLine("===== You are now viewing the " + stores[storeInp - 1].city + " branch.");
                        Console.WriteLine("===== What would you like to do?");
                        Console.WriteLine("<Enter a valid action, or enter 0 to exit>");
                        Console.WriteLine("1  |  View store inventory");
                        Console.WriteLine("2  |  View store order history");
                        var mngInp = front.GetNumericalInput (0, 2);
                        if (mngInp == 0) break;

                        //Viewing store inventory
                        if (mngInp == 1) {
                            while (true) {
                                List<Inventory> stock = operations.GetStoreInventory(stores[storeInp - 1]);
                                Console.WriteLine("===== You are viewing the " + stores[storeInp - 1].city + " branch's products");
                                Console.WriteLine("===== Select a product to add more stock");
                                Console.WriteLine("<Enter a valid product number, or enter 0 to return to store menu>");
                                optionCounter = 1;
                                foreach (Inventory i in stock) {
                                    Console.WriteLine(optionCounter.ToString() + "   |   " + string.Format("{0, -20}", operations.GetProduct(i.productID).name) + " |   QTY: " + i.quantity);
                                    optionCounter++;
                                }
                                var productInp = front.GetNumericalInput (0, stock.Count);
                                if (productInp == 0) break; //Break out of the product loop

                                //Product quantity desired
                                string productName = operations.GetProduct(stock[productInp - 1].productID).name;
                                Console.WriteLine("===== " + productName + ": " + stock[productInp - 1].quantity + " in stock");
                                Console.WriteLine("<Enter product quantity to add, or 0 to return to product selection>");
                                var quantityInp = front.GetNumericalInput (0, int.MaxValue);
                                if (quantityInp > 0) {
                                    operations.AddInventory(stock[productInp - 1], quantityInp);
                                    Console.WriteLine("== " + quantityInp + " " + productName + "s added to the store ==");
                                }
                            }
                        }
                        else if (mngInp == 2) {
                            front.DisplayStoreOrderHistory(stores[storeInp - 1], operations);
                        }

                    }
                }

            }

        }


        


    }
}
