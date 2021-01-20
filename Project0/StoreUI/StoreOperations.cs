using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using StoreDatabase.Models;

public class StoreOperations {



    public void PageBreak (int l) {
        for (int i = 0; i < l; i++) {
            Console.WriteLine("\n");
        }
    }


    //Simple function to check if password is correct
    public bool ValidatePassword (string pw) {
        if (pw == "RevatureIsAwesome") {
            return true;
        }
        else {
            return false;
        }
    }


    //Used to receive numerical input from the user
    //Validates to input to ensure it's within the bounds (inclusive)
    public int GetNumericalInput (int min, int max) {

        while (true) {
            int result = 0;
            try { result = Int32.Parse( Console.ReadLine()); }
            catch (FormatException) { 
                Console.WriteLine("!!! Invalid input! Only numeric values allowed !!!"); 
                continue;
            }
            if (result >= min && result <= max) {
                return result;
            }
            else {
                Console.WriteLine("!!! Invalid input! Please enter again !!!");
            }
        }

    }



    //Used to receive customer input
    //Customers can input a number, but also can open their shopping cart, past orders, and checkout as well
    public int GetCustomerInput (int min, int max) {
        while (true) {
            int result = 0;
            char c = '\0';
            var input = Console.ReadLine();
            try { result = Int32.Parse( input ); }
            catch (FormatException) { 
                try { c = char.Parse(input); }
                catch (Exception) { 
                    Console.WriteLine("Invalid"); 
                    continue;
                }
            }

            if (c == 'S' || c == 's') {
                //Signal to display shopping cart
                return -1;
            }
            else if (c == 'o' || c == 'O') {
                return -2;
            }
            else {
                if (result >= min && result <= max) {
                    return result;
                }
                else {
                    Console.WriteLine("!!! Invalid input! Please enter again !!!");
                }
            }

            
        }
    }


    public void DisplayCustomerOrderHistory (DatabaseOperations d) {

        PageBreak(1);
        Console.WriteLine ("To view your order history, log in with your email");
        string e = Console.ReadLine();

        //Make sure there is at least an order using this email!
        if (!d.ValidateEmail(e)) {
            Console.WriteLine("!!! User email not found !!!");
            return;
        }

        PageBreak(1);
        List<Order> storeOrders = d.GetOrders(e);
        foreach (Order o in storeOrders) {
            decimal price = d.GetProduct(o.productID).price * o.quantity;
            Console.WriteLine(
                string.Format("{0, -4}", o.orderID) + "   |   " + 
                string.Format("{0, -25}", o.email) + "   |   " + 
                string.Format("{0, -20}", d.GetProduct(o.productID).name) + "  |   $" + 
                string.Format("{0, -8}", price) + "  |  QTY: " + 
                o.quantity + "  |  @ " + 
                o.date );
        }

        while (true) {
            Console.WriteLine ("===== Sorting options:");
            Console.WriteLine ("<1: Oldest to newest  |  2: Newest to oldest>");
            Console.WriteLine ("<3: By ascending price  |  4: By descending price>");
            Console.WriteLine ("<0: Exit>");
            int input = GetNumericalInput (0, 4);

            if (input == 1) {
                PageBreak(3);
                storeOrders = d.GetOrdersOldToNew(e);
                foreach (Order o in storeOrders) {
                    decimal price = d.GetProduct(o.productID).price * o.quantity;
                    Console.WriteLine(
                        string.Format("{0, -4}", o.orderID) + "   |   " + 
                        string.Format("{0, -25}", o.email) + "   |   " + 
                        string.Format("{0, -20}", d.GetProduct(o.productID).name) + "  |   $" + 
                        string.Format("{0, -8}", price) + "  |  QTY: " + 
                        o.quantity + "  |  @ " + 
                        o.date );
                }
            }
            else if (input == 2) {
                PageBreak(3);
                storeOrders = d.GetOrdersNewToOld(e);
                foreach (Order o in storeOrders) {
                    decimal price = d.GetProduct(o.productID).price * o.quantity;
                    Console.WriteLine(
                        string.Format("{0, -4}", o.orderID) + "   |   " + 
                        string.Format("{0, -25}", o.email) + "   |   " + 
                        string.Format("{0, -20}", d.GetProduct(o.productID).name) + "  |   $" + 
                        string.Format("{0, -8}", price) + "  |  QTY: " + 
                        o.quantity + "  |  @ " + 
                        o.date );
                }
            }
            else if (input == 3) {
                PageBreak(3);
                storeOrders = d.GetOrdersLowToHigh(e);
                foreach (Order o in storeOrders) {
                    decimal price = d.GetProduct(o.productID).price * o.quantity;
                    Console.WriteLine(
                        string.Format("{0, -4}", o.orderID) + "   |   " + 
                        string.Format("{0, -25}", o.email) + "   |   " + 
                        string.Format("{0, -20}", d.GetProduct(o.productID).name) + "  |   $" + 
                        string.Format("{0, -8}", price) + "  |  QTY: " + 
                        o.quantity + "  |  @ " + 
                        o.date );
                }
            }
            else if (input == 4) {
                PageBreak(3);
                storeOrders = d.GetOrdersHighToLow(e);
                foreach (Order o in storeOrders) {
                    decimal price = d.GetProduct(o.productID).price * o.quantity;
                    Console.WriteLine(
                        string.Format("{0, -4}", o.orderID) + "   |   " + 
                        string.Format("{0, -25}", o.email) + "   |   " + 
                        string.Format("{0, -20}", d.GetProduct(o.productID).name) + "  |   $" + 
                        string.Format("{0, -8}", price) + "  |  QTY: " + 
                        o.quantity + "  |  @ " + 
                        o.date );
                }
            }
            else if (input == 0) break;
        }

    }
    public void DisplayStoreOrderHistory (Location s, DatabaseOperations d) {

        PageBreak(2);
        List<Order> storeOrders = d.GetOrders(s);
        foreach (Order o in storeOrders) {
            decimal price = d.GetProduct(o.productID).price * o.quantity;
            Console.WriteLine(
                string.Format("{0, -4}", o.orderID) + "   |   " + 
                string.Format("{0, -25}", o.email) + "   |   " + 
                string.Format("{0, -20}", d.GetProduct(o.productID).name) + "  |   $" + 
                string.Format("{0, -8}", price) + "  |  QTY: " + 
                o.quantity + "  |  @ " + 
                o.date );
        }

        while (true) {
            Console.WriteLine ("===== Sorting options:");
            Console.WriteLine ("<1: Oldest to newest  |  2: Newest to oldest>");
            Console.WriteLine ("<3: By ascending price  |  4: By descending price>");
            Console.WriteLine ("<0: Exit>");
            int input = GetNumericalInput (0, 4);

            if (input == 1) {
                PageBreak(3);
                storeOrders = d.GetOrdersOldToNew(s);
                foreach (Order o in storeOrders) {
                    decimal price = d.GetProduct(o.productID).price * o.quantity;
                    Console.WriteLine(
                        string.Format("{0, -4}", o.orderID) + "   |   " + 
                        string.Format("{0, -25}", o.email) + "   |   " + 
                        string.Format("{0, -20}", d.GetProduct(o.productID).name) + "  |   $" + 
                        string.Format("{0, -8}", price) + "  |  QTY: " + 
                        o.quantity + "  |  @ " + 
                        o.date );
                }
            }
            else if (input == 2) {
                PageBreak(3);
                storeOrders = d.GetOrdersNewToOld(s);
                foreach (Order o in storeOrders) {
                    decimal price = d.GetProduct(o.productID).price * o.quantity;
                    Console.WriteLine(
                        string.Format("{0, -4}", o.orderID) + "   |   " + 
                        string.Format("{0, -25}", o.email) + "   |   " + 
                        string.Format("{0, -20}", d.GetProduct(o.productID).name) + "  |   $" + 
                        string.Format("{0, -8}", price) + "  |  QTY: " + 
                        o.quantity + "  |  @ " + 
                        o.date );
                }
            }
            else if (input == 3) {
                PageBreak(3);
                storeOrders = d.GetOrdersLowToHigh(s);
                foreach (Order o in storeOrders) {
                    decimal price = d.GetProduct(o.productID).price * o.quantity;
                    Console.WriteLine(
                        string.Format("{0, -4}", o.orderID) + "   |   " + 
                        string.Format("{0, -25}", o.email) + "   |   " + 
                        string.Format("{0, -20}", d.GetProduct(o.productID).name) + "  |   $" + 
                        string.Format("{0, -8}", price) + "  |  QTY: " + 
                        o.quantity + "  |  @ " + 
                        o.date );
                }
            }
            else if (input == 4) {
                PageBreak(3);
                storeOrders = d.GetOrdersHighToLow(s);
                foreach (Order o in storeOrders) {
                    decimal price = d.GetProduct(o.productID).price * o.quantity;
                    Console.WriteLine(
                        string.Format("{0, -4}", o.orderID) + "   |   " + 
                        string.Format("{0, -25}", o.email) + "   |   " + 
                        string.Format("{0, -20}", d.GetProduct(o.productID).name) + "  |   $" + 
                        string.Format("{0, -8}", price) + "  |  QTY: " + 
                        o.quantity + "  |  @ " + 
                        o.date );
                }
            }
            else if (input == 0) break;
        }

    }


    public void DisplayShoppingCart (ShoppingCart c, DatabaseOperations d) {
        
        //Shopping cart total price
        decimal total = 0;

        Console.WriteLine("===== The items in your cart:");
        foreach (CartItem i in c.cart) {
            Product p = d.GetProduct(i.productID);
            total += (p.price * i.quantity);

            Console.WriteLine(
                string.Format("{0, -20}", p.name) + "  |  " + 
                string.Format("{0, -8}", p.price) + "  |  " + 
                string.Format("{0, -20}", d.GetLocation(i.storeID).city) + "  |  x" + 
                i.quantity);
        }

        PageBreak(1);
        Console.WriteLine("===== CART TOTAL: " + total.ToString());
        Console.WriteLine("<Enter 1 if you'd like to proceed to checkout, or 0 to return to the previous menu>");
        int input = GetNumericalInput(0, 1);

        if (input == 0) { return; }
        Console.WriteLine("===== Please enter an email address so that we may send a confirmation and tracking number:");
        string email = Console.ReadLine();
        d.PurchaseOrder(email, c);
        Console.WriteLine("===== Purchase confirmed! Thank you for shopping");
        PageBreak(1);

    }
    



}