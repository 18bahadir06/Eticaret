using Eticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eticaret.Controllers
{
	public class SettingController : Controller
	{
		EticaretEntities db = new EticaretEntities();
		// GET: setting

		public ActionResult Lagout()
		{
			if (Session["KullaniciAd"] != null)
			{
				Session.Remove("KullaniciAd");
				Session.Remove("KullaniciSoyad");
				Session.Remove("KullaniciMail");
				return RedirectToAction("Index", "Home");
			}
			else { return View(); }
		}

		public ActionResult settingad()
		{
			return View();

		}

		[HttpPost]
		public ActionResult settingad(Kullanici a)
		{
			string b = Session["KullaniciId"].ToString();
			var degerler = db.Kullanici.FirstOrDefault(x => x.KullaniciId.ToString() == b);

			degerler.KullaniciAd = a.KullaniciAd;
			degerler.KullaniciSoyad = a.KullaniciSoyad;
			db.SaveChanges();
			Session["KullaniciAd"] = degerler.KullaniciAd;
			Session["KullaniciSoyad"] = degerler.KullaniciSoyad;
			return RedirectToAction("setting", "Home");
		}

		public ActionResult settingmail()
		{
			return View();
		}
		[HttpPost]
		public ActionResult settingmail(Kullanici a)
		{
			string b = Session["KullaniciId"].ToString();

			var degerler = db.Kullanici.FirstOrDefault(x => x.KullaniciId.ToString() == b);

			degerler.KullaniciMail = a.KullaniciMail;
			db.SaveChanges();
			Session["KullaniciMail"] = degerler.KullaniciMail;
			return RedirectToAction("setting", "Home");
		}

		public ActionResult settingsoru()
		{
			var deger = db.gsorusu;
			List<gsorusu> list = deger.ToList();
			return View(list);
		}
		[HttpPost]
		public ActionResult settingsoru(Kullanici a)
		{
			string b = Session["KullaniciId"].ToString();
			var deger = db.Kullanici.FirstOrDefault(x => x.KullaniciId.ToString() == b);

			deger.gsorusuid = a.gsorusuid;
			deger.gcevap = a.gcevap;
			db.SaveChanges();

			string d = a.gsorusuid.ToString();
			var degerler = db.gsorusu.FirstOrDefault(x => x.gsorusuid.ToString() == d);

			Session["gsouru"] = degerler.gsouru.ToString();
			Session["gsorusu"] = a.gcevap;
			return RedirectToAction("setting", "Home");
		}
		public ActionResult sifredegistir()
		{
			var deger = db.gsorusu;
			List<gsorusu> list = deger.ToList();
			return View(list);
		}
		[HttpPost]
		public ActionResult sifredegistir(Kullanici a)
		{
			string b = Session["KullaniciId"].ToString();
			var deger = db.Kullanici.FirstOrDefault(x => x.KullaniciId.ToString() == b);

			var deger2 = db.Kullanici.FirstOrDefault(x => x.KullaniciSifre.ToString() == a.KullaniciSifre);
			var deger3 = db.Kullanici.FirstOrDefault(x => x.gsorusuid.ToString() == a.gsorusuid.ToString());

			if (deger2 != null && deger3 != null)
			{
				return RedirectToAction("newsifre", "Home");
			}
			else
			{
				ViewBag.hatasifre = "Bilgilerinizi yanlış girdiniz";
				var gsorusu = db.gsorusu;
				List<gsorusu> list = gsorusu.ToList();
				return View(list);
			}

		}




		public ActionResult siparisremove()
		{
			return View();
		}
		
		public ActionResult hesapdelete()
		{
			return View();
		}
		[HttpPost]
		public ActionResult hesapdelete(Kullanici c)
		{

			string b = Session["KullaniciId"].ToString();
			var deger = db.Kullanici.FirstOrDefault(x => x.KullaniciId.ToString() == b);
			deger = db.Kullanici.Remove(deger);
			db.SaveChanges();
			Session["KullaniciId"]    = null;
			Session["KullaniciMail"]  = null;
			Session["KullaniciAd"]    = null;
			Session["KullaniciSoyad"] = null;
			Session["gsouru"]         = null;
			Session["gsorusu"] = null;
			return RedirectToAction("Giris","Home");
		}	
	}
}
