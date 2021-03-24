using simple_business_to_business.DomainLayer.Entities.Interfaces;
using simple_business_to_business.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.DomainLayer.Entities.Concrete
{
    public class Basket:IBaseEntity
    {
        public int Id { get; set; }

        public int AppUserId { get; set; }
        public AppUsers AppUsers { get; set; }

        public int ProductId { get; set; }
        public Products Products { get; set; }

        public int BasketQuantity { get; set; }



        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
