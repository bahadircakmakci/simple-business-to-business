using simple_business_to_business.DomainLayer.Entities.Interfaces;
using simple_business_to_business.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace simple_business_to_business.DomainLayer.Entities.Concrete
{
    public class Products : IBaseEntity
    {

        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }

        public int BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public Brands Brands { get; set; }

        public int MainCategoryId { get; set; }
        [ForeignKey(nameof(MainCategoryId))]
        public MainCategories MainCategories { get; set; }

        public int SubCategoryId { get; set; }
        [ForeignKey(nameof(SubCategoryId))]
        public SubCategories SubCategories { get; set; }

        public decimal Vat { get; set; }


        public int CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public Currencies Currencies { get; set; }

        public decimal ListPrice { get; set; }
        public decimal ListPriceVat { get; set; }

        public int Quantity { get; set; }
        public int CriticalQuantity { get; set; }
        public int MaxSellerQuantity { get; set; }


        public ICollection<ProductPictures>ProductPictures { get; set; }

        public ICollection<MainAttributes> MainAttributes { get; set; }

        public ICollection<SubAttributes> SubAttributes { get; set; }

        public Companies Companies { get; set; }


        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
