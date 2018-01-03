using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MyAppServices.Models;

namespace MyAppServices.Controllers
{
    public class CustomerController : ApiController
    {
        static DataAccess dta = new DataAccess();

        [System.Web.Http.HttpPost]
        //[Route("api/Register")]
        [System.Web.Http.Route("api/Register")]
        public string PostCust(customer cust)
        {
            if (cust != null)
            {
                return dta.AddCust(cust);
            }
            return "Unable to Register the customer";
        }


        //Get all Customers information stored in the database
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetCustomers")]
        public IEnumerable<customer> GetCustomers()
        {
            return dta.GetCust();
        }


        //Login into the system
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/CustomersLogin")]
        public customer GetCust(string email, string password)
        {
            return dta.CustomerLogin(email, password);
        }

        //Update
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/UpdateCustomer")]
        public customer UpdateCustomer(customer cust, int id)
        {
            return dta.CustUpdate(cust, id);
        }

    }
}
