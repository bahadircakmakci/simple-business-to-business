using AutoMapper;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.DomainLayer.Repositories.Interfaces.EntityType;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Concrete
{
    public class ProductPictureService : IProductPictureService
    {
        private IProductPictureRepository _productpictureRepository;
        private IMapper _Mapper;
        public ProductPictureService(IProductPictureRepository productpictureRepository, IMapper mapper)
        {
            _productpictureRepository = productpictureRepository;
            _Mapper = mapper;
        }
        public async Task Add(ProductPictureDTO AddDTO)
        {

            if (AddDTO.Image != null)
            {
                using var image = Image.Load(AddDTO.Image.OpenReadStream());
                image.Mutate(x => x.Resize(256, 256));
                string newName = Guid.NewGuid().ToString();
                image.Save($"wwwroot/images/product/{newName}.jpg");
                AddDTO.ImagePath = ($"/images/product/{newName}.jpg");
                AddDTO.Image = null;
            }
            await _productpictureRepository.Add(_Mapper.Map<ProductPictures>(AddDTO));
            await _productpictureRepository.Commit();
            await _productpictureRepository.DisposeAsync();
        }

        public async Task Delete(int id)
        {
            var remove = await _productpictureRepository.GetById(id);
            _productpictureRepository.Delete(remove);
            await _productpictureRepository.Commit();
        }

        public async Task Edit(ProductPictureDTO editDTO)
        {
            if (editDTO.Image != null)
            {
                using var image = Image.Load(editDTO.Image.OpenReadStream());
                image.Mutate(x => x.Resize(256, 256));
                string newName = Guid.NewGuid().ToString();
                image.Save($"wwwroot/images/product/{newName}.jpg");
                editDTO.ImagePath = ($"/images/product/{newName}.jpg");
                editDTO.Image = null;
            }
            _productpictureRepository.Update(_Mapper.Map<ProductPictures>(editDTO));
            await _productpictureRepository.Commit();
        }

        public async Task<List<ProductPictureDTO>> GetAll()
        {
            return _Mapper.Map<List<ProductPictureDTO>>(await _productpictureRepository.GetAll());
        }

        public async Task<ProductPictureDTO> GetById(int id)
        {
            return _Mapper.Map<ProductPictureDTO>(await _productpictureRepository.GetById(id));
        }

        public async Task<int> IdFromName(string name)
        {
            return await _productpictureRepository.GetFilteredFirstOrDefault(selector: x => x.Id, expression: x => x.ProductId.ToString() == name);
        }

        
    }
}
