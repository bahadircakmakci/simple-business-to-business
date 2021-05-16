using simple_business_to_business.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.ApplicationLayer.Modes.DTOs
{
    public class ListUserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string FullName { get; set; }
        public int PlasiyerCode { get; set; }

        public int? CompanyId { get; set; }
        public CompaniesDto Companies { get; set; }

        public Status Status { get; set; }
    }
}
