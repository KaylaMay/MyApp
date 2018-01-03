using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MyAppServices.Models;

namespace MyAppServices.Controllers
{
    public class OrderController : ApiController
    {
        static DataAccess dta = new DataAccess();

        [HttpPost]
        //[Route("api/Order")]
        [Route("api/Orders")]
        public string PostCust(orders order)
        {
            if (order != null)
            {
                return dta.AddOrder(order);
            }
            return "Unable to Add the Order";
        }


        //Get all Order information stored in the database
        [HttpGet]
        [Route("api/GetOrders")]
        public IEnumerable<orders> GetOrders()
        {
            return dta.GetOrders();
        }


    }
}
