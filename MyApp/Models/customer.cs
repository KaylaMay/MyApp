using System;
namespace MyApp.Models
{
    public class customer
    {
        public int cust_id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string phone { get; set; }

        public customer(string Name, string Surname, string Email, string Password)
        {
            name = Name;
            surname = Surname;
            email = Email;
            password = Password;
        }
        public customer(int Cust_id, string Name, string Surname, string Email, string Password)
        {
            cust_id = Cust_id;
            name = Name;
            surname = Surname;
            email = Email;
            password = Password;
        }
        public customer(string Email, string Password)
        {
            email = Email;
            password = Password;
        }
        public customer(int Cust_id, string Name, string Surname, string Email, string Password, string gender, string address, string Phone)
        {
            cust_id = Cust_id;
            name = Name;
            surname = Surname;
            email = Email;
            password = Password;
            Gender = gender;
            Address = address;
            phone = Phone;

        }

        public customer()
        {

        }
    }
}
