using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using simple_business_to_business.ApplicationLayer.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_business_to_business.PresentationLayer.Controllers
{
    [Authorize, AutoValidateAntiforgeryToken]
    public class Product : Controller
    {

        private readonly ProductService _productService;

        public Product(ProductService productService)
        {
            _productService = productService;
        }
         
        [HttpGet, Authorize(Roles = "admin")]
        public IActionResult Products(int totalpage, int pageIndex = 1)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            ViewBag.Currentpage = pageIndex;

            var totalcompany = _productService.GetAll().Result.Count();
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
    }
}
