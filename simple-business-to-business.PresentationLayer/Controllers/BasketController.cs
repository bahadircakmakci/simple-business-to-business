using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.DomainLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_business_to_business.PresentationLayer.Controllers
{
    public class BasketController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;
        private readonly IAppUserService _userService;
        public BasketController(IUnitOfWork unitOfWork, IBasketService basketService, IAppUserService userService)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _userService = userService;
        }
        [HttpGet, Authorize]
        public IActionResult BasketList(int totalpage, int pageIndex = 1)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            ViewBag.Currentpage = pageIndex;

            var totalcompany = _basketService.GetAll().Result.Count();
            if (totalcompany % 10 == 0)
            {
                ViewBag.Totalpage = totalcompany / 10;
            }
            else
            {
                ViewBag.Totalpage = (totalcompany / 10) + 1;
            }
            var usrid = _userService.UserIdFromName(User.Identity.Name).Result;
            ViewBag.BasketCount = _basketService.GetAll().Result.Where(x => x.AppUserId == usrid).Select(x => x.BasketQuantity).Sum();
            return View();
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> BasketList(int pageindex = 1)
        {
            var list = await _basketService.List(pageindex);
            return Json(list, new JsonSerializerSettings());
        }
    }
}
