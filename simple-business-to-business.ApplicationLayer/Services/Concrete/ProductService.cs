using AutoMapper;
using Microsoft.EntityFrameworkCore;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.DomainLayer.Repositories.Interfaces.EntityType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _Mapper;
        public ProductService(IProductRepository productRepository, IMapper Mapper)
        {
            _productRepository = productRepository;
            _Mapper = Mapper;
        }
        public async Task Add(ProductDto AddCompany)
        {
            await _productRepository.Add(_Mapper.Map<Products>(AddCompany));
            await _productRepository.Commit();
            
        }

        public async Task Delete(int id)
        {
            var removeproduct = await _productRepository.GetById(id);
            _productRepository.Delete(removeproduct);
            await _productRepository.Commit();
        }

        public async Task Edit(ProductDto editCompanyDTO)
        {
            _productRepository.Update(_Mapper.Map<Products>(editCompanyDTO));
            await _productRepository.Commit();
        }

        public async Task<List<ProductDto>> GetAll()
        {
            return _Mapper.Map<List<ProductDto>>(await _productRepository.GetAll());
        }

        public async Task<ProductDto> GetById(int id)
        {
            return _Mapper.Map<ProductDto>(await _productRepository.GetById(id));
        }

        public async Task<List<ProductDto>> List(int pageIndex)
        {
            var product = await _productRepository.GetFilteredList(
               selector: x => new ProductDto
               {
                   Id = x.Id,
                   BrandId = x.BrandId,
                   ProductName = x.ProductName,
                   CriticalQuantity = x.CriticalQuantity,
                   CurrencyId = x.CurrencyId,
                   Description = x.Description,
                   ListPrice = x.ListPrice,
                   ListPriceVat = x.ListPriceVat,
                   MainCategoryId = x.MainCategoryId,
                   MaxSellerQuantity = x.MaxSellerQuantity,
                   ProductCode = x.ProductCode,
                   Quantity = x.Quantity,
                   SubCategoryId = x.SubCategoryId,
                   Vat = x.Vat,
                   ProductPictures = _Mapper.Map<ProductPictureDTO>(x.ProductPictures),
                   Brands = _Mapper.Map<BrandDTO>(x.Brands),
                   MainCategories = _Mapper.Map<CategoryDTO>(x.MainCategories),
                   Status = x.Status,
                   SubCategories= _Mapper.Map<CategoryDTO>(x.SubCategories)
                  
               },
               expression: null,
               include: x => x.Include(a=>a.MainCategories).Include(z => z.ProductPictures).Include(s => s.SubCategories).Include(k => k.Brands),
               pageIndex: pageIndex,
               pageSize: 10); 

            return product;
        }

        public async Task<int> ProductIdFromName(string productname)
        {
            return await _productRepository.GetFilteredFirstOrDefault(selector: x => x.Id, expression: x => x.ProductName == productname);
        }
    }
}
