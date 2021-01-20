using System;
using System.Collections.Generic;

namespace StoreDatabase.Models {
public class Product //: IProduct 
    {

        public int productID { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }

        //public virtual ICollection<Inventory> inventories { get; set; }

    }
}