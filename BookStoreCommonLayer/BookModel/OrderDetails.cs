using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreCommonLayer.BookModel
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PinCode { get; set; }
        public int Phone_no { get; set; }
        public string PaymentMethod { get; set; }
        public int Quantity { get; set; }
        public int UUserId { get; set; }
        public int BBookId { get; set; }

    }
}
