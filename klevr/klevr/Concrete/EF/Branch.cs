using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace klevr.Concrete.EF
{
    public partial class Branch
    {
        [Required]
        [Key]
        public Guid BranchId { get; set; }
        public string Name { get; set; }
        //relations
        public List<User> Users { get; set; }
    }
}
