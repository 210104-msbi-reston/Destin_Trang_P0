using System.Collections.Generic;

namespace StoreDatabase.Models {
public class Inventory //: IInventory 
    {

        public int storeID { get; set; }
        public int productID { get; set; }
        public int quantity { get; set; }

        //public virtual Location location { get; set; }
        //public virtual Product product { get; set; }

    }
}