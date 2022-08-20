using System;
using System.ComponentModel.DataAnnotations;
namespace klevr.Concrete.EF
{
    public partial class UserLimits
    {
        [Required]
        [Key]
        public string UserLimitsId { get; set; }
        public double TransactionAmountLimit { get; set; }
        public double DailyAmountLimit { get; set; }

        //relations
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
