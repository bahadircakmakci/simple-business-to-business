using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.DomainLayer.UnitOfWork;
using simple_business_to_business.PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace simple_business_to_business.PresentationLayer.Controllers
{
    [Authorize, AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppUserService _appUserService;
        private readonly ICompanyService _companyService;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IAppUserService userService, ICompanyService companyService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _appUserService = userService;
            _companyService = companyService;
            _unitOfWork = unitOfWork;
        }
        [HttpGet, Authorize]
        public async Task<IActionResult> Index()
        {
            var usrid = await _appUserService.UserIdFromName(User.Identity.Name);
            var usr = await _appUserService.GetById(usrid);

            ViewBag.TotalOrder = _unitOfWork.OrderRepository.Get(x => x.CompanyId == usr.CompanyId).Result.Count();
            ViewBag.TotalUser= _unitOfWork.AppUser.Get(x => x.CompanyId == usr.CompanyId).Result.Count();
            ViewBag.Bakiye = _unitOfWork.OrderRepository.Get(x => x.CompanyId == usr.CompanyId).Result.Sum(x=>x.SubTotal)+ _unitOfWork.OrderRepository.Get(x => x.CompanyId == usr.CompanyId).Result.Sum(x => x.TotalTax);
          


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
        [HttpGet, Authorize]
        public async Task<IActionResult> UserAccountEdit(int id)
        {  
            var usrid = await _appUserService.UserIdFromName(User.Identity.Name);
            if (usrid != id)
            {
                return StatusCode(403, "Yok Böyle Bir Erişim :D");
            }
           var usr=await _appUserService.GetById(id);
            ViewData["Companies"] = _companyService.GetAll().Result.Select(
               
                n=>new SelectListItem
                {
                    Value= n.Id.ToString(),
                    Text=n.CompanyName
                }
                );
            return View(usr);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> UserAccountEdit(EditProfileDTO model, IFormFile file)
        { 
            if (ModelState.IsValid)
            {
                if (file!=null)
                {
                    model.Image = file;
                }
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
