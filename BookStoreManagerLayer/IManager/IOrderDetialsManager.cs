using BookStoreCommonLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagerLayer.IManager
{
    public interface IOrderDetialsManager
    {
       object PlaceOrder(OrderDetails orderDetail);
       object ViewOrderPlaced();
       bool OrderConfirmationToEmail(int UserId, int orderid);
    }
}
