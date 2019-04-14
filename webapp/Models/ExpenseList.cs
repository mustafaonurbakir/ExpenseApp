using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SmartAdminMvc.Models
{
    //[Table("ExpenseStatus")]
    public class ExpenseStatus
    {
        //[Key]
        public int Id { get; set; }
        public string ExpenseStatusName { get; set; }
    }

    //[Table("ExpenseItem")]
    public partial class ExpenseItem
    {
        //[Key]
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public string ExpenseType { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
    }

    //[Table("ExpenseHistory")]
    public partial class ExpenseHistory
    {
        //[Key]
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ExpenseStatusId { get; set; }
        public int Createdby_UserId { get; set; }
        public string RejectDefinition { get; set; }
    }

    //[Table("Expense")]
    public class Expense
    {
        //[Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public float TotalAmount { get; set; }
    }
    public class ExpenseList : DbContext
    {
        public ExpenseList()
        { }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseHistory> ExpenseHistory { get; set; }
        public DbSet<ExpenseItem> ExpenseItemm { get; set; }
        public DbSet<ExpenseStatus> ExpenseStatuss { get; set; }

    }
}