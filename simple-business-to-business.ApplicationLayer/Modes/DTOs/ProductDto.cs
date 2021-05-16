using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.ApplicationLayer.Modes.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }

        public int BrandId { get; set; }
 
        public int MainCategoryId { get; set; }
 

        public int SubCategoryId { get; set; }
 

        public decimal Vat { get; set; }


        public int CurrencyId { get; set; }
 

        public decimal ListPrice { get; set; }
        public decimal ListPriceVat { get; set; }

        public int Quantity { get; set; }
        public int CriticalQuantity { get; set; }
        public int MaxSellerQuantity { get; set; }

         
    }
}
