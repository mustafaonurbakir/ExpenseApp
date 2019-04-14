using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseApplication.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ExpenseApplication.Engine.Domain;
using System.Data.SqlClient;

namespace ExpenseApplication.Engine.Handlers
{
    public class ExpenseHandler : System.Web.SessionState.IRequiresSessionState
    {

        public static bool ExpenseExists(int id)
        {
            ExpensesEntities db = new ExpensesEntities();
            return db.Expense.Count(e => e.Id == id) > 0;
        }
        public static IQueryable<Expense> getallrecords()
        {
            ExpensesEntities db = new ExpensesEntities();



            //var a = HttpContext.Current.Session["CurrentUserRole"];

            //if (role == "Manager")
            //{

            //}
            //else if(role == "Employee")
            //{
            //    return (from u in db.Expense where u.UserId == userid select u);

            //}
            //else if(role == "Accountant")
            //{

            //}

            return db.Expense;
        }

        public static Expense getexpensebyid(int id)
        {

            ExpensesEntities db = new ExpensesEntities();
            return db.Expense.Find(id);
        }

        public static void saveexpense(dynamic data)
        {

            ExpensesEntities db = new ExpensesEntities();
            Expense newExpense = new Expense();
            ExpenseHistory newExpenseHistory = new ExpenseHistory();



            newExpense.UserId = data.expensedata.CurrentUserId;
            newExpense.CreatedDate = DateTime.Now;
            newExpense.TotalAmount = data.expensedata.TotalAmount;

            db.Expense.Add(newExpense);
            db.SaveChanges();

            newExpenseHistory.ExpenseId = db.Expense.ToList().Last().Id;
            newExpenseHistory.ExpenseStatusId = 2;
            newExpenseHistory.Createdby_UserId = data.expensedata.CurrentUserId;

            db.ExpenseHistory.Add(newExpenseHistory);
            db.SaveChanges();

            foreach (var item in data.expenseitemdata)
            {
                Data.ExpenseItem newExpenseItem = new Data.ExpenseItem();
                newExpenseItem.ExpenseId = db.Expense.ToList().Last().Id;
                newExpenseItem.ExpenseType = item.ExpenseType;
                newExpenseItem.Description = item.Description;
                newExpenseItem.Amount = item.Amount;
                newExpenseItem.ExpenseDate = item.ExpenseDate;
                db.ExpenseItem.Add(newExpenseItem);
            }

            db.SaveChanges();
        }

        public static Expense deleteexpensebyid(int id)
        {
            ExpensesEntities db = new ExpensesEntities();
            Expense expense = db.Expense.Find(id);

            if (expense != null)
            {
                db.Expense.Remove(expense);
                db.SaveChanges();
            }

            return expense;
        }

        public static bool updateexpense(Expense expense, int id)
        {
            ExpensesEntities db = new ExpensesEntities();

            db.Entry(expense).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
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






        //Employee expense list
        public static IEnumerable<ExpenseDomain> GetExpenseByUserId(string CurrentUserId)
        {
            ExpensesEntities db = new ExpensesEntities();

            var replay = (from expense in db.Expense.Where(e => e.UserId == CurrentUserId)
                          select new ExpenseDomain
                          {
                              ID = expense.Id,
                              CreatedTime = expense.CreatedDate,
                              UserId = expense.UserId,
                              UserName = (from a in db.AspNetUsers.Where(e => e.Id == expense.UserId) select a.UserName).FirstOrDefault(),
                              StatusName = (from h in db.ExpenseHistory.Where(h => h.ExpenseId == expense.Id)
                                            from s in db.ExpenseStatus.Where(s => s.Id == h.ExpenseStatusId)
                                            select s.ExpenseStatusName).FirstOrDefault(),
                              TotalAmount = expense.TotalAmount
                         }).ToList();


            return replay;
        }

        //Manager and Accountant Expense list
        public static IEnumerable<ExpenseDomain> GetExpenseByStatus(string CurrentUserRole)
        {
            ExpensesEntities db = new ExpensesEntities();
            
            if (CurrentUserRole == "Manager") //manager
            {
                var entities = (from h in db.ExpenseHistory
                                join e in db.Expense on h.ExpenseId equals e.Id
                                where h.ExpenseStatusId == 2
                                select new ExpenseDomain
                                {
                                    ID = e.Id,
                                    CreatedTime = e.CreatedDate,
                                    UserId = e.UserId,
                                    UserName = (from a in db.AspNetUsers.Where(k => k.Id == e.UserId) select a.UserName).FirstOrDefault(),
                                    StatusName = (from h in db.ExpenseHistory.Where(h => h.ExpenseId == e.Id)
                                                  from s in db.ExpenseStatus.Where(s => s.Id == h.ExpenseStatusId)
                                                  select s.ExpenseStatusName).FirstOrDefault(),
                                    TotalAmount = e.TotalAmount
                                }).ToList();

                                
                return entities;
            }
            else //accountant
            {
                
                var entities = (from h in db.ExpenseHistory
                                join e in db.Expense on h.ExpenseId equals e.Id
                                where h.ExpenseStatusId == 3
                                select new ExpenseDomain
                                {
                                    ID = e.Id,
                                    CreatedTime = e.CreatedDate,
                                    UserId = e.UserId,
                                    UserName = (from a in db.AspNetUsers.Where(k => k.Id == e.UserId) select a.UserName).FirstOrDefault(),
                                    StatusName = (from h in db.ExpenseHistory.Where(h => h.ExpenseId == e.Id)
                                                  from s in db.ExpenseStatus.Where(s => s.Id == h.ExpenseStatusId)
                                                  select s.ExpenseStatusName).FirstOrDefault(),
                                    TotalAmount = e.TotalAmount
                                }).ToList();


                return entities;
            }
        }
    }
}
