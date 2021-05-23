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
    public class BrandService : IBrandService
    {

        private IBrandRepository _brandRepository;
        private IMapper _Mapper;
        public BrandService(IBrandRepository brandRepository, IMapper Mapper)
        {
            _Mapper = Mapper;
            _brandRepository = brandRepository;
        }
        public async Task Add(BrandDTO AddDTO)
        {
            await _brandRepository.Add(_Mapper.Map<Brands>(AddDTO));
            await _brandRepository.Commit();
            await _brandRepository.DisposeAsync();
        }

        public async Task Delete(int id)
        {
            var remove = await _brandRepository.GetById(id);
            _brandRepository.Delete(remove);
            await _brandRepository.Commit();
        }

        public async Task Edit(BrandDTO editDTO)
        {
            _brandRepository.Update(_Mapper.Map<Brands>(editDTO));
            await _brandRepository.Commit();
        }

        public async Task<List<BrandDTO>> GetAll()
        {
            return _Mapper.Map<List<BrandDTO>>(await _brandRepository.GetAll());
        }

        public async Task<BrandDTO> GetById(int id)
        {
            return _Mapper.Map<BrandDTO>(await _brandRepository.GetById(id));
        }

        public async Task<int> IdFromName(string name)
        {
            return await _brandRepository.GetFilteredFirstOrDefault(selector: x => x.Id, expression: x => x.BrandName == name);
        }

        public async Task<List<BrandDTO>> List(int pageIndex)
        {
            var brands = await _brandRepository.GetFilteredList(
               selector: x => new BrandDTO
               {
                   Id = x.Id,
                   BrandName=x.BrandName,
                   DeleteDate=x.DeleteDate,
                   Description=x.Description,
                   ImagePath=x.ImagePath,
                   UpdateDate=x.UpdateDate,
                   Status = x.Status
               },
               expression: null,
               include: null,
               pageIndex: pageIndex,
               pageSize: 10);

            return brands;
        }
    }
}
