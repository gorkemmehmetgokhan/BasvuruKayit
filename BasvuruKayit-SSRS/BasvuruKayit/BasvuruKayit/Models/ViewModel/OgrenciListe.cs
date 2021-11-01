using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasvuruKayit.Models.ViewModel
{
    public class OgrenciListe
    {      
        public int OgrenciDersID { get; set; }
        public string DersAd { get; set; }
        public string OgrenciFoto { get; set; }
        public string AdSoyad { get; set; }
        public Int16 Sinif { get; set; }
    }
}