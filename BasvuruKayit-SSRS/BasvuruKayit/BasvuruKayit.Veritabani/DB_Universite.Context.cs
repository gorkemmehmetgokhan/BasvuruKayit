﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB_UniversiteEntities : DbContext
    {
        public DB_UniversiteEntities()
            : base("name=DB_UniversiteEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tbl_BasvuruDurum> tbl_BasvuruDurum { get; set; }
        public virtual DbSet<tbl_Birey> tbl_Birey { get; set; }
        public virtual DbSet<tbl_Bolum> tbl_Bolum { get; set; }
        public virtual DbSet<tbl_Ders> tbl_Ders { get; set; }
        public virtual DbSet<tbl_Kullanici> tbl_Kullanici { get; set; }
        public virtual DbSet<tbl_KullaniciRol> tbl_KullaniciRol { get; set; }
        public virtual DbSet<tbl_Ogrenci> tbl_Ogrenci { get; set; }
        public virtual DbSet<tbl_OgrenciDers> tbl_OgrenciDers { get; set; }
        public virtual DbSet<tbl_Personel> tbl_Personel { get; set; }
        public virtual DbSet<tbl_PersonelDers> tbl_PersonelDers { get; set; }
        public virtual DbSet<tbl_Rol> tbl_Rol { get; set; }
        public virtual DbSet<tbl_Sinav> tbl_Sinav { get; set; }
        public virtual DbSet<tbl_SinavTip> tbl_SinavTip { get; set; }
    
        public virtual ObjectResult<sproc_Birey_ListByOgrenciID_Result> sproc_Birey_ListByOgrenciID(Nullable<int> ogrenciID)
        {
            var ogrenciIDParameter = ogrenciID.HasValue ?
                new ObjectParameter("OgrenciID", ogrenciID) :
                new ObjectParameter("OgrenciID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sproc_Birey_ListByOgrenciID_Result>("sproc_Birey_ListByOgrenciID", ogrenciIDParameter);
        }
    
        public virtual ObjectResult<sproc_DersKayit_ListByOgrenciID_Result> sproc_DersKayit_ListByOgrenciID(Nullable<int> ogrenciID, Nullable<byte> sinif, Nullable<short> bolumID)
        {
            var ogrenciIDParameter = ogrenciID.HasValue ?
                new ObjectParameter("OgrenciID", ogrenciID) :
                new ObjectParameter("OgrenciID", typeof(int));
    
            var sinifParameter = sinif.HasValue ?
                new ObjectParameter("Sinif", sinif) :
                new ObjectParameter("Sinif", typeof(byte));
    
            var bolumIDParameter = bolumID.HasValue ?
                new ObjectParameter("BolumID", bolumID) :
                new ObjectParameter("BolumID", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sproc_DersKayit_ListByOgrenciID_Result>("sproc_DersKayit_ListByOgrenciID", ogrenciIDParameter, sinifParameter, bolumIDParameter);
        }
    
        public virtual ObjectResult<sproc_OgrenciDers_ListByOgrenciID_Result> sproc_OgrenciDers_ListByOgrenciID(Nullable<int> ogrenciID)
        {
            var ogrenciIDParameter = ogrenciID.HasValue ?
                new ObjectParameter("OgrenciID", ogrenciID) :
                new ObjectParameter("OgrenciID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sproc_OgrenciDers_ListByOgrenciID_Result>("sproc_OgrenciDers_ListByOgrenciID", ogrenciIDParameter);
        }
    
        public virtual ObjectResult<sproc_SinavNot_ListByDersID_Result> sproc_SinavNot_ListByDersID(Nullable<short> dersID)
        {
            var dersIDParameter = dersID.HasValue ?
                new ObjectParameter("DersID", dersID) :
                new ObjectParameter("DersID", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sproc_SinavNot_ListByDersID_Result>("sproc_SinavNot_ListByDersID", dersIDParameter);
        }
    }
}