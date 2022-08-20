using System;
using System.ComponentModel.DataAnnotations;
namespace klevr.Concrete.EF
{
    public partial class UserLimits
    {
        [Required]
        [Key]
        public Guid UserLimitsId { get; set; }
        public double TransactionAmountLimit { get; set; }
        public double DailyAmountLimit { get; set; }

        //relations
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
