using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Interfaces
{
    public interface IProductPictureService
    {
        Task<int> IdFromName(string name);
        Task<ProductPictureDTO> GetById(int id);
        Task Edit(ProductPictureDTO editDTO);
        Task Add(ProductPictureDTO AddDTO);
        Task Delete(int id);
      
        Task<List<ProductPictureDTO>> GetAll();
    }
}
