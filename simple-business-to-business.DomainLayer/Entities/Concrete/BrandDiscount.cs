using simple_business_to_business.DomainLayer.Entities.Interfaces;
using simple_business_to_business.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.DomainLayer.Entities.Concrete
{
    public class BrandDiscount : IBaseEntity
    {
        public int Id { get; set; }

        public int BrandId { get; set; }
        public Brands Brands { get; set; }

        public int Discount1 { get; set; }
        public int Discount2 { get; set; }
        public int Discount3 { get; set; }
        public int Discount4 { get; set; }
        public int Discount5 { get; set; }


        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
