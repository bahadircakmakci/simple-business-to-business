using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace simple_business_to_business.PresentationLayer.Controllers
{
    [Authorize, AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;

        public HomeController(ILogger<HomeController> logger, IAppUserService userService, ICompanyService companyService)
        {
            _logger = logger;
            _appUserService = userService;
            _companyService = companyService; 
        }
        [HttpGet, Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            _appUserService.LogOut();

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet, Authorize(Roles = "admin")]
        public  IActionResult UsersAccount(int totalpage, int pageIndex = 1)
        {
            
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            ViewBag.Currentpage = pageIndex;

            var totalcompany = _appUserService.GetAll().Result.Count();
            if (totalcompany % 10 == 0)
            {
                ViewBag.Totalpage = totalcompany / 10;
            }
            else
            {
                ViewBag.Totalpage = (totalcompany / 10) + 1;
            }

            return View();
             
        }
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> UsersAccount(int pageindex = 1)
        { 
                var userlist = await _appUserService.ListUser(pageindex);
                return Json(userlist, new JsonSerializerSettings());
             
        }
        [HttpGet, Authorize(Roles = "admin")]
        public async Task<IActionResult> UserAccountEdit(int id)
        {
            ViewData["Companies"] = _companyService.GetAll().Result.Select(
               
                n=>new SelectListItem
                {
                    Value= n.Id.ToString(),
                    Text=n.CompanyName
                }
                );
            return View(await _appUserService.GetById(id));
        }

        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> UserAccountEdit(EditProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                await _appUserService.EditUser(model);
                
            }
            return RedirectToAction("UsersAccount", "Home");
        }
        [HttpGet, Authorize(Roles = "admin")]
        public async Task<IActionResult> UserAccountDelete(int id)
        {
            if (id>0)
            {
                //delete kodu yazılacak
            }
            return RedirectToAction("UsersAccount", "Home");
        }

        [HttpGet, Authorize(Roles = "admin")]
        public IActionResult WaitingUsersAccount(int totalpage, int pageIndex = 1)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            ViewBag.Currentpage = pageIndex;

            var totalcompany = _appUserService.GetAll().Result.Where(x=>x.CompanyId==null || x.CompanyId==0).Count();
            if (totalcompany % 10 == 0)
            {
                ViewBag.Totalpage = totalcompany / 10;
            }
            else
            {
                ViewBag.Totalpage = (totalcompany / 10) + 1;
            } 
            return View();
        }
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> WaitingUsersAccount(int pageindex = 1)
        {
            var userlist = await _appUserService.WaitListUser(pageindex);
            return Json(userlist, new JsonSerializerSettings());
        }
    }
}
