using Eticaret.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Collections;
using System.Linq;
using Microsoft.Ajax.Utilities;
using System.Web.Security;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Resources;
using System.Drawing;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Razor.Generator;
using System.Drawing;

namespace Eticaret.Controllers
{
	public class HomeController : Controller
	{
		EticaretEntities db=new EticaretEntities();
		public ActionResult Index()
		{
			var deger = db.Urun.ToList();

			List<Urun> list = deger.ToList();
			return View(list);
		}
		


		public ActionResult Listele() 
		{
			var deger = db.AltKatgori.Where(x => x.KatagoriId == 1);

			List<AltKatgori> list=deger.ToList();
			return View(list); 
		}
		
		public ActionResult Listelemasaustu() 
		{
			var deger = db.AltKatgori.Where(x => x.KatagoriId == 2);

			List<AltKatgori> list = deger.ToList();
			return View(list);
		}
		
		public ActionResult Listeledonanım()
		{

			var deger = db.AltKatgori.Where(x => x.KatagoriId == 3);

			List<AltKatgori> list = deger.ToList();
			return View(list);
		}



		public ActionResult Siparis()
		{
			if (Session["KullaniciAd"] == null)
			{
				return RedirectToAction("Giris");
			}
			else
			{
				return View();
			}
			
		}





		public ActionResult Giris()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Giris(Kullanici a)
		{
			var bilgiler = db.Kullanici.FirstOrDefault(x => x.KullaniciMail == a.KullaniciMail && x.KullaniciSifre == a.KullaniciSifre);
			var firmabilgi = db.Firma.FirstOrDefault(x => x.firmaMail == a.KullaniciMail && x.firmasifre == a.KullaniciSifre);

			if (bilgiler != null)
			{
				var bilgiler2 = db.gsorusu.FirstOrDefault(x => x.gsorusuid == bilgiler.gsorusuid);
				FormsAuthentication.SetAuthCookie(bilgiler.KullaniciMail, false);
				Session["KullaniciId"] = bilgiler.KullaniciId;
				Session["KullaniciMail"] = bilgiler.KullaniciMail.ToString();
				Session["KullaniciAd"] = bilgiler.KullaniciAd.ToString();
				Session["KullaniciSoyad"] = bilgiler.KullaniciSoyad.ToString();
				Session["gsouru"]=bilgiler2.gsouru.ToString();
				Session["gsorusu"]=bilgiler.gcevap.ToString();
				return RedirectToAction("Index");
			}
			else if (firmabilgi != null)
			{
				var bilgiler2=db.Firma.FirstOrDefault(x=>x.FirmaId == firmabilgi.FirmaId);
				FormsAuthentication.SetAuthCookie(firmabilgi.firmaMail, false);
				
				Session["firmaId"]= bilgiler2.FirmaId;
				Session["firmaAd"] = bilgiler2.FirmaAd;
				Session["firmaMail"] = bilgiler2.firmaMail;
				Session["FirmaInfo"] = bilgiler2.FirmaInfo;
				Session["firmaadres"] = bilgiler2.firmaadres;
				
				return RedirectToAction("Index");
			}
			else
			{
				ViewBag.hata = "Kullanıcı adı veya şifre hatalı";
				return View();
			}
		}
		public ActionResult setting()
		{
			return View();
		}



		
		public ActionResult Sepet()
		{
			if (Session["KullaniciAd"] == null)
			{
				return RedirectToAction("Giris");
			}
			else
			{
				return View();
			}
		}


		public ActionResult Urun()
		{
			return View();
		}


		public ActionResult OdemeDetay()
		{
			return View();
		}
		public ActionResult Kayit()
		{
			var deger = db.gsorusu;
			List<gsorusu> list = deger.ToList();
			return View(list);
		}
		[HttpPost]
		public ActionResult Kayit(Kullanici data, gsorusu data2)
		{
			var bilgiler = db.Kullanici.FirstOrDefault(x => x.KullaniciMail == data.KullaniciMail);
			if (bilgiler == null)
			{
				db.Kullanici.Add(data);
				db.SaveChanges();
				return RedirectToAction("Giris");
			}
			else
			{
				ViewBag.hata = "Kullanıcı adı dahja önceden kullanılmış";
				return RedirectToAction("Index", ViewBag.hata);
			}	
		}
		[HttpGet]
		public ActionResult firmakayit()
		{
			return View();
 		}
		[HttpPost]
		public ActionResult  firmakayit(Firma a,HttpPostedFileBase c)
		{

			var dosyaadi = Path.GetFileName(Request.Files[0].FileName);
			var uzanti = Path.GetExtension(Request.Files[0].FileName);
			string randomName = "~/image/" + dosyaadi + uzanti;
			Request.Files[0].SaveAs(Server.MapPath(randomName));
			a.firmalogo = "~/image/ " + dosyaadi + uzanti;
			


			////HttpPostedFileBase file= Request.Files[a.firmalogo];

			////if (file!=null)
			////{
			//	string dosyaadi = Path.GetFileName(a.firmalogo);
			//	string uzanti = Path.GetExtension(a.firmalogo);
			//	string yol = "~/image/" + dosyaadi + uzanti;
			//	Request.Files[a.firmalogo].SaveAs(Server.MapPath(yol));
			//	a.firmalogo = "/image/" + dosyaadi + uzanti;

			////}
			//db.Firma.Add(a);
			//db.SaveChanges();
			//return View();

			///////////////////////////////////////////////////////////////////////////
			//ÇALIŞAN KOD SATIRI  ////
			//if (a.firmalogo.Length > 0)
			//{
			//	var c = a.firmalogo;
			//	var extent = Path.GetExtension(a.firmalogo);
			//	string randomName = "/image/" + Guid.NewGuid() + Path.GetExtension(a.firmalogo);
			//	string filepath = Server.MapPath(randomName);
			//	a.firmalogo = filepath;
			//	a.Save(Server.MapPath(filepath));
			//}
			////////////////////////////////////////////////////////////////////////////

			//a.firmalogo.SaveAs(Path.GetExtension(filepath));
			///////////////////////////////////////
			// Yol bulunuyor 
			//////////////////////////////////////


			//new FileStream(filepath, FileMode.Create);



			//Response.AddHeader("Content-Type", "application/json");

			//Response.Clear();
			//Response.ClearHeaders();
			//Response.ClearContent();
			//Response.AddHeader("con","sada"+c);
			//Response.Flush();
			//Response.TransmitFile(filepath);
			//Response.End();

			//HttpPostedFilepostedFile
			//			var file = Request.Files["firmalogo"];

			//if(file != null)
			//{

			//}


			//if (a.firmalogo != null)
			//{q
			//var extent = Path.GetExtension(a.firmalogo);
			//var randomName = ($"{Guid.NewGuid()}{extent}");
			//var path = Path.Combine(Directory.GetCurrentDirectory(), "firmalogo", randomName);
			//var path2 = "C:\\Users\\Bahadir\\Desktop\\staj ödevleri\\Eticaret\\Eticaret\\firmalogo\\" + randomName;
			//string uzantı = Path.GetExtension(a.firmalogo);

			//using (var stream = new FileStream(filepath, FileMode.Create))
			//{
			//	//await a.firmalogo.CopyTo(stream);
			//	//await a.firmalogo.CopyTo(stream); 
			//	//await a.firmalogo.CopyToAsync(stream);
			//	return Json("Dosya yüklendi");
			//	return View();
			//}

			//}
			//return View();
			//Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"/firmalogo/" + a.firmaMail);
			//string link= "C:\\Users\\Bahadir\\Desktop\\staj ödevleri\\Eticaret\\Eticaret\\firmalogo\\"+a.firmaMail;
			//var strem = new FileStream(link, FileMode.Create);


			//Directory.Move(uzantı, link);



			//if (a.firmalogo.Length > 0)
			//{
			//	if (Request.Files.Count > 0)
			//	{

			//	}
			//	string dosyaadi = Path.GetFileName(a.firmalogo);


			//	//string  = Path.GetExtension(Request.Files[a.firmalogo].FileName);

			//	string yol = "//firmalogo//" + dosyaadi;


			//}




			/////////////////////////////
			/// VERİ TABANI EKLEME
			db.Firma.Add(a);
			db.SaveChanges();
			return RedirectToAction("Index", "Home");
			/////////////////////////////
		}
		public ActionResult sifremiunuttum() 
		{
			var deger = db.gsorusu;
			List<gsorusu> list = deger.ToList();
			return View(list);
		}
		[HttpPost]
		public ActionResult sifremiunuttum(Kullanici c)
		{
			var degerler= db.Kullanici.FirstOrDefault(x => x.KullaniciMail == c.KullaniciMail && 
			x.KullaniciAd == c.KullaniciAd && x.KullaniciSoyad==c.KullaniciSoyad && x.gsorusuid == x.gsorusuid 
			&& x.gcevap == c.gcevap);



			if (degerler != null)
			{
				Session["id"] = degerler.KullaniciId;

				ViewBag.hatasifre = "kullanıcı bulunamadı";

				return RedirectToAction("newsifre");
			}
			else 
			{
				ViewBag.hata = "kullanıcı bilgileri bulunamadı";
				var deger = db.gsorusu;
				List<gsorusu> list = deger.ToList();
				return View(list); 
			}
		}

		public ActionResult newsifre()
		{
			
				return View();
			
		}
		[HttpPost]
		public ActionResult newsifre(Kullanici a)
		{
			string id;
			if (Session["id"] == null)
			{
				id = Session["KullaniciId"].ToString();
			}
			else
			{
				id = Session["id"].ToString();
			}
			
			var degerler = db.Kullanici.FirstOrDefault(x=>x.KullaniciId.ToString()==id);

			degerler.KullaniciSifre=a.KullaniciSifre;
			db.SaveChanges();

			if (Session["id"] == null) 
			{
				return RedirectToAction("setting","Home");

			}
			else
			{
				return RedirectToAction("Giris");
			}
		}
		public ActionResult firmasetting() 
		{
			return View();
		}

		public ActionResult Firma()
		{
			return View();
		}
		public ActionResult Guncelle()
		{
			return View();
		}
		public ActionResult YeniUrun()
		{
			return View();
		}
	}
}