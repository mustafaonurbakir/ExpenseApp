using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseApplication.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace ExpenseApplication.Engine.Handlers
{
    public class UserHandler
    {
        
        
        public static string GetUserRoleId(string id)
        {
            ExpensesEntities db = new ExpensesEntities();
            return db.AspNetUsers.Find(id).RoleId;
        }

        public static string GetUserRole(string id)
        {
            ExpensesEntities db = new ExpensesEntities();
            return db.AspNetRoles.Find(db.AspNetUsers.Find(id).RoleId).Name;
        }
    }
}
