using System;
namespace MyApp.Models
{
    public class restaurant
    {
        public int rest_id { get; set; }
        public string rest_name { get; set; }
        public string rest_location { get; set; }
        public string phone { get; set; }
        public byte[] rest_pic { get; set; }

        public restaurant(string Rest_name, string Rest_location, string Phone, byte[] Rest_pic)
        {
            rest_name = Rest_name;
            rest_location = Rest_location;
            phone = Phone;
            rest_pic = Rest_pic;

        }
        public restaurant(int Rest_id, string Rest_name, string Rest_location, string Phone, byte[] Rest_pic)
        {
            rest_id = Rest_id;
            rest_name = Rest_name;
            rest_location = Rest_location;
            phone = Phone;
            rest_pic = Rest_pic;

        }
        public restaurant()
        {
        }
    }
}
