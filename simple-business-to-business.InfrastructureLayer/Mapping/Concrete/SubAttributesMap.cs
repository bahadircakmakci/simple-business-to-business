using Microsoft.EntityFrameworkCore.Metadata.Builders;
using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.InfrastructureLayer.Mapping.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.InfrastructureLayer.Mapping.Concrete
{
    public class SubAttributesMap:BaseMap<SubAttributes>
    {
        public override void Configure(EntityTypeBuilder<SubAttributes> builder)
        {
            builder.HasKey(x=>x.Id);
            base.Configure(builder);
        }
    }
}
