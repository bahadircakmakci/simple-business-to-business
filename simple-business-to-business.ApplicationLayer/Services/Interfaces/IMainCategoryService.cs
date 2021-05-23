using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Interfaces
{
    public interface IMainCategoryService
    {
        Task<int> IdFromName(string name);
        Task<CategoryDTO> GetById(int id);
        Task Edit(CategoryDTO editDTO);
        Task Add(CategoryDTO AddDTO);
        Task Delete(int id);
        Task<List<CategoryDTO>> List(int pageIndex);
        Task<List<CategoryDTO>> GetAll();
    }
}
