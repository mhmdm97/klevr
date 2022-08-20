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
        public Guid OriginAccountId { get; set; }
        public Account OriginAccount { get; set; }
        public Guid TargetAccountId { get; set; }
        public Account TargetAccount { get; set; }
    }
}
