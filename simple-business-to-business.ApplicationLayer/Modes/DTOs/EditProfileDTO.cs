using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace simple_business_to_business.ApplicationLayer.Modes.DTOs
{
    public class EditProfileDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int CompanyId { get; set; }
        
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
