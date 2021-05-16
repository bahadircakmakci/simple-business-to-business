using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Concrete
{
    public class ProductService : IProductService
    {
        public Task AddProduct(ProductDto AddCompany)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditProduct(ProductDto editCompanyDTO)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDto>> ListProdcut(int pageIndex)
        {
            throw new NotImplementedException();
        }

        public Task<int> ProductIdFromName(string productname)
        {
            throw new NotImplementedException();
        }
    }
}
