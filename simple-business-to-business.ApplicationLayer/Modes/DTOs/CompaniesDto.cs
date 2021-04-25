using simple_business_to_business.DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace simple_business_to_business.ApplicationLayer.Modes.DTOs
{
    public class CompaniesDto
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string AccountingCode { get; set; }
        public string TaxNumber { get; set; }
        public string TaskAdress { get; set; }
        public string PlasiyerCode { get; set; }
        public decimal RiskLimit { get; set; }
        public decimal TotalRiskLimit { get; set; }



        public decimal TotalBalance { get; set; }
 
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

 
        public Status Status { get; set; }
    }
}
