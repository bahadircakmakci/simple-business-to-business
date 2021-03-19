using Microsoft.AspNetCore.Identity;
using simple_business_to_business.DomainLayer.Entities.Interfaces;
using simple_business_to_business.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.DomainLayer.Entities.Concrete
{
    public class AppUser : IdentityUser<int>, IBaseEntity
    {


        public string FullName { get; set; }
        public string ImagePath { get; set; } = "/images/users/default.jpg";

        public int CompanyId { get; set; }
        public Companies Companies { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
