using System;


namespace StoreDatabase.Models {
    public class Order 
    {

        public int orderID {get; set;}
        public string email {get; set;}
        public int storeID {get; set;}
        public int productID {get; set;}
        public int quantity {get; set;}
        public DateTime date {get; set;}

        // public Order (string e, int s, int p, int q) {
        //     email = e;
        //     storeID = s;
        //     productID = p;
        //     quantity = q;
        // }

    }
}