namespace _Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerAccount")]
    public partial class CustomerAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string PassWord { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string GioiTinh { get; set; }

        public DateTime? BirthDay { get; set; }

        public int TelephoneNumber { get; set; }

        [StringLength(50)]
        public string ConfirmPassWord { get; set; }
    }
}
