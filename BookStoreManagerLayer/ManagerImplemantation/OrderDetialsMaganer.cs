using BookStoreCommonLayer.BookModel;
using BookStoreManagerLayer.IManager;
using BookStoreRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManagerLayer.ManagerImplemantation
{
    public class OrderDetialsMaganer: IOrderDetialsManager
    {
        private IOrderDetialsRepository _BookStore;
        public OrderDetialsMaganer(IOrderDetialsRepository BookStore)
        {
            _BookStore = BookStore;
        }

        public bool OrderConfirmationToEmail(int UserId, int orderid)
        {
            return this._BookStore.OrderConfirmationToEmail( UserId,orderid);
        }

        public object PlaceOrder(OrderDetails orderDetail)
        {
            return this._BookStore.PlaceOrder(orderDetail);
        }

        public object ViewOrderPlaced()
        {
            return this._BookStore.ViewOrderPlaced();
        }
    }
}
