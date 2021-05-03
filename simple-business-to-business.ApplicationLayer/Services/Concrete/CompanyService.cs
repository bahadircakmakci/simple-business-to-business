using AutoMapper;
using Microsoft.EntityFrameworkCore;
using simple_business_to_business.ApplicationLayer.Modes.DTOs;
using simple_business_to_business.ApplicationLayer.Services.Interfaces;
using simple_business_to_business.DomainLayer.Entities.Concrete;
using simple_business_to_business.DomainLayer.Repositories.Interfaces.EntityType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace simple_business_to_business.ApplicationLayer.Services.Concrete
{
    public class CompanyService : ICompanyService
    {
        private ICompanyRepository _companyRepository;
        private IMapper _Mapper;
        public CompanyService(ICompanyRepository companyRepository,IMapper Mapper)
        {
            _companyRepository = companyRepository;
            _Mapper = Mapper;
        }

        public async Task AddCompany(CompaniesDto AddCompany)
        {
            await _companyRepository.Add(_Mapper.Map<Companies>(AddCompany));
            await _companyRepository.Commit();
           await _companyRepository.DisposeAsync();
           
        }

        public async Task<int> CompanyIdFromName(string companyname)
        {
            return await _companyRepository.GetFilteredFirstOrDefault(selector: x => x.Id, expression: x => x.CompanyName == companyname);
        }

        public async Task DeleteCompany(int id)
        {
            var removecompany = await _companyRepository.GetById(id);
            _companyRepository.Delete(removecompany);
        }

        public async Task EditCompany(CompaniesDto editCompanyDTO)
        {
              _companyRepository.Update(_Mapper.Map<Companies>(editCompanyDTO));
        }

        public async Task<CompaniesDto> GetById(int id)
        {
            return _Mapper.Map<CompaniesDto>(await _companyRepository.GetById(id));
        }

        public async Task<List<CompaniesDto>> ListCompany(int pageIndex)
        {
            var company = await _companyRepository.GetFilteredList(
               selector: x => new CompaniesDto
               {
                   Id = x.Id,
                   AccountingCode=x.AccountingCode,
                   Address=x.Address,
                   City=x.City,
                   CompanyName=x.CompanyName,
                   DeleteDate=x.DeleteDate,
                   Fax=x.Fax,
                   Phone1=x.Phone1,
                   Phone2=x.Phone2,
                   RiskLimit=x.RiskLimit,
                   State=x.State,
                    TaxAdress=x.TaxAdress,
                   TaxNumber=x.TaxNumber,
                   TotalBalance=x.TotalBalance,
                   TotalRiskLimit=x.TotalRiskLimit,
                   UpdateDate=x.UpdateDate,
                   PlasiyerCode = x.PlasiyerCode,
                   Status = x.Status
               },
               expression: null,
               include: null,
               pageIndex: pageIndex,
               pageSize: 10);

            return company;
        }
    }
}
