using System;
namespace MyAppServices.Models
{
    public class product
    {
        public int prod_id { get; set; }
        public string prod_name { get; set; }
        public string prod_desc { get; set; }
        public int rest_id { get; set; }
        public double prod_price { get; set; }
        public byte[] prod_pic { get; set; }



        public product(int id, string pname, string pdesc, int resid, double pprice, byte[] ppic)
        {
            prod_id = id;
            prod_name = pname;
            prod_desc = pdesc;
            rest_id = resid;
            prod_price = pprice;
            prod_pic = ppic;
        }


        public product(string pname, string pdesc, int resid, double pprice, byte[] ppic)
        {
            prod_name = pname;
            prod_desc = pdesc;
            rest_id = resid;
            prod_price = pprice;
            prod_pic = ppic;
        }
        public product()
        {
        }
    }
}
