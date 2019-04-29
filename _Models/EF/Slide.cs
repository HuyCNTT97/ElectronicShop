namespace _Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slide")]
    public partial class Slide
    {
        public int SlideID { get; set; }

        [Required]
        [StringLength(250)]
        public string Image { get; set; }

        public int? DisplayOrder { get; set; }

        [Required]
        [StringLength(50)]
        public string Link { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateBy { get; set; }

        [StringLength(50)]
        public string ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }

        public virtual UserAccount UserAccount { get; set; }
    }
}
