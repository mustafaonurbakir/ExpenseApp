using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseApplication.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ExpenseApplication.Engine.Domain;

namespace ExpenseApplication.Engine.Handlers
{
    public class ExpenseItemHandler
    {
        public static bool ExpenseItemExists(int id)
        {
            ExpensesEntities db = new ExpensesEntities();
            return db.ExpenseItem.Count(e => e.Id == id) > 0;
        }


        //kullanımda
        public static IEnumerable<ExpensesItemDomain> getallrecords(string Id)
        {
            ExpensesEntities db = new ExpensesEntities();

            int newId = Convert.ToInt32(Id);

            var replay = (from expenseItem in db.ExpenseItem.Where(e => e.ExpenseId == newId)
                          select new ExpensesItemDomain
                          {
                              ID = expenseItem.Id,
                              ExpenseId = expenseItem.ExpenseId,
                              ExpenseType = expenseItem.ExpenseType,
                              Description = expenseItem.Description,
                              //ExpenseDate = expenseItem.ExpenseDate,
                              Amount = expenseItem.Amount
                        }).ToList();


            return replay;

            //ExpensesEntities db = new ExpensesEntities();
            //return db.ExpenseItem;
        }

        public static bool ApproveForm(string Id)
        {
            ExpensesEntities db = new ExpensesEntities();

            int newId = Convert.ToInt32(Id);

            (from ExpenseHistory in db.ExpenseHistory.Where(e => e.ExpenseId == newId) select ExpenseHistory).SingleOrDefault().ExpenseStatusId = 3;
            db.SaveChanges();

            return true;
        }

        public static bool PayForm(string Id)
        {
            ExpensesEntities db = new ExpensesEntities();

            int newId = Convert.ToInt32(Id);

            (from ExpenseHistory in db.ExpenseHistory.Where(e => e.ExpenseId == newId) select ExpenseHistory).SingleOrDefault().ExpenseStatusId = 4;
            db.SaveChanges();

            return true;
        }

        public static bool ConfirmReject(dynamic data)
        {
            ExpensesEntities db = new ExpensesEntities();

            int newId = Convert.ToInt32(data.DetailId);
            string RejectDefinition = data.RejectDefiniton.ToString();
            (from ExpenseHistory in db.ExpenseHistory.Where(e => e.ExpenseId == newId) select ExpenseHistory).SingleOrDefault().ExpenseStatusId = 5;
            (from ExpenseHistory in db.ExpenseHistory.Where(e => e.ExpenseId == newId) select ExpenseHistory).SingleOrDefault().RejectDefinition = RejectDefinition;
            db.SaveChanges();

            return true;
        }

        //






        public static ExpenseItem getExpenseItembyid(int id)
        {
            ExpensesEntities db = new ExpensesEntities();
            return db.ExpenseItem.Find(id);
        }

        public static void saveExpenseItem(Data.ExpenseItem expenseItem)
        {
            ExpensesEntities db = new ExpensesEntities();

            expenseItem.ExpenseId = db.Expense.ToList().Last().Id;
            db.ExpenseItem.Add(expenseItem);
            
            db.SaveChanges();
        }

        public static ExpenseItem deleteexpenseItembyid(int id)
        {
            ExpensesEntities db = new ExpensesEntities();
            Data.ExpenseItem expenseItem = db.ExpenseItem.Find(id);

            if (expenseItem != null)
            {
                db.ExpenseItem.Remove(expenseItem);
                db.SaveChanges();
            }

            return expenseItem;
        }

        public static bool updateexpenseItem(Data.ExpenseItem expense, int id)
        {
            ExpensesEntities db = new ExpensesEntities();

            db.Entry(expense).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseItemExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }


    }
}
