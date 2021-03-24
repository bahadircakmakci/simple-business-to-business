using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.DomainLayer.Repositories.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.DomainLayer.Repositories.Interfaces.EntityType
{
    public interface IOrderRepository : IRepository<Orders>
    {
    }
}
