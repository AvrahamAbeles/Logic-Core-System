namespace Logic_Core_Server.Data.Context;

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

        modelBuilder.Entity<CalculationLog>()
            .HasIndex(l => new { l.OperationKey, l.CreatedAt });

        modelBuilder.Entity<OperationType>().HasData(
                new OperationType {
                    Key = "add",
                    Name = "חיבור",
                    Formula = "arg1 + arg2",
                    Symbol = "+",
                    IsActive = true ,
                    ValidationRegex = null,
                    ValidationMessage = null
                },
                new OperationType { 
                     Key = "sub",
                     Name = "חיסור",
                     Formula = "arg1 - arg2",
                     Symbol = "-",
                     IsActive = true,
                     ValidationRegex = @"^-?\d+(\.\d+)?$",
                     ValidationMessage = "פעולה זו תומכת במספרים בלבד"
                },
                new OperationType {
                     Key = "mul",
                     Name = "כפל",
                     Formula = "arg1 * arg2",
                     Symbol = "*",
                     IsActive = true ,
                     ValidationRegex = @"^-?\d+(\.\d+)?$",
                     ValidationMessage = "פעולה זו תומכת במספרים בלבד"
                },
                new OperationType { 
                     Key = "div",
                     Name = "חילוק",
                     Formula = "arg1 / arg2",
                     Symbol = "/",
                     IsActive = true,
                     ValidationRegex = @"^-?\d+(\.\d+)?$",
                     ValidationMessage = "פעולה זו תומכת במספרים בלבד"
                },
                new OperationType
                {
                    Key = "vat",
                    Name = "חישוב כולל מעמ",
                    Formula = "arg1 * 1.18",
                    Symbol = "vat %",
                    IsActive = true,
                    ValidationRegex = @"^-?\d+(\.\d+)?$",
                    ValidationMessage = "פעולה זו תומכת במספרים בלבד"
                }
            );
        }
    }

