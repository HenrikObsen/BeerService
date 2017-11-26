using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BeerService.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //AutoGen ag = new AutoGen("BeerService",Request.PhysicalApplicationPath); 
            
            return View();
        }
    }
}