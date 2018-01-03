using System;
namespace MyAppServices.Models
{
    public class payment
    {
        public int pay_id { get; set; }
        public int cust_id { get; set; }
        public int account_no { get; set; }
        public string account_holder { get; set; }
        public int cvv { get; set; }

        public payment(int Id, int Cust_id, int AccountNo, string AccountHolder, int Cvv)
        {
            pay_id = Id;
            cust_id = Cust_id;
            account_no = AccountNo;
            account_holder = AccountHolder;
            cvv = Cvv;
        }

        public payment(int Cust_id, int AccountNo, string AccountHolder, int Cvv)
        {
            cust_id = Cust_id;
            account_no = AccountNo;
            account_holder = AccountHolder;
            cvv = Cvv;
        }
        public payment()
        {
        }
    }
}
