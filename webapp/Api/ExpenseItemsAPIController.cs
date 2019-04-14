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
//using ExpenseApplication.Data;
using ExpenseApplication.Engine.Handlers;
using ExpenseApplication.Engine.Response;
using ExpenseApplication.Engine.Domain;
using ExpenseApplication.Data;

namespace SmartAdminMvc.Controllers
{
    public class ExpenseItemsAPIController : ApiController
    {
        private ExpensesEntities db = new ExpensesEntities();

        
        [HttpPost]
        public IEnumerable<ExpensesItemDomain> GetExpenseItem(dynamic DetailId)
        {
            var response = new GetExpenseItemById();
            string Id = DetailId.DetailId.ToString();
            response.ExpensesItem = ExpenseItemHandler.getallrecords(Id);

            return response.ExpensesItem;
            //return ExpenseItemHandler.getallrecords();
        }

        [HttpPost]
        public IHttpActionResult ApproveForm(dynamic DetailId)
        {
            var response = new GetExpenseItemById();
            string Id = DetailId.DetailId.ToString();
            return Ok(ExpenseItemHandler.ApproveForm(Id));
        }

        [HttpPost]
        public IHttpActionResult EditForm(dynamic DetailId)
        {
            var response = new GetExpenseItemById();
            string Id = DetailId.DetailId.ToString();
            return Ok(ExpenseItemHandler.PayForm(Id));//edit form
        }

        [HttpPost]
        public IHttpActionResult Payform(dynamic DetailId)
        {
            var response = new GetExpenseItemById();
            string Id = DetailId.DetailId.ToString();
            return Ok(ExpenseItemHandler.PayForm(Id));
        }

        public IHttpActionResult ConfirmReject(dynamic data)
        {
            var response = new GetExpenseItemById();
            //string Id = data.DetailId.ToString();
            //string RejectDefi
            return Ok(ExpenseItemHandler.ConfirmReject(data));
        }




        // GET: api/ExpenseItemsAPI/5
        [ResponseType(typeof(ExpenseItem))]
        public IHttpActionResult GetExpenseItem(int id)
        {
            ExpenseItem expenseItem = ExpenseItemHandler.getExpenseItembyid(id);
            if (expenseItem == null)
            {
                return NotFound();
            }
           
            return Ok(expenseItem);
        }

        // PUT: api/ExpenseItemsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExpenseItem(int id, ExpenseItem expenseItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != expenseItem.Id)
            {
                return BadRequest();
            }


            ///////////////////bakilacak

            bool rep = ExpenseItemHandler.updateexpenseItem(expenseItem, id);
            if (!rep)
            {
                if (ExpenseItemHandler.ExpenseItemExists(id))
                {
                    return NotFound();
                }

            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ExpenseItemsAPI
        [ResponseType(typeof(ExpenseItem))]
        public IHttpActionResult PostExpenseItem(ExpenseItem expenseItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ExpenseItemHandler.saveExpenseItem(expenseItem);
            
            return CreatedAtRoute("DefaultApi", new { id = expenseItem.Id }, expenseItem);
        }

        // DELETE: api/ExpenseItemsAPI/5
        [ResponseType(typeof(ExpenseItem))]
        public IHttpActionResult DeleteExpenseItem(int id)
        {
            ExpenseItem expenseItem = ExpenseItemHandler.deleteexpenseItembyid(id);
            
            if (expenseItem == null)
            {
                return NotFound();
            }
            
            return Ok(expenseItem);
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