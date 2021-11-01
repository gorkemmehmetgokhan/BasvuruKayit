using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BasvuruKayit.Veritabani;
using System.Web.Mvc;

namespace BasvuruKayit.Models.ViewModel
{
    public class BasvuruViewModel
    {
        public tbl_Ogrenci Ogrenci { get; set; }
        public tbl_Birey Birey { get; set; }
        public tbl_Kullanici Kullanici { get; set; }
        public tbl_KullaniciRol KullaniciRol { get; set; }
        public IEnumerable<SelectListItem> BolumList { get; set; }     

    }
}