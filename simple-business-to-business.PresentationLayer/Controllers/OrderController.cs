using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_business_to_business.PresentationLayer.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult OrderList()
        {
            return View();
        }
    }
}
