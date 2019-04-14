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
using ExpenseApplication.Data;

namespace SmartAdminMvc.Api
{
    public class ExpenseAPIController : ApiController
    {
        private ExpensesDB db = new ExpensesDB();

        // GET: api/ExpenseAPI
        public IQueryable<Expense> GetExpense()
        {
            return db.Expense;
        }

        // GET: api/ExpenseAPI/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult GetExpense(int id)
        {
            Expense expense = db.Expense.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            return Ok(expense);
        }

        // PUT: api/ExpenseAPI/5
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

            db.Entry(expense).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ExpenseAPI
        [ResponseType(typeof(Expense))]
        public IHttpActionResult PostExpense(Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Expense.Add(expense);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = expense.Id }, expense);
        }

        // DELETE: api/ExpenseAPI/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult DeleteExpense(int id)
        {
            Expense expense = db.Expense.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            db.Expense.Remove(expense);
            db.SaveChanges();

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

        private bool ExpenseExists(int id)
        {
            return db.Expense.Count(e => e.Id == id) > 0;
        }
    }
}