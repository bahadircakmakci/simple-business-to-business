using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        private readonly IAppUserService _userService;

        

        public HomeController(ILogger<HomeController> logger, IAppUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet,Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            _userService.LogOut();
            
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
