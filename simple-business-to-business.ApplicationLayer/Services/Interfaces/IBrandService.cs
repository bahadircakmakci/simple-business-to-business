using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Interfaces
{
    public interface IBrandService
    {
        Task<int> IdFromName(string name);
        Task<BrandDTO> GetById(int id);
        Task Edit(BrandDTO editDTO);
        Task Add(BrandDTO AddDTO);
        Task Delete(int id);
        Task<List<BrandDTO>> List(int pageIndex);
        Task<List<BrandDTO>> GetAll();
    }
}
