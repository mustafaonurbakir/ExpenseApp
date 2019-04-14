using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseApplication.Engine.Domain
{
    public class ExpenseDomain
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string StatusName { get; set; }
        public DateTime CreatedTime { get; set; }
        public float TotalAmount { get; set; }
    }
}
