using Logic_Core_Server.Data.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace Logic_Core_Server.Data.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<OperationType> Operations { get; set; }
        public DbSet<CalculationLog> CalculationLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<OperationType>().HasData(
                new OperationType {
                    Key = "add",
                    Name = "חיבור",
                    Formula = "arg1 + arg2",
                    Symbol = "+",
                    IsActive = true },
                new OperationType { 
                     Key = "sub",
                     Name = "חיסור",
                     Formula = "arg1 - arg2",
                     Symbol = "-",
                     IsActive = true },
                new OperationType {
                     Key = "mul",
                     Name = "כפל",
                     Formula = "arg1 * arg2",
                     Symbol = "*",
                     IsActive = true },
                new OperationType { 
                     Key = "div",
                     Name = "חילוק",
                     Formula = "arg1 / arg2",
                     Symbol = "/",
                     IsActive = true }
            );
        }
    }
}
