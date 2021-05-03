using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace simple_business_to_business.PresentationLayer.Controllers
{
    [Authorize, AutoValidateAntiforgeryToken]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
        [HttpGet, Authorize(Roles = "admin")]
        public IActionResult CompanyList()
        {
            return View();
        }

        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> CompanyList(int pageIndex = 1)
        {
            var company = await _companyService.ListCompany(pageIndex);
            return Json(company, new JsonSerializerSettings());

        }
        [HttpGet, Authorize(Roles = "admin")]
        public IActionResult CompanyAdd()
        {
            return View();
        }
    }
}
