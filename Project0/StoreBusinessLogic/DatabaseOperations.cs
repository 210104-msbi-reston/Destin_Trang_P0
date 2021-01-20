using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using StoreDatabase.Models;


public class DatabaseOperations {

    Project0DBContext db = new Project0DBContext();
    int order_counter = 1;


    public Product GetProduct (int pID) {
        
        var product = (
            from d in db.ProductTable
            where d.productID == pID
            select d
        ).Single();
        return product;

    }
    public Location GetLocation (int sID) {

        var loc = (
            from d in db.LocationTable
            where d.storeID == sID
            select d
        ).Single();
        return loc;

    }

    //Returns a list of all store locations in the database
    public List<Location> GetAllLocations () {

        return db.LocationTable.ToList();

    }

    //Returns the inventory for a given store
    public List<Inventory> GetStoreInventory (Location store) {
        var inventory = (
            from i in db.InventoryTable
            where i.storeID == store.storeID
            select i
        ).ToList();
        return inventory;
    }


    //Checks if there is at least one order with the given email
    public bool ValidateEmail (string e) {
        var o = (
            from i in db.OrderTable
            where i.email == e
            select i
        ).ToList();
        if (o.Count == 0) return false;
        else return true;
    }


    //Get orders by a specific user
    public List<Order> GetOrders (string e) {
        var o = (
            from i in db.OrderTable
            where i.email == e
            select i
        ).ToList();
        return o;
    }
    public List<Order> GetOrdersOldToNew (string e) {
        var o = (
            from i in db.OrderTable
            where i.email == e
            orderby i.date
            select i
        ).ToList();
        return o;
    }
    public List<Order> GetOrdersNewToOld (string e) {
        var o = (
            from i in db.OrderTable
            where i.email == e
            orderby i.date descending
            select i
        ).ToList();
        return o;
    }
    public List<Order> GetOrdersLowToHigh (string e) {
        var o = (
            from i in db.OrderTable
            join p in db.ProductTable
            on i.productID equals p.productID
            where i.email == e
            orderby i.quantity * p.price
            select i
        ).ToList();
        return o;
    }
    public List<Order> GetOrdersHighToLow (string e) {
        var o = (
            from i in db.OrderTable
            join p in db.ProductTable
            on i.productID equals p.productID
            where i.email == e
            orderby i.quantity * p.price descending
            select i
        ).ToList();
        return o;
    }
    

    //Get orders at a specific location
    public List<Order> GetOrders (Location s) {
        var o = (
            from i in db.OrderTable
            where i.storeID == s.storeID
            select i
        ).ToList();
        return o;
    }
    public List<Order> GetOrdersOldToNew (Location s) {
        var o = (
            from i in db.OrderTable
            where i.storeID == s.storeID
            orderby i.date
            select i
        ).ToList();
        return o;
    }
    public List<Order> GetOrdersNewToOld (Location s) {
        var o = (
            from i in db.OrderTable
            where i.storeID == s.storeID
            orderby i.date descending
            select i
        ).ToList();
        return o;
    }
    public List<Order> GetOrdersLowToHigh (Location s) {
        var o = (
            from i in db.OrderTable
            join p in db.ProductTable
            on i.productID equals p.productID
            where i.storeID == s.storeID
            orderby i.quantity * p.price 
            select i
        ).ToList();
        return o;
    }
    public List<Order> GetOrdersHighToLow (Location s) {
        var o = (
            from i in db.OrderTable
            join p in db.ProductTable
            on i.productID equals p.productID
            where i.storeID == s.storeID
            orderby i.quantity * p.price descending
            select i
        ).ToList();
        return o;
    }


    public void PurchaseOrder (string e, ShoppingCart c) {

        foreach (CartItem i in c.cart) {
            order_counter ++;
            Order newOrder = new Order {
                orderID = order_counter,
                email = e,
                storeID = i.storeID,
                productID = i.productID,
                quantity = i.quantity,
                date = DateTime.Now
            };
            db.OrderTable.Add(newOrder);
            db.SaveChanges(); 
        }

    }


    public void DepleteInventory (Inventory inv, int qty) {
        inv.quantity = inv.quantity - qty;
        //db.SaveChanges();
    }
    public void AddInventory (Inventory inv, int qty) {
        inv.quantity = inv.quantity + qty;
        db.SaveChanges();
    }


}