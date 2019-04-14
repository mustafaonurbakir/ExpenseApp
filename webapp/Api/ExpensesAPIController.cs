using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SmartAdminMvc.Models;
using ExpenseApplication.Data;
using ExpenseApplication.Engine.Handlers;
using System.Web;
using ExpenseApplication.Engine.Request;
using ExpenseApplication.Engine.Response;
using ExpenseApplication.Engine.Domain;

namespace SmartAdminMvc.api// controller
{
    public class ExpensesAPIController : ApiController
    {
        //private HttpContextBase Context { get; set; }
        private ExpensesEntities db = new ExpensesEntities();
        
        // GET: api/ExpensesAPI
        public IEnumerable<Expense> GetExpenses()
        {
            return ExpenseHandler.getallrecords();
        }

        // GET: api/ExpensesAPI/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult GetExpense(int id)
        {
            Expense expense = ExpenseHandler.getexpensebyid(id);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        // PUT: api/ExpensesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExpense(int id, Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expense.Id)
            {
                return BadRequest();
            }

            //////////////////////////////////bakılacak
            bool rep = ExpenseHandler.updateexpense(expense, id);
            if (!rep)
            {
                if (ExpenseHandler.ExpenseExists(id))
                {
                    return NotFound();
                }
                
            }
            
            return StatusCode(HttpStatusCode.NoContent);
        }





        [HttpPost]
        public IEnumerable<ExpenseDomain> getExpenseByUserId(dynamic User)
        {
            var response = new GetExpenseByUserIdResponse();
            string Id = User.User.ToString();
            response.Expenses = ExpenseHandler.GetExpenseByUserId(Id);
            
            return response.Expenses;
        }

       
        [HttpPost]
        public IEnumerable<ExpenseDomain> GetExpenseByStatus(/*StatusRequest request*/ dynamic Status)
        {
            
            var response = new GetExpenseByUserIdResponse();
            string Role = Status.Role.ToString();
            response.Expenses = ExpenseHandler.GetExpenseByStatus(/*request.Status*/ Role);

            return response.Expenses;
        }

        [HttpPost]
        public IHttpActionResult saveExpense(dynamic data)
        {

            ExpenseHandler.saveexpense(data);

            foreach (var item in data.expenseitemdata)
            {
                var gewgwe = item.ExpenseType;
            }

            var asd = data;
            return StatusCode(HttpStatusCode.NoContent);
        }






        // POST: api/ExpensesAPI
        [ResponseType(typeof(Expense))]
        public IHttpActionResult PostExpense(Expense expense  )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            ExpenseHandler.saveexpense(expense);

            return CreatedAtRoute("DefaultApi", new { id = expense.Id }, expense);
        }

        // DELETE: api/ExpensesAPI/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult DeleteExpense(int id)
        {
            Expense expense = ExpenseHandler.deleteexpensebyid(id);

            if (expense == null)
            {
                return NotFound();
            }
            
            return Ok(expense);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        





    }
}