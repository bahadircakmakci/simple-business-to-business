using Microsoft.AspNetCore.Identity;
using simple_business_to_business.DomainLayer.Entities.Interfaces;
using simple_business_to_business.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace simple_business_to_business.DomainLayer.Entities.Concrete
{
    public class AppUsers : IdentityUser<int>, IBaseEntity
    {


        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public int PlasiyerCode { get; set; }
        public string ImagePath { get; set; } = "/images/users/default.jpg";

        public int? CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public Companies Companies { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }

        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Passive;
        public Status Status { get => _status; set => _status = value; }
    }
}
