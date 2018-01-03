using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MyAppServices.Models
{
    public class DataAccess
    {
        static string connectString = "SERVER=localhost;UID=root;Password=newpass;DATABASE=UberEats;";
        static MySqlDataReader read;

        //Register
        public string AddCust(customer cust)
        {
            string x = "";
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                string query = "INSERT INTO UberEats.Customer(name,surname,email,password) " +
                    "VALUES('" + cust.name + "','" + cust.surname + "','" + cust.email + "','" + cust.password + "');";
                using (MySqlCommand comma = new MySqlCommand(query, connect))
                {
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.AddWithValue("@name", cust.name);
                        comma.Parameters.AddWithValue("@surnname", cust.surname);
                        comma.Parameters.AddWithValue("@email", cust.email);
                        comma.Parameters.AddWithValue("@password", cust.password);
                        int y = comma.ExecuteNonQuery();

                        x = y.ToString();

                    }
                    catch (MySqlException exception)
                    {
                        comma.Connection.Close();
                        exception.ToString();

                    }
                }
                return null;
            }
        }

        //Login
        public customer CustomerLogin(string email, string password)
        {
            string sql = "SELECT cust_id,name,surname,email,password FROM UberEats.Customer WHERE email='" + email + "'AND password='" + password + "';";

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                MySqlCommand comma = new MySqlCommand(sql, connect);
                comma.Connection = connect;

                try
                {
                    comma.Connection.Open();
                    comma.Parameters.Add(new MySqlParameter("@email", email));
                    comma.Parameters.Add(new MySqlParameter("@password", password));

                    read = comma.ExecuteReader();


                    while (read.Read())
                    {
                        return new customer(Convert.ToInt32(read[("cust_id")]),
                                            Convert.ToString(read[("name")]),
                                            Convert.ToString(read[("surname")]),
                                            Convert.ToString(read["email"]),
                                            Convert.ToString(read["password"]));
                    }
                    read.Close();
                }
                catch (MySqlException exception)
                {
                    comma.Connection.Close();
                    exception.ToString();
                }
                return null;
            }
        }

        //GettingAllCustomersIn TheDatabase
        public customer[] GetCust()
        {
            string sql = "Select * from UberEats.Customer;";
            List<customer> cust = new List<customer>();

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                MySqlCommand comma = new MySqlCommand(sql, connect);
                comma.Connection = connect;


                try
                {
                    comma.Connection.Open();
                    customer obj = new customer();
                    read = comma.ExecuteReader();
                    while (read.Read())
                    {
                        obj = new customer(Convert.ToInt32(read[("cust_id")]),
                                            Convert.ToString(read[("name")]),
                                            Convert.ToString(read[("surname")]),
                                            Convert.ToString(read["email"]),
                                            Convert.ToString(read["password"]));
                        cust.Add(obj);
                    }

                    read.Close();

                    MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                    reader.Read();
                    reader.Close();
                }

                catch (MySqlException exception)
                {
                    comma.Connection.Close();
                    exception.ToString();
                }

                return cust.ToArray();
            }

        }

        //CustUpdate

        public customer CustUpdate(customer cust, int id)
        {
            string sql = "UPDATE UberEats.Customer SET surname='" + cust.name + "',Gender='" + cust.surname + "',Address='" + cust.Address + "',phone='" + cust.phone + "' WHERE cust_id=" + id + ";";
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                using (MySqlCommand comma = new MySqlCommand(sql, connect))
                {

                    comma.Connection = connect;
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.Add(new MySqlParameter("@surname", cust.surname));
                        comma.Parameters.Add(new MySqlParameter("@Gender", cust.Gender));
                        comma.Parameters.Add(new MySqlParameter("@Address", cust.Address));
                        comma.Parameters.Add(new MySqlParameter("@phone", cust.phone));


                        read = comma.ExecuteReader();
                        while (read.Read())
                        {
                            cust = new customer(Convert.ToString(read["Firstname"]),
                                                Convert.ToString(read["Lastname"]),
                                                Convert.ToString(read["Email"]),
                                                Convert.ToString(read["Password"])

                                           );
                        }
                        read.Close();

                    }
                    catch (MySqlException exception)
                    {
                        exception.ToString();

                    }
                    finally
                    {
                        comma.Connection.Close();
                    }
                }
                return cust;
            }
        }

        //GettingAllRestaurants In TheDatabase
        public restaurant[] GetRestaurant()
        {
            string sql = "Select * from UberEats.Restaurant;";
            List<restaurant> restu = new List<restaurant>();

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                MySqlCommand comma = new MySqlCommand(sql, connect);
                comma.Connection = connect;


                try
                {
                    comma.Connection.Open();
                    restaurant res = new restaurant();
                    read = comma.ExecuteReader();
                    while (read.Read())
                    {
                        res = new restaurant(Convert.ToInt32(read[("rest_id")]),
                                            Convert.ToString(read[("rest_name")]),
                                            Convert.ToString(read[("rest_location")]),
                                            Convert.ToString(read["phone"]),
                                                (byte[])(read["rest_pic"])
                                               );
                        restu.Add(res);
                    }

                    read.Close();

                    MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                    reader.Read();
                    reader.Close();
                }

                catch (MySqlException exception)
                {
                    comma.Connection.Close();
                    exception.ToString();
                }

                return restu.ToArray();
            }

        }

        //GettingAllProducts 
        public product[] GetProduct()
        {
            string sql = "Select * from UberEats.Product;";
            List<product> prod = new List<product>();

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                MySqlCommand comma = new MySqlCommand(sql, connect);
                comma.Connection = connect;


                try
                {
                    comma.Connection.Open();
                    product objp = new product();
                    read = comma.ExecuteReader();
                    while (read.Read())
                    {
                        objp = new product(Convert.ToInt32(read[("prod_id")]),
                                            Convert.ToString(read[("prod_name")]),
                                            Convert.ToString(read[("prod_desc")]),
                                            Convert.ToInt32(read["rest_id"]),
                                            Convert.ToDouble(read["prod_price"]),
                                            (byte[])(read["prod_pic"])
                                               );
                        prod.Add(objp);
                    }

                    read.Close();

                    MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                    reader.Read();
                    reader.Close();
                }

                catch (MySqlException exception)
                {
                    comma.Connection.Close();
                    exception.ToString();
                }

                return prod.ToArray();
            }

        }

        //Payment
        public string AddPayment(payment pay)
        {
            string x = "";
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                string query = "INSERT INTO UberEats.Payment(cust_id,account_no,account_holder,cvv) " +
                    "VALUES('" + pay.cust_id + "','" + pay.account_no + "','" + pay.account_holder + "','" + pay.cvv + ");";
                using (MySqlCommand comma = new MySqlCommand(query, connect))
                {
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.AddWithValue("@cust_id", pay.cust_id);
                        comma.Parameters.AddWithValue("@account_no", pay.account_no);
                        comma.Parameters.AddWithValue("@account_holder", pay.account_holder);
                        comma.Parameters.AddWithValue("@cvv", pay.cvv);
                        int y = comma.ExecuteNonQuery();
                        x = y.ToString();

                    }
                    catch (MySqlException exception)
                    {
                        comma.Connection.Close();
                        exception.ToString();

                    }
                }
                return null;
            }
        }

        //GettingAllPayments
        public payment[] GetPayments()
        {
            string sql = "Select * from UberEats.Payment;";
            List<payment> pay = new List<payment>();

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                MySqlCommand comma = new MySqlCommand(sql, connect);
                comma.Connection = connect;


                try
                {
                    comma.Connection.Open();
                    payment objpay = new payment();
                    read = comma.ExecuteReader();
                    while (read.Read())
                    {
                        objpay = new payment(Convert.ToInt32(read[("pay_id")]),
                                            Convert.ToInt32(read[("cust_id")]),
                                             Convert.ToInt32(read[("account_no")]),
                                             Convert.ToString(read["account_holder"]),
                                             Convert.ToInt32(read["cvv"]));

                        pay.Add(objpay);
                    }

                    read.Close();

                    MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                    reader.Read();
                    reader.Close();
                }

                catch (MySqlException exception)
                {
                    comma.Connection.Close();
                    exception.ToString();
                }

                return pay.ToArray();
            }

        }

        //Order
        public string AddOrder(orders Order)
        {
            string x = "";
            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                string query = "INSERT INTO UberEats.Orders(cust_id,prod_id,prod_name,quantity,total_price,Delivery_Address) " +
                    "VALUES('" + Order.cust_id + "','" + Order.prod_id + "','" + Order.prod_name + "','" + Order.quantity + "','" + Order.total_price + "','" +Order.Delivery_Address + ");";
                using (MySqlCommand comma = new MySqlCommand(query, connect))
                {
                    try
                    {
                        comma.Connection.Open();

                        comma.Parameters.AddWithValue("@cust_id", Order.cust_id);
                        comma.Parameters.AddWithValue("@prod_id", Order.prod_id);
                        comma.Parameters.AddWithValue("@prod_name", Order.prod_name);
                        comma.Parameters.AddWithValue("@quantity", Order.quantity);
                        comma.Parameters.AddWithValue("@total_price", Order.total_price);
                        comma.Parameters.AddWithValue("@Delevery_Address", Order.Delivery_Address);
                        int y = comma.ExecuteNonQuery();
                        x = y.ToString();

                    }
                    catch (MySqlException exception)
                    {
                        comma.Connection.Close();
                        exception.ToString();

                    }
                }
                return null;
            }
        }
        //GetAllOrders
        public orders[] GetOrders()
        {
            string sql = "Select * from UberEats.Orders;";
            List<orders> pay = new List<orders>();

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = connectString;
                MySqlCommand comma = new MySqlCommand(sql, connect);
                comma.Connection = connect;


                try
                {
                    comma.Connection.Open();
                    orders objOrder = new orders();
                    read = comma.ExecuteReader();
                    while (read.Read())
                    {
                        objOrder = new orders(Convert.ToInt32(read[("Order_id")]),
                                              Convert.ToInt32(read[("cust_id")]),
                                              Convert.ToInt32(read[("prod_id")]),
                                              Convert.ToString(read["prod_name"]),
                                              Convert.ToInt32(read["quantity"]),
                                              Convert.ToDouble(read["total_price"]),
                                              Convert.ToString(read["Delivery_Address"]));

                        pay.Add(objOrder);
                    }

                    read.Close();

                    MySqlDataReader reader = comma.ExecuteReader(System.Data.CommandBehavior.SingleRow);
                    reader.Read();
                    reader.Close();
                }

                catch (MySqlException exception)
                {
                    comma.Connection.Close();
                    exception.ToString();
                }

                return pay.ToArray();
            }

        }



    }
}
