using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApplication.Engine.Domain
{
    public class ExpensesItemDomain
    {
        
        public int ID { get; set; }
        public int ExpenseId { get; set; }
        public string ExpenseType { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public float Amount { get; set; }
        
    }
}
