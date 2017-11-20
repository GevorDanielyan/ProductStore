using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class ShoppingCart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        //Adding an item to the cart
        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(g => g.Product.ProductId == product.ProductId).FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        //Remove an item from the shopping cart
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        /*Calculating the total cost of items in the basket*/
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        //Cleaning the basket by removing all items
        public void Clear()
        {
            lineCollection.Clear();
        }

        /*Property that allows to access the contents of the Recycle Bin 
         using IEnumerable<CartLine>*/
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    //Represents the goods selected by the user, as well as the quantity
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
