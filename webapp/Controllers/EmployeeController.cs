using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

        // GET: Manager
        
        public ActionResult CreateExpense()
        {
            return View();
        }

        public ActionResult ExpensesDetails(int? gelenid)
        {
            HttpContext.Session["CurrentDetailId"] = gelenid;
            return View(gelenid);
        }


    }
}