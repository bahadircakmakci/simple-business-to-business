using simple_business_to_business.DomainLayer.Entities.Interfaces;
using simple_business_to_business.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.DomainLayer.Entities.Concrete
{
    public class SubCategories : IBaseEntity
    {
        public int Id { get; set; }
        public string CategoyName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } = "/images/SubCategory/default.jpg";

        
        public int MainCategoryId { get; set; }
        public MainCategories MainCategories { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
