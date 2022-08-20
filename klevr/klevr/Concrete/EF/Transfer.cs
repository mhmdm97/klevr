using System;
using System.ComponentModel.DataAnnotations;
namespace klevr.Concrete.EF
{
    public partial class Transfer
    {
        [Required]
        [Key]
        public string TransferId { get; set; }
        public double TransferAmount { get; set; }
        public string TransferCurrency { get; set; }

        //relations
        public string OriginUserId { get; set; }
        public User OriginUser { get; set; }
        public string TargetUserId { get; set; }
        public User TargetUser { get; set; }
    }
}
