using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_business_to_business.PresentationLayer.Controllers
{
    [Authorize, AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {

        private readonly IAppUserService _userService;

        public AccountController(IAppUserService appUserService)
        {
            _userService = appUserService;
        }
        [HttpGet,AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Login(model);
                if (result!=null)
                {
                    if (result.Succeeded) return RediredToLocal(returnUrl);
                    ModelState.AddModelError(String.Empty, "Invalid Login Attampt..!");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Kullanıcı Adı,Şifreniz Yanlış olabilir veya Kullanıcınız Henüz Aktif Edilmemiş Olabilir.");
                }
                
            }

            return View();
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction(nameof(HomeController.Index), "Home");
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
        
            if (ModelState.IsValid)
            {
               
                var result = await _userService.Register(model);

                if (result.Succeeded) {
                    ModelState.AddModelError(string.Empty, "Başarı ile  Kayıt Olundu... ancak Kullanıcınız onaylanana kadar giriş yapamazssınız!");
                }
                

                foreach (var item in result.Errors) ModelState.AddModelError(string.Empty, item.Description);
            }

            return View(model);
        }

        private IActionResult RediredToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            else return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
