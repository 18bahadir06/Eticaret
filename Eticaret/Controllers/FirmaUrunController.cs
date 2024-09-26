using Eticaret.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Eticaret.Controllers
{
    public class FirmaUrunController : Controller
    {
		EticaretEntities db = new EticaretEntities();
		public ActionResult Urunler()
        {
			string a=Session["firmaId"].ToString();
			var deger = db.Urun.Where(x => x.FirmaId.ToString() == a);

			List<Urun> list = deger.ToList();
			return View(list);
	    }
		public ActionResult UrunGetir(int id)
		{
			var ktgr = db.Urun.Find(id);
			var deger=db.Urun.FirstOrDefault(x=>x.UrunId.ToString() == ktgr.ToString());
			return View(deger);
		}
		public ActionResult KatagoriSec()
		{
			List<AltKatgori> liste=db.AltKatgori.ToList();
			return View(liste);
		}
		[HttpPost]
		public ActionResult KatagoriSec(AltKatgori a)
		{
			var sorgu=db.AltKatgori.FirstOrDefault(x=>x.AltKatagoriId==a.AltKatagoriId);
			TempData["veri"] = sorgu.AltKatagoriId;
			return RedirectToAction("MarkaSec");
		}
		public ActionResult MarkaSec()
		{
			List<Marka> liste=db.Marka.ToList();
			return View(liste);
		}
		[HttpPost]
		public ActionResult MarkaSec(Marka a)
		{
			var sorgu=db.Marka.FirstOrDefault(x=>x.MarkaId==a.MarkaId);
			var c=sorgu.MarkaId;
			TempData["veriler"]= c;
			return RedirectToAction("Urunekle");
		}
		public ActionResult KatagoriEkle()
		{
			return View();
		}
		[HttpPost]
		public ActionResult KatagoriEkle(AltKatgori a)
		{
			db.AltKatgori.Add(a);
			db.SaveChanges();
			return View();
		}

		public ActionResult Urunekle() 
		{
			List<AltKatgori> aklist= db.AltKatgori.ToList();
			var a= TempData["veri"];
			var b= TempData["veriler"];
			TempData["veri1"]=TempData["veri"];
			TempData["veri2"] = TempData["veriler"];
			return View(aklist);
		}
		[HttpPost]
		public ActionResult Urunekle(Urun a)
		{
			var marka = TempData["veri2"];
			var katagori = TempData["veri1"];
			var firma = Session["firmaId"];

			var sorgu1 = db.Marka.FirstOrDefault(x => x.MarkaId.ToString() == marka.ToString());
			var sorgu2 = db.AltKatgori.FirstOrDefault(x => x.AltKatagoriId.ToString() == katagori.ToString());
			var sorgu3= db.Firma.FirstOrDefault(x=>x.FirmaId.ToString() == firma.ToString());

			a.MarkaId = sorgu1.MarkaId;
			a.AltKatagoriId= sorgu2.AltKatagoriId;
			a.FirmaId = sorgu3.FirmaId;
			a.UrunLogo = "null şimdilik";
			db.Urun.Add(a);
			db.SaveChanges();
			return View();
		}

		public ActionResult kategoriekle()
		{
			return View();
		}
		[HttpPost]
		public ActionResult kategoriekle(AltKatgori a)
		{
			db.AltKatgori.Add(a);
			db.SaveChanges();
			return View();
		}
	}
}