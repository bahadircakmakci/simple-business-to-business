using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.ApplicationLayer.IoC
{
    public static class DependencyInjection
    {
        //Aslında burada Startup.cs sınıfı içerisindeki Configure() methodunun içerisini dışarıya taşıdık gibi düşünebilirsiniz. Burada unutmammız gereken nokta bu static sınıfın Startup.cs içerisinde çağrılarak eklenmesi gerekmektedir.

        //public static IServiceCollection RegisterService(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(typeof(Mapping));

        //    services.AddScoped<IUnitOfWork, UnitOfWork>();
        //    services.AddScoped<IAppUserService, AppUserSerivice>();

        //    return service;
        //}
    }
}
