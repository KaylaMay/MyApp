using System;
namespace MyAppServices.Models
{
    public class orders
    {
        public int Order_id { set; get; }
        public int cust_id { set; get; }
        public int prod_id { set; get; }
        public string prod_name { set; get; }
        public int quantity { set; get; }
        public double total_price { set; get; }
        public string Delivery_Address { set; get; }

        public orders(int orderid, int custid, int prodid, string Prodname, int Quantity, double price, string delivery_add)
        {
            Order_id = orderid;
            cust_id = custid;
            prod_id = prodid;
            prod_name = Prodname;
            quantity = Quantity;
            total_price = price;
            Delivery_Address = delivery_add;
        }
        public orders(int custid, int prodid, string Prodname, int Quantity, double price, string delivery_add)
        {
            cust_id = custid;
            prod_id = prodid;
            prod_name = Prodname;
            quantity = Quantity;
            total_price = price;
            Delivery_Address = delivery_add;
        }
        public orders()
        {
        }
    }
}
