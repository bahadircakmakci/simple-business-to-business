using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Interfaces
{
    public interface IBasketService
    {
        Task<int> IdFromName(string name);
        Task<BasketDTO> GetById(int id);
        Task<BasketDTO> GetByUserProductId(int Userid, int productId);
        Task Edit(BasketDTO editDTO);
        Task Add(BasketDTO AddDTO);
        Task Delete(int id);
        Task<List<BasketDTO>> List(int pageIndex);
        Task<List<BasketDTO>> GetAll();
    }
}
