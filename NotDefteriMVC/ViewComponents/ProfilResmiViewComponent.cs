using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotDefteriMVC.Data;
using NotDefteriMVC.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NotDefteriMVC.ViewComponents
{
    public class ProfilResmiViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ProfilResmiViewComponent(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync(int genislik, int yukseklik, string sinif)
        {
            var userId = ((ClaimsPrincipal)User).FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            var vm = new ProfilResmiComponentViewModel()
            {
                DosyaAd = user.PhotoPath,
                Genislik = genislik,
                Yukseklik = yukseklik,
                Sinif = sinif
            };

            return View(vm);
        }
    }
}
