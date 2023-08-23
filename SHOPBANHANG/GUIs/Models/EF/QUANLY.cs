namespace GUIs.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QUANLY")]
    public partial class QUANLY
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(100)]
        public string password { get; set; }
    }
}
