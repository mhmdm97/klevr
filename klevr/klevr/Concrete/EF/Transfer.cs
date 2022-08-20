using System;
using System.ComponentModel.DataAnnotations;
namespace klevr.Concrete.EF
{
    public partial class Transfer
    {
        [Required]
        [Key]
        public Guid TransferId { get; set; }
        public double TransferAmount { get; set; }
        public DateTime TransferDate { get; set; }

        //relations
        public Guid OriginUserId { get; set; }
        public User OriginUser { get; set; }
        public Guid TargetUserId { get; set; }
        public User TargetUser { get; set; }
    }
}
