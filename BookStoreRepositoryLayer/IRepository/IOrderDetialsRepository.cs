using BookStoreCommonLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreRepositoryLayer.IRepository
{
    public interface IOrderDetialsRepository
    {
       object PlaceOrder(OrderDetails orderDetail);
       IEnumerable<OrderDetails> ViewOrderPlaced();
       bool OrderConfirmationToEmail(int UserId, int orderid);
    }
}
