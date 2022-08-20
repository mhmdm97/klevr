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
                .HasOne(p => p.OriginAccount)
                .WithMany(b => b.OutgoingTransfers)
                .HasForeignKey(p => p.OriginAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transfer>()
                .HasOne(p => p.TargetAccount)
                .WithMany(b => b.IncomingTransfers)
                .HasForeignKey(p => p.TargetAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            //seed data for testing purposes
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();
            var branch = Guid.NewGuid();
            var account = Guid.NewGuid();
            modelBuilder.Entity<Branch>().HasData(new Branch
            {
                BranchId = branch,
                Name = "Test branch"
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = user1,
                FirstName = "Mohamad",
                MiddleName = "Bassam",
                LastName = "Mortada",
                DOB = new DateTime(1997, 2, 9),
                Gender = 0,
                BranchId = branch
            });
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = user2,
                FirstName = "Nour",
                MiddleName = "Bassam",
                LastName = "Mortada",
                DOB = new DateTime(1998, 11, 28),
                Gender = 1,
                BranchId = branch
            });

            modelBuilder.Entity<Account>().HasData(new Account
            {
                AccountNumber = account,
                AccountCurrency = "USD",
                AccountType = 1,
                AccountStatus = 1,
                UserId = user1
            });

            modelBuilder.Entity<UserLimits>().HasData(new UserLimits
            {
                UserLimitsId = Guid.NewGuid(),
                TransactionAmountLimit = 500,
                DailyAmountLimit = 1000,
                UserId = user1
            });

            modelBuilder.Entity<UserLimits>().HasData(new UserLimits
            {
                UserLimitsId = Guid.NewGuid(),
                TransactionAmountLimit = 200,
                DailyAmountLimit = 1000,
                UserId = user2
            });

            modelBuilder.Entity<Transfer>().HasData(new Transfer
            {
                TransferId = Guid.NewGuid(),
                TransferAmount = 300,
                TransferDate = DateTime.Now,
                OriginAccountId = account,
                TargetAccountId = account
            });
            modelBuilder.Entity<Transfer>().HasData(new Transfer
            {
                TransferId = Guid.NewGuid(),
                TransferAmount = 400,
                TransferDate = DateTime.Now,
                OriginAccountId = account,
                TargetAccountId = account
            });
        }
    }
}

