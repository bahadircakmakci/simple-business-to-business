using Autofac;
using FluentValidation;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Concrete;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.ApplicationLayer.Validations.FluentValidations;
using simple_business_to_business.DomainLayer.Repositories.Interfaces.EntityType;
using simple_business_to_business.DomainLayer.UnitOfWork;
using simple_business_to_business.InfrastructureLayer.Repositories.Concrete.EntityType;
using simple_business_to_business.InfrastructureLayer.UnitOfWork.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.ApplicationLayer.IoC
{
    public class DependencyResolver:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Serivces
            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<CompanyService>().As<ICompanyService>().InstancePerLifetimeScope();

            //UnitofWork
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            //Repository
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>().InstancePerLifetimeScope();

            //Validation
            builder.RegisterType<LoginValidation>().As<IValidator<LoginDTO>>().InstancePerLifetimeScope();
            builder.RegisterType<RegisterValidation>().As<IValidator<RegisterDTO>>().InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}
