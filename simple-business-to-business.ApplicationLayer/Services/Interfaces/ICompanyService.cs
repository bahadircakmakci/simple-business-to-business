using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Interfaces
{
    public interface ICompanyService
    {  
        Task<int> CompanyIdFromName(string companyname);
        Task<CompaniesDto> GetById(int id);
        Task EditCompany(CompaniesDto editCompanyDTO);
        Task AddCompany(CompaniesDto AddCompany);
        Task DeleteCompany(int id);
        Task<List<CompaniesDto>> ListCompany(int pageIndex);
    }
}
