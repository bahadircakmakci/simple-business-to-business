using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.DomainLayer.UnitOfWork;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace simple_business_to_business.PresentationLayer.Controllers
{
    public class OrderController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet, Authorize]
        public IActionResult OrderList(int totalpage, int pageIndex = 1)
        {
             if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            ViewBag.Currentpage = pageIndex;

            var totalcompany = _unitOfWork.OrderRepository.GetAll().Result.Count();
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
        [HttpPost, Authorize]
        public async Task<IActionResult> OrderList(int pageIndex = 1)
        {
            var companyid = await _unitOfWork.AppUser.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var list =  await _unitOfWork.OrderRepository.GetFilteredList(
               selector: x => new Orders
               {
                   Id = x.Id,
                   AppUserId=x.AppUserId,
                   CompanyId=x.CompanyId,
                   SubTotal=x.SubTotal,
                   TotalDiscount=x.TotalDiscount,
                   DeleteDate=x.DeleteDate,
                   TotalTax=x.TotalTax,
                   UpdateDate=x.UpdateDate,
                   AppUsers=x.AppUsers,
                   Companies=x.Companies,
                   CreateDate=x.CreateDate
                     
               },
               expression: x=>x.CompanyId== companyid.CompanyId,
               include: x => x.Include(x => x.AppUsers).Include(x => x.Companies),
               pageIndex: pageIndex,
               pageSize: 10);
             
            return Json(list, new JsonSerializerSettings());
          
        }
        public async Task<IActionResult> OrderDetails(Guid id, int pageIndex = 1)
        {
            var list = await _unitOfWork.OrderDetailsRepository.GetFilteredList(
              selector: x => new OrderDetails
              {
                  Id = x.Id,
                  Products=x.Products,
                  DeleteDate=x.DeleteDate,
                  Orders=x.Orders,
                  OrderId=x.OrderId,
                  Discount=x.Discount,
                  Price=x.Price,
                  ProductId=x.ProductId,
                  Quantity=x.Quantity,
                  Status=x.Status,
                  Tax=x.Tax,
                  Total=x.Total,
                  UpdateDate=x.UpdateDate,
                  CreateDate = x.CreateDate

              },
              expression: x => x.OrderId == id,
              include: x => x.Include(x => x.Products).ThenInclude(z=>z.ProductPictures).Include(x => x.Products).ThenInclude(z => z.Brands).Include(x => x.Products).ThenInclude(z => z.MainAttributes).Include(x => x.Products).ThenInclude(z => z.SubCategories),
              pageIndex: pageIndex,
              pageSize: 100);
            return View(list);
        }
    }
}
