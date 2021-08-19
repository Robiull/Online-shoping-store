using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class LoginController : Controller
    {
        string connectionString = @"Data Source = DESKTOP-3IDLQ84;  Initial Catalog =OnlineShoppingStore;  Integrated Security  = true";
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
    }
}