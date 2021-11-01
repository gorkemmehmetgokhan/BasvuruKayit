using BasvuruKayit.Models.ViewModel;
using BasvuruKayit.Repository;
using BasvuruKayit.Veritabani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BasvuruKayit.Controllers
{
    public class BasvuruController : Controller
    {
        BaseRepository<tbl_Bolum> bolum = new BaseRepository<tbl_Bolum>();
        BaseRepository<tbl_Ogrenci> ogrenci = new BaseRepository<tbl_Ogrenci>();
        BaseRepository<tbl_Birey> birey = new BaseRepository<tbl_Birey>();
        BaseRepository<tbl_Kullanici> kullanici = new BaseRepository<tbl_Kullanici>();
        BaseRepository<tbl_KullaniciRol> kullaniciRol = new BaseRepository<tbl_KullaniciRol>();     
        // GET: Basvuru

        [HttpGet]
        public ActionResult BasvuruEkle()
        {
            BasvuruViewModel bwm = new BasvuruViewModel();
            List<SelectListItem> BolumList = new List<SelectListItem>();
            List<SelectListItem> SinifList = new List<SelectListItem>();

            foreach (tbl_Bolum item in bolum.SelectAll())
            {
                BolumList.Add(new SelectListItem { Value = item.BolumID.ToString(), Text = item.BolumAd });
            }
          
            bwm.BolumList = BolumList;      
            bwm.Ogrenci = null;
            bwm.Birey = null;
            bwm.Kullanici = null;
            return View(bwm);
        }

        [HttpPost]
        public ActionResult BasvuruEkle(BasvuruViewModel model, HttpPostedFileBase foto, string sampleradio)
        {
            string PhotoName = "";
            if (foto.ContentLength > 0 & foto != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Images/" + PhotoName);
                foto.SaveAs(path);
            }
            if (sampleradio == "Erkek")
            {
                model.Birey.Cinsiyet = 1;
            }
            else
            {
                model.Birey.Cinsiyet = 2;
            }
            model.Ogrenci.OgrenciFoto = PhotoName;
            model.Ogrenci.BasvuruDurumID = 1;

            birey.Insert(model.Birey);
            birey.Save();

            model.Ogrenci.BireyID = model.Birey.BireyID;

            ogrenci.Insert(model.Ogrenci);
            ogrenci.Save();

            model.Kullanici.BireyID = model.Birey.BireyID;
            model.Kullanici.KullaniciAd = model.Ogrenci.OgrenciNo;
            string password = Membership.GeneratePassword(5, 1);
            model.Kullanici.Sifre = password;
            kullanici.Insert(model.Kullanici);
            kullanici.Save();

            model.KullaniciRol.KullaniciID = model.Kullanici.KullaniciID;
            model.KullaniciRol.RolID = 1;
            kullaniciRol.Insert(model.KullaniciRol);
            kullaniciRol.Save();

            TempData["KullaniciAd"] = model.Kullanici.KullaniciAd;
            TempData["Sifre"] = model.Kullanici.Sifre;
            TempData["Email"] = model.Ogrenci.Email;

            return RedirectToAction("Email");
        }

        public async Task<ActionResult> Email()
        {
            string KullaniciAd = TempData["KullaniciAd"].ToString();
            string Sifre = "Kullanıcı Şifreniz: " + TempData["Sifre"].ToString();
            string Email = TempData["Email"].ToString();

            if (ModelState.IsValid)
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(Email));
                message.From = new MailAddress("gorkemmehmet88@gmail.com");
                message.Subject = "Öğrenci Bilgi Sistemi Kullanıcı Şifresi";
                message.Body = string.Format(Sifre);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "gorkemmehmet88@gmail.com",
                        Password = "Gorkem.88"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

    }
}