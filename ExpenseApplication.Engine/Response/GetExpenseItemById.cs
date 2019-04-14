using ExpenseApplication.Engine.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApplication.Engine.Response
{
    public class GetExpenseItemById
    {
        public IEnumerable<ExpensesItemDomain> ExpensesItem { get; set; }
    }
}
