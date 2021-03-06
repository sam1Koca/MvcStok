using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities dbStokEntities = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
            var degerler = from d in dbStokEntities.TBLMUSTERILER select d; //LINQ sorgusu : degerler: d Musteriler içinde bul ve d'ye at
            if (!string.IsNullOrEmpty(p)) // eğerki p değeri null değilse
            {
                degerler = degerler.Where (m => m.MUSTERIAD.Contains (p)); // musteriadi icinde paremetreye eşit olan değerleri getir
            } 

            return View(degerler.ToList()); // değerleri liste halinde döndür.


            // var musteriler = dbStokEntities.TBLMUSTERILER.ToList();
            // return View(musteriler);


        }

        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERILER musteri)
        {
            if (!ModelState.IsValid)
            {

                return View("YeniMusteri"); 

            }
            dbStokEntities.TBLMUSTERILER.Add(musteri);
            dbStokEntities.SaveChanges(); // Değişiklikleri Kayıt Et.
            return View();
        }

        public ActionResult MusteriSil(int id)
        {
            var musteri = dbStokEntities.TBLMUSTERILER.Find(id);
            dbStokEntities.TBLMUSTERILER.Remove(musteri);

            dbStokEntities.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var musteri = dbStokEntities.TBLMUSTERILER.Find(id);
            return View("MusteriGetir", musteri);
        }

        public ActionResult Guncelle(TBLMUSTERILER item)
        {
            var musteri = dbStokEntities.TBLMUSTERILER.Find(item.MUSTERIID); // MUTERIID'ye göre bul
            musteri.MUSTERIAD = item.MUSTERIAD; // item'dan gelen müşteri adı, musteri.MUSTERİAD'a ata.
            musteri.MUSTERISOYAD = item.MUSTERISOYAD;
            dbStokEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}