namespace _Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Feedback")]
    public partial class Feedback
    {
        public int FeedbackID { get; set; }

        [StringLength(50)]
        public string FeedbackName { get; set; }

        public int? Phone { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Status { get; set; }

        [StringLength(500)]
        public string Content { get; set; }
    }
}
