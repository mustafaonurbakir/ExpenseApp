using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace SmartAdminMvc.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Expenses()
        {
            return View();
        }

        public ActionResult ExpensesDetails(int? id)
        {
            return View(id);
        }

    }
}