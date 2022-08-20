using System;
using Microsoft.EntityFrameworkCore;
namespace klevr.Concrete.EF
{
    public class DBContext : DbContext
    {
        public DBContext()
        {
        }


        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLimits> UserLimits { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Transfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>()
                .HasOne(p => p.OriginUser)
                .WithMany(b => b.OutgoingTransfers)
                .HasForeignKey(p => p.OriginUserId);

            modelBuilder.Entity<Transfer>()
                .HasOne(p => p.TargetUser)
                .WithMany(b => b.IncomingTransfers)
                .HasForeignKey(p => p.TargetUserId);

            //seed data for testing purposes
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = "0",
                FirstName = "Mohamad",
                MiddleName = "Bassam",
                LastName = "Mortada",
                DOB = new DateTime(1997, 2, 9),
                Gender = 0
            });

            modelBuilder.Entity<UserLimits>().HasData(new UserLimits
            {
                UserLimitsId = "0",
                TransactionAmountLimit = 1000,
                DailyAmountLimit = 200,
                UserId = "0"
            });
        }
    }
}

