using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class BasketService : IBasketService
    {

        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _Mapper;
        public BasketService(IBasketRepository basketRepository, IMapper Mapper)
        {
            _basketRepository = basketRepository;
            _Mapper = Mapper;
        }
        public async Task Add(BasketDTO AddDTO)
        {
            await _basketRepository.Add(_Mapper.Map<Basket>(AddDTO));
            await _basketRepository.Commit();
            await _basketRepository.DisposeAsync();
        }

        public async Task Delete(int id)
        {
            var removeproduct = await _basketRepository.GetById(id);
            _basketRepository.Delete(removeproduct);
            await _basketRepository.Commit();
        }

        public async Task Edit(BasketDTO editDTO)
        {
            _basketRepository.Update(_Mapper.Map<Basket>(editDTO));
            await _basketRepository.Commit();
        }

        public async Task<List<BasketDTO>> GetAll()
        {
            return _Mapper.Map<List<BasketDTO>>(await _basketRepository.GetAll());
        }

        public async Task<BasketDTO> GetById(int id)
        {
            return _Mapper.Map<BasketDTO>(await _basketRepository.GetById(id));
        }

        public Task<int> IdFromName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BasketDTO>> List(int pageIndex)
        {
            var product = await _basketRepository.GetFilteredList(
               selector: x => new BasketDTO
               {
                   Id = x.Id,
                   AppUserId = x.AppUserId,
                   AppUsers = _Mapper.Map<EditProfileDTO>(x.AppUsers),
                   BasketQuantity = x.BasketQuantity,
                   DeleteDate = x.DeleteDate,
                   ProductId = x.ProductId,
                   Products = _Mapper.Map<ProductDto>(x.Products),
                   Status = x.Status,
                   UpdateDate = x.UpdateDate
               },
               expression: null,
               include: x=>x.Include(z=>z.AppUsers).Include(z=>z.Products),
               pageIndex: pageIndex,
               pageSize: 10) ;

            return product;
        }
    }
}
