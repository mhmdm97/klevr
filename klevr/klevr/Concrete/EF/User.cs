using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace klevr.Concrete.EF
{
    public partial class User
    {
        [Required]
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DOB { get; set; }
        public int Gender { get; set; }

        //relations
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }

        public List<Account> Accounts { get; set; }

        public List<Transfer> OutgoingTransfers { get; set; }
        public List<Transfer> IncomingTransfers { get; set; }

    }
}
