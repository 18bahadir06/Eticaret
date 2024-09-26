using Eticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eticaret.Controllers
{
    public class firmasettingController : Controller
    {
		// GET: firmasetting
		EticaretEntities db = new EticaretEntities();
		public ActionResult firmalagout()
		{
			if (Session["firmaAd"] != null)
			{
				Session.Remove("firmaMail");
				Session.Remove("firmaAd");
				Session.Remove("firmaId");
				return RedirectToAction("Index", "Home");
			}
			else 
			{ 
				return View(); 
			}
		}

		public ActionResult firmaMail() 
		{
			return View();
		}
		[HttpPost]
		public ActionResult firmaMail(Firma a) 
		{
			var c=Session["firmaId"].ToString();
			var sorgu = db.Firma.FirstOrDefault(x=>x.FirmaId.ToString()==c);
			sorgu.firmaMail = a.firmaMail;
			Session["firmaMail"] = a.firmaMail;
			db.SaveChanges();
			return RedirectToAction("firmasetting","Home");
		}
		public ActionResult firmaInfo()
		{
			return View();
		}
		[HttpPost]
		public ActionResult firmaInfo(Firma a)
		{
			var c = Session["firmaId"].ToString();
			var sorgu = db.Firma.FirstOrDefault(x => x.FirmaId.ToString() == c);
			sorgu.FirmaInfo = a.FirmaInfo;
			Session["firmaInfo"] = a.FirmaInfo;
			db.SaveChanges();
			return RedirectToAction("firmasetting", "Home");
		}
		public ActionResult firmaAdres()
		{
			return View();
		}
		[HttpPost]
		public ActionResult firmaAdres(Firma a)
		{
			var c = Session["firmaId"].ToString();
			var sorgu = db.Firma.FirstOrDefault(x => x.FirmaId.ToString() == c);
			sorgu.firmaadres = a.firmaadres;
			Session["firmaadres"] = a.firmaadres;
			db.SaveChanges();
			return RedirectToAction("firmasetting", "Home");
		}
		public ActionResult firmaSifre()
		{
			return View();
		}
		[HttpPost]
		public ActionResult firmaSifre(Firma a)
		{
			var c = Session["firmaId"].ToString();
			var sorgu = db.Firma.FirstOrDefault(x => x.FirmaId.ToString() == c);
			if (sorgu.firmasifre == a.firmasifre)
			{
				return RedirectToAction("newfirmaSifre", "Firmasetting");
			}
			ViewBag.Hata = "eski şifrenizi yanlış girdiniz.";
			return View();
		}


		public ActionResult newfirmaSifre()
		{
			return View();
		}
		[HttpPost]
		public ActionResult newfirmaSifre(Firma a)
		{
			var c = Session["firmaId"].ToString();
			var sorgu = db.Firma.FirstOrDefault(x => x.FirmaId.ToString() == c);
			sorgu.firmasifre = a.firmasifre;
			db.SaveChanges();
			return RedirectToAction("firmasetting", "Home");
		}

	}
}