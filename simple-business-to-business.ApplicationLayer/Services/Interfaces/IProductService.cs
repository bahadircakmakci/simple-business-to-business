using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<int> ProductIdFromName(string productname);
        Task<ProductDto> GetById(int id);
        Task EditProduct(ProductDto editCompanyDTO);
        Task AddProduct(ProductDto AddCompany);
        Task DeleteProduct(int id);
        Task<List<ProductDto>> ListProdcut(int pageIndex);
        Task<List<ProductDto>> GetAll();
    }
}
