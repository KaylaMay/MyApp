using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MyAppServices.Models;

namespace MyAppServices.Controllers
{
    public class RestaurantController : ApiController
    {
        static DataAccess dta = new DataAccess();

        //Get all the restaurants information stored in the database
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Restaurants")]
        public IEnumerable<restaurant> GetAllRestaurants()
        {
            return dta.GetRestaurant();
        }
    }
}
