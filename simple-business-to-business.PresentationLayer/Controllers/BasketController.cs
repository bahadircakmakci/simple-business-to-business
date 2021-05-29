using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.DomainLayer.Enums;
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
        private readonly IMapper _mapper;
        public BasketController(IUnitOfWork unitOfWork, IBasketService basketService, IAppUserService userService,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _basketService = basketService;
            _userService = userService;
            _mapper = mapper;
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
        [HttpPost, Authorize]
        public async Task<JsonResult> BasketQuantityUpdate(int Id, int Count)
        {
            var usrid = _userService.UserIdFromName(User.Identity.Name).Result;
            var getbasket = await _basketService.GetByUserProductId(usrid, Id);
            if (getbasket != null)
            {
                getbasket.BasketQuantity = Count;
                await _basketService.Edit(getbasket);
                return Json(Ok(), new JsonSerializerSettings());
            }

            return null;
        }
        [HttpGet, Authorize]
        public async Task<IActionResult> OrderCreate()
        {
            var usr = _unitOfWork.AppUser.Get(x => x.UserName == User.Identity.Name).Result.FirstOrDefault();
            var sepetim = await _unitOfWork.BasketRepository.GetFilteredList(
                selector: x => new BasketDTO
                {
                    Id = x.Id,
                    AppUserId = x.AppUserId,
                    AppUsers = _mapper.Map<EditProfileDTO>(x.AppUsers),
                    BasketQuantity = x.BasketQuantity,
                    DeleteDate = x.DeleteDate,
                    ProductId = x.ProductId,
                    Products = _mapper.Map<ProductDto>(x.Products),
                    Status = x.Status,
                    UpdateDate = x.UpdateDate
                },
                expression:null,
                include: x => x.Include(z => z.AppUsers).Include(z => z.Products).ThenInclude(a => a.Brands).Include(p => p.Products).ThenInclude(s => s.ProductPictures)
                 
                );
            var orderguiid = Guid.NewGuid();
            if (sepetim.Count>0)
            {
                var neworder = new OrderDTO();
                neworder.Id = orderguiid;
                neworder.AppUserId = usr.Id;
                neworder.CompanyId = usr.CompanyId ?? 1;
                neworder.Status = Status.Active;
                neworder.TotalDiscount = 0;
                neworder.SubTotal = sepetim.Sum(x => x.Products.ListPrice * x.BasketQuantity);
                neworder.TotalTax = sepetim.Sum(x => x.Products.ListPriceVat * x.BasketQuantity) - neworder.SubTotal;
                var orderid=  _unitOfWork.OrderRepository.Add(_mapper.Map<Orders>(neworder));
               
                var neworderDetail = new OrderDetailDTO();
                foreach (var ord in sepetim)
                {
                    await _unitOfWork.OrderDetailsRepository.Add(_mapper.Map<OrderDetails>(new OrderDetailDTO
                    {
                        OrderId = orderguiid,
                        Price = ord.Products.ListPrice,
                        ProductId = ord.ProductId,
                        Discount = 0,
                        Quantity = ord.BasketQuantity,
                        Status = Status.Active,
                        Tax = (ord.Products.ListPrice * ord.BasketQuantity) * (ord.Products.Vat / 100),
                        Total = (ord.Products.ListPrice * ord.BasketQuantity)

                    }));
                    ord.AppUsers = null;
                      _unitOfWork.BasketRepository.Delete(_mapper.Map<Basket>(ord));
                }              
                var getcompony = await _unitOfWork.CompanyRepository.GetById(usr.CompanyId??1);
                getcompony.TotalBalance+=(neworder.SubTotal+neworder.TotalTax);
                  _unitOfWork.CompanyRepository.Update(getcompony);
                 
                await _unitOfWork.Commit();
                await _unitOfWork.DisposeAsync();

            }
            


            return RedirectToAction("OrderList", "Order");
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> BasketDelete(int id)
        {
            var basketid = await _basketService.GetById(id);
            await _basketService.Delete(basketid.Id);           
            return RedirectToAction("BasketList", "Basket");
        }

    }
}
