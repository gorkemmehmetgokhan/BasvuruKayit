//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BasvuruKayit.Veritabani
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Ders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Ders()
        {
            this.tbl_OgrenciDers = new HashSet<tbl_OgrenciDers>();
            this.tbl_PersonelDers = new HashSet<tbl_PersonelDers>();
        }
    
        public int DersID { get; set; }
        public string DersAd { get; set; }
        public short BolumID { get; set; }
        public byte Sinif { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_OgrenciDers> tbl_OgrenciDers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_PersonelDers> tbl_PersonelDers { get; set; }
    }
}
