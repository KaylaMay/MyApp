using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MyAppServices.Models;

namespace MyAppServices.Controllers
{
    public class PaymentController : ApiController
    {
        static DataAccess dta = new DataAccess();

        [System.Web.Http.HttpPost]
        //[Route("api/Register")]
        [System.Web.Http.Route("api/Payments")]
        public string PostCust(payment pay)
        {
            if (pay != null)
            {
                return dta.AddPayment(pay);
            }
            return "Unable to add payment";
        }


        //Get all Customers information stored in the database
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetPayment")]
        public IEnumerable<payment> GetPayments()
        {
            return dta.GetPayments();
        }

    }
}
