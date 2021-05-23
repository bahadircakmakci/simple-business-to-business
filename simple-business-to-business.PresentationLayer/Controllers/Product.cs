using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Concrete;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.DomainLayer.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace simple_business_to_business.PresentationLayer.Controllers
{
    [Authorize, AutoValidateAntiforgeryToken]
    public class Product : Controller
    {

        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly IMainCategoryService _maincategoryService;
        private readonly ISubCategoryService _subcategoryService;
        private readonly IProductPictureService _productPictureService;
        private readonly IBasketService _basketService;
        private readonly IAppUserService _userService;
        public Product(IProductService productService, IBrandService brandService, IMainCategoryService maincategoryService, ISubCategoryService subcategoryService, IProductPictureService productPictureService, IBasketService basketService, IAppUserService userService)
        {
            _productService = productService;
            _brandService = brandService;
            _maincategoryService = maincategoryService;
            _subcategoryService = subcategoryService;
            _productPictureService = productPictureService;
            _basketService = basketService;
            _userService = userService;
        }

        [HttpGet, Authorize]
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
            var usrid = _userService.UserIdFromName(User.Identity.Name).Result;
            ViewBag.BasketCount = _basketService.GetAll().Result.Where(x => x.AppUserId == usrid).Select(x => x.BasketQuantity).Sum();
            return View();
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Products(int pageindex = 1)
        {
            var list = await _productService.List(pageindex);
            return Json(list, new JsonSerializerSettings());
        }
        [HttpGet,Authorize(Roles ="admin")]
        public async Task<IActionResult> ProductEdit(int id)
        {
            ViewData["Brands"] = _brandService.GetAll().Result.Select(

                n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.BrandName
                });
            ViewData["MainCategory"] = _maincategoryService.GetAll().Result.Select(

                n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.CategoyName
                });
            ViewData["SubCategory"] = _subcategoryService.GetAll().Result.Select(

                n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.CategoyName
                });
            return View(await _productService.GetById(id));
        }
        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> ProductEdit(ProductDto productDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                productDto.ListPriceVat = productDto.ListPrice * ((productDto.Vat / 100) + 1);
                await _productService.Edit(productDto);

                var getid = await _productService.ProductIdFromName(productDto.ProductName);
                if (getid > 0)
                {
                    var pictureid =await _productPictureService.IdFromName(getid.ToString());
                    var veri = _productPictureService.GetAll().Result.Where(x=>x.Id== pictureid).FirstOrDefault();
                    if (file!=null)
                    {
                        veri.Image = file;
                    }
                    await _productPictureService.Edit(veri);
                }
            }

            return RedirectToAction("Products", "Product");
        }

        [HttpGet, Authorize(Roles = "admin")]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var product = await _productService.GetById(id);
            var pictureid = await _productPictureService.IdFromName(product.Id.ToString());
            var sepet = _basketService.GetAll().Result.Where(x=>x.ProductId==product.Id).ToList();
            if (sepet.Count==0)
            {
                await _productPictureService.Delete(pictureid);
                await _productService.Delete(product.Id);
            }
            
            return RedirectToAction("Products", "Product");
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> BasketAdd(int id)
        {
            var product = await _productService.GetById(id);
            var usrid = await _userService.UserIdFromName(User.Identity.Name);
            if (product!=null)
            {
                

                await _basketService.Add(new BasketDTO { AppUserId= usrid, AppUsers=null, BasketQuantity=1,ProductId=product.Id, Products=null, Status=Status.Active });
            }  
            return RedirectToAction("Products", "Product");
        }
         
        [HttpGet, Authorize(Roles = "admin")]
        public IActionResult ProductAdd()
        {
            ViewData["Brands"] = _brandService.GetAll().Result.Select(

                n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.BrandName
                });
            ViewData["MainCategory"] = _maincategoryService.GetAll().Result.Select(

                n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.CategoyName
                });
            ViewData["SubCategory"] = _subcategoryService.GetAll().Result.Select(

                n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.CategoyName
                });
             



            return View();
        }

        [HttpPost, Authorize(Roles = "admin")]
        public async Task<IActionResult> ProductAdd(ProductDto productDto ,IFormFile file)
        {

            if (ModelState.IsValid)
            {
                productDto.ListPriceVat = productDto.ListPrice * ((productDto.Vat / 100) + 1);
                await _productService.Add(productDto);

                var getid = await _productService.ProductIdFromName(productDto.ProductName);
                if (getid>0)
                {
                    var veri = new ProductPictureDTO
                    {
                        Image = file,
                        ProductId = getid,
                        Status = Status.Active

                    };
                    await _productPictureService.Add(veri);
                }
               
            }
            
            return RedirectToAction("Products", "Product");
        }
    }
}
