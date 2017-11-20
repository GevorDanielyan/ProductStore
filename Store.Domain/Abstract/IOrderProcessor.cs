using System;
using Store.Domain.Entities;

namespace Store.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(ShoppingCart cart, ShippingDetails shippingDetails);
    }
}
