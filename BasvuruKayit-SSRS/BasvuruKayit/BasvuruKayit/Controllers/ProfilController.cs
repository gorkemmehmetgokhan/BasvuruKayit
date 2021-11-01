using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BasvuruKayit.Veritabani;
using BasvuruKayit.Repository;
using System.Data.SqlClient;
using BasvuruKayit.Models.ViewModel;
using Microsoft.SqlServer.Server;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using BasvuruKayit.Models;

namespace BasvuruKayit.Controllers
{
    public class ProfilController : Controller
    {
        DB_UniversiteEntities db = new DB_UniversiteEntities();
        BaseRepository<tbl_Ogrenci> ogrenci = new BaseRepository<tbl_Ogrenci>();
        BaseRepository<tbl_OgrenciDers> ogrenciDers = new BaseRepository<tbl_OgrenciDers>();
        BaseRepository<tbl_Sinav> sinav = new BaseRepository<tbl_Sinav>();
        public ActionResult Default()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult BasvuruListele()
        {
            var basvuru = (from ogr in db.tbl_Ogrenci
                           join birey in db.tbl_Birey on ogr.BireyID equals birey.BireyID
                           join durum in db.tbl_BasvuruDurum on ogr.BasvuruDurumID equals durum.BasvuruDurumID
                           join bolum in db.tbl_Bolum on ogr.BolumID equals bolum.BolumID
                           orderby bolum.BolumAd, ogr.Sinif, birey.Ad, birey.Soyad
                           select new
                           {
                               OgrenciID = ogr.OgrenciID,
                               BasvuruDurumAd = durum.BasvuruDurumAd,
                               BasvuruDurumID = durum.BasvuruDurumID,
                               OgrenciFoto = ogr.OgrenciFoto,
                               OgrenciNo = ogr.OgrenciNo,
                               BolumID = bolum.BolumID,
                               Sinif = ogr.Sinif,
                               BolumAd = bolum.BolumAd,
                               Ad = birey.Ad,
                               Soyad = birey.Soyad,
                               Cinsiyet = birey.Cinsiyet,
                               DogumTarihi = birey.DoğumTarihi,
                               DogumYeri = birey.DoğumYeri,
                               KimlikNo = birey.KimlikNo
                           }).ToList();

            return Json(basvuru, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Onayla(int ogrenciID)
        {
            tbl_Ogrenci guncelle = ogrenci.GetByOgrenciID(ogrenciID);
            guncelle.BasvuruDurumID = 2;
            ogrenci.Update(guncelle);
            ogrenci.Save();
            return Json(ogrenci, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult Reddet(int ogrenciID)
        {
            tbl_Ogrenci guncelle = ogrenci.GetByOgrenciID(ogrenciID);
            guncelle.BasvuruDurumID = 3;
            ogrenci.Update(guncelle);
            ogrenci.Save();
            return Json(ogrenci, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DersKayit()
        {
            return View();
        }

        [HttpGet]
        public JsonResult DersListele()
        {
            byte sinif = Convert.ToByte(Session["Sinif"]);
            Int16 bolumID = Convert.ToInt16(Session["BolumID"]);
            int ogrenciID = Convert.ToInt32(Session["OgrenciID"]);

            var p1 = new SqlParameter("@OgrenciID", ogrenciID);
            var p2 = new SqlParameter("@Sinif", sinif);
            var p3 = new SqlParameter("@BolumID", bolumID);

            var dersler = db.Database.SqlQuery<sproc_DersKayit_ListByOgrenciID_Result>("exec sproc_DersKayit_ListByOgrenciID @OgrenciID,@Sinif,@BolumID", p1, p2, p3).ToList();
            return Json(dersler, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DersKaydet(int dersID)
        {
            tbl_OgrenciDers ogrDers = new tbl_OgrenciDers();
            ogrDers.OgrenciID = Convert.ToInt32(Session["OgrenciID"].ToString());
            ogrDers.DersID = dersID;
            ogrenciDers.Insert(ogrDers);
            ogrenciDers.Save();
            return Json(ogrenciDers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DersSil(int OgrenciDersID)
        {
            tbl_OgrenciDers sil = ogrenciDers.GetByOgrenciDersID(OgrenciDersID);
            ogrenciDers.Delete(sil);
            ogrenciDers.Save();
            return Json(ogrenciDers, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SinavListele()
        {
            int PersonelID = Convert.ToInt32(Session["PersonelID"].ToString());
            var dersListesi = (from ders in db.tbl_Ders
                               join prsDers in db.tbl_PersonelDers on ders.DersID equals prsDers.DersID
                               where prsDers.PersonelID == PersonelID
                               select new DersListe
                               {
                                   DersID = prsDers.DersID,
                                   DersAd = ders.DersAd
                               }).ToList();
            TempData["DersListesi"] = dersListesi;
            ViewBag.DersListesi = TempData["DersListesi"];
            return View();
        }

        [HttpPost]
        public JsonResult OgrenciListeleme(int DersID)
        {
            var p = new SqlParameter("@DersID", DersID);

            var dersler = db.Database.SqlQuery<sproc_SinavNot_ListByDersID_Result>("exec sproc_SinavNot_ListByDersID @DersID", p).ToList();
            TempData["Liste"] = dersler;
            ViewBag.Liste = TempData["Liste"];
            return Json(dersler, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult NotVer(int OgrenciDersID_Secilen, double vize, double final)
        {
            tbl_Sinav snv = new tbl_Sinav();
            snv.OgrenciDersID = OgrenciDersID_Secilen;
            snv.SinavTipID = 1;
            snv.SinavNot = vize;
            sinav.Insert(snv);
            sinav.Save();

            snv.OgrenciDersID = OgrenciDersID_Secilen;
            snv.SinavTipID = 2;
            snv.SinavNot = final;
            sinav.Insert(snv);
            sinav.Save();

            return Json(sinav, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OgrenciNotGuncelle(int OgrenciDersID_Secilen, float vize, float final)
        {
            tbl_Sinav guncelleVize = sinav.GetByOgrenciDersIDSinavTipID(OgrenciDersID_Secilen, 1);
            tbl_Sinav guncelleFinal = sinav.GetByOgrenciDersIDSinavTipID(OgrenciDersID_Secilen, 2);

            guncelleVize.SinavNot = vize;
            guncelleFinal.SinavNot = final;
            sinav.Update(guncelleVize);
            sinav.Update(guncelleFinal);
            sinav.Save();
            return Json(sinav, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult OgrenciNotSil(int OgrenciDersID_Secilen)
        {
            tbl_Sinav guncelleVize = sinav.GetByOgrenciDersIDSinavTipID(OgrenciDersID_Secilen, 1);
            tbl_Sinav guncelleFinal = sinav.GetByOgrenciDersIDSinavTipID(OgrenciDersID_Secilen, 2);

            sinav.Delete(guncelleVize);
            sinav.Delete(guncelleFinal);
            sinav.Save();
            return Json(sinav, JsonRequestBehavior.AllowGet);
        }


        public void GetStudentReport()
        {         
            ReportParams obj = new ReportParams();
            var data = GetStudentInfo();
            obj.DataSource = data.Tables[0];
            obj.RptFileName = "Transkript.rdlc";         
            obj.DataSetName = "dsOgrenci";
            
            this.HttpContext.Session["ReportParam"] = obj;
        }

        public DataSet GetStudentInfo()
        {
            int id = Convert.ToInt32(Session["OgrenciID"]);

            string constr = ConfigurationManager.ConnectionStrings["DB_UniversiteConnectionString"].ConnectionString;
            DataSet ds = new DataSet();
           
            string sql = String.Format(@"SELECT tbl_Ogrenci.OgrenciNo, tbl_Ogrenci.OgrenciFoto,
                                                tbl_Ogrenci.Sinif,tbl_Ogrenci.Email,
                                                tbl_Birey_Ogrenci.Ad + ' ' + tbl_Birey_Ogrenci.Soyad AS OgrAdSoyad,
                                                tbl_Birey_Ogrenci.DoğumTarihi,tbl_Birey_Ogrenci.DoğumYeri,
                                                tbl_Ders.DersAd,tbl_Birey_Personel.Ad + ' ' + tbl_Birey_Personel.Soyad AS PrsAdSoyad,
                                                tbl_Sinav.SinavNot,tbl_SinavTip.SinavTipAd
                            FROM tbl_Ogrenci
                            LEFT JOIN tbl_Bolum ON tbl_Bolum.BolumID = tbl_Ogrenci.BolumID
                            LEFT JOIN tbl_OgrenciDers ON tbl_OgrenciDers.OgrenciID = tbl_Ogrenci.OgrenciID
                            LEFT JOIN tbl_Ders ON tbl_Ders.DersID = tbl_OgrenciDers.DersID
                            LEFT JOIN tbl_Sinav ON tbl_Sinav.OgrenciDersID = tbl_OgrenciDers.OgrenciDersID
                            LEFT JOIN tbl_SinavTip ON tbl_SinavTip.SinavTipID = tbl_Sinav.SinavTipID
                            LEFT JOIN tbl_PersonelDers ON tbl_PersonelDers.DersID = tbl_Ders.DersID
                            LEFT JOIN tbl_Personel ON tbl_Personel.PersonelID = tbl_PersonelDers.PersonelID
                            LEFT JOIN tbl_Birey AS tbl_Birey_Personel ON tbl_Birey_Personel.BireyID = tbl_Personel.BireyID
                            LEFT JOIN tbl_Birey AS tbl_Birey_Ogrenci ON tbl_Birey_Ogrenci.BireyID = tbl_Ogrenci.BireyID
                            WHERE tbl_Ogrenci.OgrenciID= " + id + "");
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            return ds;
        }
    }
}