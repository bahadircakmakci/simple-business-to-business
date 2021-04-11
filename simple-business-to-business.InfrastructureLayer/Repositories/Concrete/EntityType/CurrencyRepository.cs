﻿using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.DomainLayer.Repositories.Interfaces.EntityType;
using simple_business_to_business.InfrastructureLayer.Context;
using simple_business_to_business.InfrastructureLayer.Repositories.Concrete.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.InfrastructureLayer.Repositories.Concrete.EntityType
{
    public class CurrencyRepository:BaseRepository<Currencies>, ICurrencyRepository
    {
        public CurrencyRepository(ApplicationDbContext context):base(context)
        {

        }
    }
}
