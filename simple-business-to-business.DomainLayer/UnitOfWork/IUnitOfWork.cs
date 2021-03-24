using simple_business_to_business.DomainLayer.Repositories.Interfaces.EntityType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.DomainLayer.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        IOrderRepository OrderRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }
        IBasketRepository BasketRepository { get; }        
        IProductRepository ProductRepository { get; }
        ICompanyRepository CompanyRepository { get; }



        Task Commit();
    }
}
