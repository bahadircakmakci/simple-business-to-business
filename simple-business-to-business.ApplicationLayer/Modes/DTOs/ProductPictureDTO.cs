using Microsoft.AspNetCore.Http;
using simple_business_to_business.DomainLayer.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_business_to_business.ApplicationLayer.Modes.DTOs
{
    public class ProductPictureDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
   
      

        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }


        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public Status Status { get ; set; }
    }
}
