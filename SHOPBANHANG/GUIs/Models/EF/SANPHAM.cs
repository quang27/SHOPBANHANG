namespace GUIs.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            CHITIETHOADON = new HashSet<CHITIETHOADON>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string origin { get; set; }

        [StringLength(300)]
        public string img { get; set; }

        [StringLength(50)]
        public string color { get; set; }

        public int? quatity { get; set; }

        public int? price { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETHOADON> CHITIETHOADON { get; set; }
    }
}
