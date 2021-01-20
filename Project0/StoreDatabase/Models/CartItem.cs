using System;
using StoreDatabase.Models;

public class CartItem {

    public int storeID;
    public int productID;
    public int quantity;

    public CartItem (int l, int p, int q) {
        this.storeID = l;
        this.productID = p;
        this.quantity = q;
    }

    public void AddQuantity (int q) {
        quantity += q;
    }

}