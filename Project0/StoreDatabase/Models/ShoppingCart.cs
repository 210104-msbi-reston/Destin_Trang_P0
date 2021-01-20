using System;
using StoreDatabase.Models;
using System.Collections.Generic;

public class ShoppingCart {

    public List<CartItem> cart;

    public ShoppingCart () {
        cart = new List<CartItem>();
    }


    public void AddToCart (int pID, int sID, int q) {

        CartItem existing = CheckForMatch(pID, sID);
        if (existing != null) {
            existing.AddQuantity(q);
        }
        else {
            cart.Add(new CartItem(sID, pID, q));
        }

    }
    //Searches the cart to see if theres already a matching product from the same location
    //In which case we add to that quantity, instead of making a new cart item
    private CartItem CheckForMatch (int p, int s) {
        foreach (CartItem i in cart) {
            if (p == i.productID && s == i.storeID) {
                return i;
            }
        }
        return null;
    }

}