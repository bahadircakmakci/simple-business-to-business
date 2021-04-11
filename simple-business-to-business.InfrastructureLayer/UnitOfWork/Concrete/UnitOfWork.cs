using simple_business_to_business.DomainLayer.Repositories.Interfaces.EntityType;
using simple_business_to_business.DomainLayer.UnitOfWork;
using simple_business_to_business.InfrastructureLayer.Context;
using simple_business_to_business.InfrastructureLayer.Repositories.Concrete.EntityType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.InfrastructureLayer.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        private IOrderRepository _orderRepository;
        public IOrderRepository OrderRepository => _orderRepository ?? (_orderRepository = new OrderRepository(_context));

        private IOrderDetailsRepository _orderDetailsRepository;
        public IOrderDetailsRepository OrderDetailsRepository => _orderDetailsRepository ?? (_orderDetailsRepository = new OrderDetailRepository(_context));
        private IBasketRepository _basketRepository;
        public IBasketRepository BasketRepository => _basketRepository ?? (_basketRepository = new BasketRepository(_context));
        private IProductRepository _productRepository;
        public IProductRepository ProductRepository => _productRepository ?? (_productRepository = new ProductRepository(_context));
        private ICompanyRepository _companyRepository;
        public ICompanyRepository CompanyRepository => _companyRepository ?? (_companyRepository = new CompanyRepository(_context));

        public async Task Commit() => await _context.SaveChangesAsync();

        private bool isDispose = false;

        public async ValueTask DisposeAsync()
        {
            if (!isDispose)
            {
                isDispose = true;
                await DisposeAsync(true);
                GC.SuppressFinalize(this); //https://docs.microsoft.com/en-us/dotnet/api/system.gc.suppressfinalize?view=net-5.0, https://stackoverflow.com/questions/151051/when-should-i-use-gc-suppressfinalize
            }
        }

        protected async ValueTask DisposeAsync(bool disposing)
        {
            if (disposing) await _context.DisposeAsync();
        }
    }
}
