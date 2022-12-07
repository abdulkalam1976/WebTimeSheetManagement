namespace WebTimeSheetManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Enumeration")]
    public partial class Enumeration
    {
        [Key]
        public int Enumid { get; set; }

        [Required]
        [StringLength(3)]
        public string Enumcode { get; set; }

        [Required]
        [StringLength(50)]
        public string Enumdesc { get; set; }

        public int Enumtype { get; set; }

        public int Sortorder { get; set; }
    }
}
