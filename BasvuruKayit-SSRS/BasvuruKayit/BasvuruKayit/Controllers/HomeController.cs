using BasvuruKayit.Veritabani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BasvuruKayit.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Oturum()
        {
            //kullanıcının oturum acma islemi basarili ise
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = User.Identity.Name;
                return RedirectToAction("Default", "Profil");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Oturum(tbl_Kullanici k)
        {
            //validation control
            if (ModelState.IsValid)
            {
                using (DB_UniversiteEntities db = new DB_UniversiteEntities())
                {
                    var kullanici = (from kul in db.tbl_Kullanici
                                     join kulRol in db.tbl_KullaniciRol on kul.KullaniciID equals kulRol.KullaniciID
                                     join birey in db.tbl_Birey on kul.BireyID equals birey.BireyID
                                     join ogr in db.tbl_Ogrenci on birey.BireyID equals ogr.BireyID
                                     join rol in db.tbl_Rol on kulRol.RolID equals rol.RolID
                                     where kulRol.RolID == 1
                                     select new
                                     {
                                         KullaniciID = kul.KullaniciID,
                                         KullaniciAd = kul.KullaniciAd,
                                         Sifre = kul.Sifre,
                                         RolID = kulRol.RolID,
                                         Ad = birey.Ad,
                                         Soyad = birey.Soyad,
                                         RolAd = rol.RolAd,
                                         OgrenciID = ogr.OgrenciID,
                                         OgrenciNo = ogr.OgrenciNo,
                                         BolumID = ogr.BolumID,
                                         Sinif = ogr.Sinif,
                                         ogr.BasvuruDurumID
                                     });

                    var y = kullanici.FirstOrDefault(x => x.KullaniciAd == k.KullaniciAd && x.Sifre == k.Sifre);
                    if (y != null)
                    {
                        Session["RolID"] = y.RolID;
                        Session["AdSoyad"] = y.Ad + " " + y.Soyad;
                        Session["RolAd"] = y.RolAd;
                        Session["BolumID"] = y.BolumID;
                        Session["OgrenciID"] = y.OgrenciID;
                        Session["Sinif"] = y.Sinif;
                        Session["OgrenciNo"] = y.OgrenciNo;
                        Session["BasvuruDurumID"] = y.BasvuruDurumID;
                        FormsAuthentication.SetAuthCookie(y.KullaniciAd, true);
                        FormsAuthentication.SetAuthCookie(y.KullaniciID.ToString(), true);
                        return RedirectToAction("Default", "Profil");
                    }
                }
            }
            return View();
        }

        public ActionResult PersonelOturum()
        {
            //kullanıcının oturum acma islemi basarili ise
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = User.Identity.Name;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult PersonelOturum(tbl_Kullanici k)
        {
            //validation control
            if (ModelState.IsValid)
            {
                using (DB_UniversiteEntities db = new DB_UniversiteEntities())
                {
                    var kullanici = (from kul in db.tbl_Kullanici
                                     join kulRol in db.tbl_KullaniciRol on kul.KullaniciID equals kulRol.KullaniciID
                                     join birey in db.tbl_Birey on kul.BireyID equals birey.BireyID
                                     join rol in db.tbl_Rol on kulRol.RolID equals rol.RolID
                                     join prs in db.tbl_Personel on birey.BireyID equals prs.BireyID
                                     where kulRol.RolID == 2 || kulRol.RolID == 3
                                     select new
                                     {
                                         KullaniciID = kul.KullaniciID,
                                         KullaniciAd = kul.KullaniciAd,
                                         Sifre = kul.Sifre,
                                         RolID = kulRol.RolID,
                                         Ad = birey.Ad,
                                         Soyad = birey.Soyad,
                                         RolAd = rol.RolAd,
                                         PersonelID = prs.PersonelID
                                     });


                    var y = kullanici.FirstOrDefault(x => x.KullaniciAd == k.KullaniciAd && x.Sifre == k.Sifre);
                    if (y != null)
                    {
                        Session["PersonelID"] = y.PersonelID;
                        Session["RolID"] = y.RolID;
                        Session["AdSoyad"] = y.Ad + " " + y.Soyad;
                        Session["RolAd"] = y.RolAd;
                        FormsAuthentication.SetAuthCookie(y.KullaniciAd, true);
                        FormsAuthentication.SetAuthCookie(y.KullaniciID.ToString(), true);
                        return RedirectToAction("Default", "Profil");
                    }
                }
            }
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}