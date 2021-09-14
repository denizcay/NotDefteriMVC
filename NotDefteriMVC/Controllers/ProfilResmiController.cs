using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotDefteriMVC.Data;
using NotDefteriMVC.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotDefteriMVC.Controllers
{
    [Authorize]
    public class ProfilResmiController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly ApplicationDbContext db;

        public ProfilResmiController(IWebHostEnvironment env, ApplicationDbContext db)
        {
            this.env = env;
            this.db = db;
        }
        public IActionResult Index(string sonuc)
        {
            var vm = new ProfilResmiViewModel()
            {
                Yuklendi = sonuc == "yuklendi",
                Foto = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier)).PhotoPath
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                ModelState.AddModelError("photo", "Yüklenecek bir resim dosyası bulunamadı.");

            else if (!photo.ContentType.StartsWith("image/"))
                ModelState.AddModelError("photo", "Lütfen bir resim dosyası seçiniz.");

            // İzin verilen en büyük doya boyutu 1MB
            else if (photo.Length > 1 * 1000 * 1000)
                ModelState.AddModelError("photo", "Resim en fazla 1MB olmalıdır.");

            if (ModelState.IsValid)
            {
                string uzanti = Path.GetExtension(photo.FileName);
                string dosyaAd = Guid.NewGuid() + uzanti;
                string kayitYolu = Path.Combine(env.WebRootPath, "upload", dosyaAd);

                using (FileStream fs = System.IO.File.Create(kayitYolu))
                {
                    photo.CopyTo(fs);
                }

                ApplicationUser user = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));
                user.PhotoPath = dosyaAd;
                db.SaveChanges();
                return RedirectToAction("Index", new { sonuc = "yuklendi" });
            }

            var vm = new ProfilResmiViewModel()
            {
                Foto = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier)).PhotoPath
            };

            return View(vm);
        }
    }
}
