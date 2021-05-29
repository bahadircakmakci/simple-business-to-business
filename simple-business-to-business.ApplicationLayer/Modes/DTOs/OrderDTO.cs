using simple_business_to_business.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.ApplicationLayer.Modes.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public int AppUserId { get; set; }

        public int CompanyId { get; set; }

        public decimal TotalDiscount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalTax { get; set; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }


        public Status Status { get; set; }
    }
}
