using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace klevr.Concrete.EF
{
    public partial class Account
    {
        [Required]
        [Key]
        public Guid AccountNumber { get; set; }
        public string AccountCurrency { get; set; }
        public int AccountType { get; set; }
        public int AccountStatus { get; set; }

        //relations
        public Guid UserId { get; set; }
        public User User { get; set; }

        public List<Transfer> OutgoingTransfers { get; set; }
        public List<Transfer> IncomingTransfers { get; set; }
    }
}
