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
    
    public partial class tbl_Bolum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Bolum()
        {
            this.tbl_Ogrenci = new HashSet<tbl_Ogrenci>();
        }
    
        public short BolumID { get; set; }
        public string BolumAd { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Ogrenci> tbl_Ogrenci { get; set; }
    }
}
