using AutoMapper;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.DomainLayer.Repositories.Interfaces.EntityType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Concrete
{
    public class SubCategoryService : ISubCategoryService
    {
        private ISubCategoryRepository _subCategoryRepository;
        private IMapper _Mapper;
        public SubCategoryService(ISubCategoryRepository subCategoryRepository, IMapper Mapper)
        {
            _subCategoryRepository = subCategoryRepository;
            _Mapper = Mapper;
        }
        public async Task Add(CategoryDTO AddDTO)
        {
            await _subCategoryRepository.Add(_Mapper.Map<SubCategories>(AddDTO));
            await _subCategoryRepository.Commit();
            await _subCategoryRepository.DisposeAsync();
        }

        public async Task Delete(int id)
        {
            var remove = await _subCategoryRepository.GetById(id);
            _subCategoryRepository.Delete(remove);
            await _subCategoryRepository.Commit();
        }

        public async Task Edit(CategoryDTO editDTO)
        {
            _subCategoryRepository.Update(_Mapper.Map<SubCategories>(editDTO));
            await _subCategoryRepository.Commit();
        }

        public async Task<List<CategoryDTO>> GetAll()
        {
            return _Mapper.Map<List<CategoryDTO>>(await _subCategoryRepository.GetAll());
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            return _Mapper.Map<CategoryDTO>(await _subCategoryRepository.GetById(id));
        }

        public async Task<int> IdFromName(string name)
        {
            return await _subCategoryRepository.GetFilteredFirstOrDefault(selector: x => x.Id, expression: x => x.CategoyName == name);
        }

        public async Task<List<CategoryDTO>> List(int pageIndex)
        {
            var category = await _subCategoryRepository.GetFilteredList(
               selector: x => new CategoryDTO
               {
                   Id = x.Id,
                   CategoyName = x.CategoyName,
                   DeleteDate = x.DeleteDate,
                   Description = x.Description,
                   ImagePath = x.ImagePath,
                   UpdateDate = x.UpdateDate,
                   Status = x.Status
               },
               expression: null,
               include: null,
               pageIndex: pageIndex,
               pageSize: 10);

            return category;
        }
    }
}
